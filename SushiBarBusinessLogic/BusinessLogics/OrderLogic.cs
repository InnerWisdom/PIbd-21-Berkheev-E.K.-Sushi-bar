using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Enums;
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

        public OrderLogic(IOrderStorage orderStorage, ISushiStorage sushiStorage,
            IKitchenStorage kitchenStorage)
        {
            _orderStorage = orderStorage;
            _kitchenStorage = kitchenStorage;
            _sushiStorage = sushiStorage;
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
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят,
                DateImplement=DateTime.Now
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
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
            if (order.Status != OrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            if (!_kitchenStorage.TakeFromKitchen(_sushiStorage.GetElement
                (new SushiBindingModel { Id = order.SushiId }).SushiIngredients, order.Count))
            {
                throw new Exception("Недостаточно ингредиентов");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                SushiId = order.SushiId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Выполняется
            });
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
            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                SushiId = order.SushiId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
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
                SushiId = order.SushiId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Оплачен
            });

        }
    }

}
