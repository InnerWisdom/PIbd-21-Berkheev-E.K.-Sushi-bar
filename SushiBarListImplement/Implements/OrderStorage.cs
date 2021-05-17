using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using SushiBarBusinessLogic.Enums;
using SushiBarListImplement.Models;
using System;
using System.Collections.Generic;

namespace SushiBarListImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly DataListSingleton source;
        public OrderStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList()
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var component in source.Orders)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var Order in source.Orders)
            {
                if ((!model.DateFrom.HasValue && !model.DateTo.HasValue && Order.DateCreate.Date == model.DateCreate.Date) ||
(model.DateFrom.HasValue && model.DateTo.HasValue && Order.DateCreate.Date >= model.DateFrom.Value.Date && Order.DateCreate.Date <= model.DateTo.Value.Date) ||
(model.ClientId.HasValue && Order.ClientId == model.ClientId) ||
(model.FreeOrders.HasValue && model.FreeOrders.Value && Order.Status == OrderStatus.Принят) ||
(model.CookId.HasValue && Order.CookId == model.CookId && Order.Status == OrderStatus.Выполняется))
                {
                    result.Add(CreateModel(Order));
                }
            }
            return result;
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var Order in source.Orders)
            {
                if (Order.Id == model.Id)
                {
                    return CreateModel(Order);
                }
            }
            return null;
        }
        public void Insert(OrderBindingModel model)
        {
            Order tempOrder = new Order { Id = 1 };
            foreach (var Order in source.Orders)
            {
                if (Order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = Order.Id + 1;
                }
            }
            source.Orders.Add(CreateModel(model, tempOrder));
        }
        public void Update(OrderBindingModel model)
        {
            Order tempOrder = null;
            foreach (var Order in source.Orders)
            {
                if (Order.Id == model.Id)
                {
                    tempOrder = Order;
                }
            }
            if (tempOrder == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempOrder);
        }
        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.SushiId = model.SushiId;
            order.ClientId = (int)model.ClientId;
            order.CookId = model.CookId;
            order.Count = model.Count;
            order.Status = model.Status;
            order.Sum = model.Sum;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        private OrderViewModel CreateModel(Order order)
        {
            string sushiName = "";
            for (int i = 0; i < source.Sushis.Count; ++i)
            {
                if (source.Sushis[i].Id == order.SushiId)
                {
                    sushiName = source.Sushis[i].SushiName;
                }
            }
            string clientFIO = "";
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id == order.ClientId)
                {
                    clientFIO = source.Clients[i].ClientFIO;
                }
            }
            string cookFIO = "";
            for (int i = 0; i < source.Cooks.Count; ++i)
            {
                if (source.Cooks[i].Id == order.CookId)
                {
                    clientFIO = source.Cooks[i].CookFIO;
                }
            }
            return new OrderViewModel
            {
                Id = order.Id,
                SushiName = sushiName,
                SushiId = order.SushiId,
                ClientId = order.ClientId,
                ClientFIO = clientFIO,
                CookId = order.CookId,
                CookFIO = cookFIO,
                Count = order.Count,
                Status = order.Status,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
            };
        }
    }
}
