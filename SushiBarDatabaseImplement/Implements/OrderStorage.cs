using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SushiBarBusinessLogic.Enums;
using System.Linq;
using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using SushiBarDatabaseImplement.Models;

namespace SushiBarDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (var context = new SushiBarDatabase())
            {
                return context.Orders.Include(rec => rec.Sushi).Include(rec => rec.Client).Include(rec => rec.Cook).Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    SushiId = rec.SushiId,
                    ClientId = rec.ClientId,
                    CookId = rec.CookId,
                    SushiName = rec.Sushi.SushiName,
                    ClientFIO = rec.Client.ClientFIO,
                    CookFIO = rec.CookId.HasValue ? rec.Cook.CookFIO : string.Empty,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                }).ToList();
            }
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new SushiBarDatabase())
            {
                return context.Orders.Include(rec => rec.Sushi).Include(rec => rec.Client).Include(rec => rec.Cook).Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DateCreate.Date == model.DateCreate.Date) || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <= model.DateTo.Value.Date) || (model.ClientId.HasValue && rec.ClientId == model.ClientId) || (model.FreeOrders.HasValue && model.FreeOrders.Value && rec.Status == OrderStatus.Принят) || (model.CookId.HasValue && rec.CookId == model.CookId && rec.Status == OrderStatus.Выполняется)).Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    SushiId = rec.SushiId,
                    ClientId = rec.ClientId,
                    CookId = rec.CookId,
                    SushiName = rec.Sushi.SushiName,
                    ClientFIO = rec.Client.ClientFIO,
                    CookFIO = rec.CookId.HasValue ? rec.Cook.CookFIO : string.Empty,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                }).ToList();
            }
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new SushiBarDatabase())
            {
                var order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ? new OrderViewModel
                {
                    Id = order.Id,
                    SushiId = order.SushiId,
                    ClientId = order.ClientId,
                    CookId = order.CookId,
                    SushiName = context.Sushis.Include(pr => pr.Orders).FirstOrDefault(rec => rec.Id == order.SushiId)?.SushiName,
                    ClientFIO = context.Clients.Include(pr => pr.Orders).FirstOrDefault(rec => rec.Id == order.ClientId)?.ClientFIO,
                    CookFIO = context.Cooks.Include(pr => pr.Order).FirstOrDefault(rec => rec.Id == order.CookId)?.CookFIO,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                } : null;
            }
        }

        public void Insert(OrderBindingModel model)
        {
            using (var context = new SushiBarDatabase())
            {
                if (model.ClientId.HasValue == false)
                {
                    throw new Exception("Клиент не указан");
                }
                Order order = new Order
                {
                    SushiId = model.SushiId,
                    ClientId = (int)model.ClientId,
                    CookId = model.CookId,
                    Count = model.Count,
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                };
                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order);
                context.SaveChanges();
            }
        }
        public void Update(OrderBindingModel model)
        {
            using (var context = new SushiBarDatabase())
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                element.SushiId = model.SushiId;
                element.ClientId = (int)model.ClientId;
                element.CookId = model.CookId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new SushiBarDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new SushiBarDatabase())
            {
                Sushi printed = context.Sushis.FirstOrDefault(rec => rec.Id == model.SushiId);
                if (printed != null)
                {
                    if (printed.Orders == null)
                    {
                        printed.Orders = new List<Order>();
                    }
                    printed.Orders.Add(order);
                    context.Sushis.Update(printed);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Изделие не найден");
                }
                Client client = context.Clients.FirstOrDefault(rec => rec.Id == model.ClientId);
                if (client != null)
                {
                    if (client.Orders == null)
                    {
                        client.Orders = new List<Order>();
                    }
                    client.Orders.Add(order);
                    context.Clients.Update(client);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Клиент не найден");
                }
                Cook cook = context.Cooks.FirstOrDefault(rec => rec.Id == model.CookId);
                if (cook != null)
                {
                    if (cook.Order == null)
                    {
                        cook.Order = new List<Order>();
                    }
                    cook.Order.Add(order);
                    context.Cooks.Update(cook);
                    context.SaveChanges();
                }
            }
            return order;
        }
    }
}
