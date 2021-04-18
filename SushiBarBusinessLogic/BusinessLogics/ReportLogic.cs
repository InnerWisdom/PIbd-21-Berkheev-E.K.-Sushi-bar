using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.HelperModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SushiBarBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly ISushiStorage _sushiStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly IKitchenStorage _kitchenStorage;
        public ReportLogic(ISushiStorage sushiStorage, IOrderStorage orderStorage, IKitchenStorage kitchenStorage)
        {
            _sushiStorage = sushiStorage;
            _orderStorage = orderStorage;
            _kitchenStorage = kitchenStorage;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportSushiIngredientViewModel> GetSushiIngredient()
        {
            var sushis = _sushiStorage.GetFullList();
            var list = new List<ReportSushiIngredientViewModel>();
            foreach (var sushi in sushis)
            {
                var record = new ReportSushiIngredientViewModel
                {
                    SushiName = sushi.SushiName,
                    Ingredients = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var ingredient in sushi.SushiIngredients)
                {
                    record.Ingredients.Add(new Tuple<string, int>(ingredient.Value.Item1, ingredient.Value.Item2));
                    record.TotalCount += ingredient.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                SushiName = x.SushiName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
            .ToList();
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveIngredientsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список суши",
                Sushis = _sushiStorage.GetFullList()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveSushiIngredientToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список суши",
                SushiIngredients = GetSushiIngredient()
            });
        }

        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        [Obsolete]
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }

        public List<ReportKitchenIngredientViewModel> GetKitchenIngredient()
        {
            var stores = _kitchenStorage.GetFullList();
            var list = new List<ReportKitchenIngredientViewModel>();
            foreach (var store in stores)
            {
                var record = new ReportKitchenIngredientViewModel
                {
                    KitchenName = store.KitchenName,
                    Ingredients = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in store.KitchenIngredients)
                {
                    record.Ingredients.Add(new Tuple<string, int>(component.Value.Item1, component.Value.Item2));
                    record.TotalCount += component.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        public List<OrderReportByDateViewModel> GetOrderReportByDate()
        {
            return _orderStorage.GetFullList()
                .GroupBy(order => order.DateCreate.ToShortDateString())
                .Select(rec => new OrderReportByDateViewModel
                {
                    Date = Convert.ToDateTime(rec.Key),
                    Count = rec.Count(),
                    Sum = rec.Sum(order => order.Sum)
                })
                .ToList();
        }

        public void SaveKitchensToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateKitchensDoc(new KitchenWordInfo
            {
                FileName = model.FileName,
                Title = "Список кухонь",
                Kitchens = _kitchenStorage.GetFullList()
            });
        }

        public void SaveKitchenIngredientToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateKitchensDoc(new KitchensExcelInfo
            {
                FileName = model.FileName,
                Title = "Список кухонь",
                KitchenIngredients = GetKitchenIngredient()
            });
        }

        public void SaveOrderReportByDateToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDocOrderReportByDate(new PdfInfoOrderReportByDate
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrderReportByDate()
            });
        }
    }
}
