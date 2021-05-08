using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;

namespace SushiBarBusinessLogic.BusinessLogics
{
    public class WorkModeling
    {

        private readonly ICookStorage _cookStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly OrderLogic _orderLogic;
        private readonly Random rnd;
        public WorkModeling(ICookStorage cookStorage, IOrderStorage orderStorage, OrderLogic orderLogic)
        {
            this._cookStorage = cookStorage;
            this._orderStorage = orderStorage;
            this._orderLogic = orderLogic;
            rnd = new Random(1000);
        }
        /// <summary>
        /// Запуск работ
        /// </summary>
        public void DoWork()
        {
            var cooks = _cookStorage.GetFullList();
            var orders = _orderStorage.GetFilteredList(new OrderBindingModel { FreeOrders = true });
            foreach (var cook in cooks)
            {
                WorkerWorkAsync(cook, orders);
            }
        }
        /// <summary>
        /// Иммитация работы исполнителя
        /// </summary>
        /// <param name="cook"></param>
        /// <param name="orders"></param>
        private async void WorkerWorkAsync(CookViewModel cook, List<OrderViewModel> orders)
        {
            // заказы, которые уже в работе
            var runOrders = await Task.Run(() => _orderStorage.GetFilteredList(new OrderBindingModel { CookId = cook.Id }));
            foreach (var order in runOrders)
            {
                Thread.Sleep(cook.WorkingTime * rnd.Next(1, 5) * order.Count);
                _orderLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = order.Id });
                Thread.Sleep(cook.PauseTime);
            }
            await Task.Run(() =>
            {
                foreach (var order in orders)
                {
                    // заказ на исполнителя
                    try
                    {
                        _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = order.Id, CookId = cook.Id });
                        Thread.Sleep(cook.WorkingTime * rnd.Next(1, 5) * order.Count);
                        _orderLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = order.Id });
                        Thread.Sleep(cook.PauseTime);
                    }
                    catch (Exception) { }
                }
            });
        }


    }
}
