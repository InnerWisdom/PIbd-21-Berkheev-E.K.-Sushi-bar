using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Enums;
using SushiBarBusinessLogic.HelperModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace SushiBarBusinessLogic.BusinessLogics
{
    public class OrderLogic
    {
        private readonly IOrderStorage _orderStorage;

        private readonly ISushiStorage _sushiStorage;

        private readonly IKitchenStorage _kitchenStorage;

        private readonly IClientStorage _clientStorage;

        private readonly object locker = new object();
        public OrderLogic(IOrderStorage orderStorage, ISushiStorage sushiStorage,
            IKitchenStorage kitchenStorage, IClientStorage clientStorage)
        {
            _orderStorage = orderStorage;
            _kitchenStorage = kitchenStorage;
            _sushiStorage = sushiStorage;
            _clientStorage = clientStorage;
        }
        public List<OrderViewModel> Read(OrderBindingModel sample)
        {
            if (sample == null)
            {
                return _orderStorage.GetFullList();
            }
            if (sample.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(sample) };
            }
            return _orderStorage.GetFilteredList(sample);
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                SushiId = model.SushiId,
                ClientId = model.ClientId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel { Id = model.ClientId })?.Email,
                Subject = $"Новый заказ",
                Text = $"Заказ от {DateTime.Now} на сумму {model.Sum:N2} принят."
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            lock (locker)
            {

                OrderStatus status = OrderStatus.Выполняется;
                var order = _orderStorage.GetElement(new OrderBindingModel
                {
                    Id = model.OrderId
                });
                if (order == null)
                {
                    throw new Exception("Заказ не найден");
                }
                if (order.Status != OrderStatus.Принят)
                {
                    throw new Exception("Заказ не в статусе \"принят\"");
                }
                if (!_kitchenStorage.CheckIngredientsCount(model.OrderId))
                {
                    status = OrderStatus.НужныИнгредиенты;
                }
                if (order.CookId.HasValue)
                {
                    throw new Exception("Order already has implementer");
                }
                _orderStorage.Update(new OrderBindingModel
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    CookId = model.CookId,
                    SushiId = order.SushiId,
                    Count = order.Count,
                    Sum = order.Sum,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                    Status = status
                });
                MailLogic.MailSendAsync(new MailSendInfo
                {
                    MailAddress = _clientStorage.GetElement(new ClientBindingModel { Id = order.ClientId })?.Email,
                    Subject = $"Заказ №{order.Id}",
                    Text = $"Заказ №{order.Id} передан в работу."
                });
            }
                
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id =
           model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status == OrderStatus.НужныИнгредиенты && _kitchenStorage.CheckIngredientsCount(model.OrderId))
            {
                order.Status = OrderStatus.Выполняется;
            }
            if (order.Status != OrderStatus.Выполняется)
            {
                return;
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                CookId = order.CookId,
                SushiId = order.SushiId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel { Id = order.ClientId })?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} выполнен."
            });
        }
        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id =
           model.OrderId
            });

            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }

            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }

            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                CookId=order.CookId,
                SushiId = order.SushiId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Оплачен
            });
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel { Id = order.ClientId })?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} оплачен."
            });
        }
    }

}
