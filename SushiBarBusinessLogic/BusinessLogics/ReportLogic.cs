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
        private readonly IIngredientStorage _ingredientStorage;
        private readonly ISushiStorage _printedStorage;
        private readonly IOrderStorage _orderStorage;
        public ReportLogic(ISushiStorage printedStorage, IIngredientStorage ingredientStorage, IOrderStorage orderStorage)
        {
            _printedStorage = printedStorage;
            _ingredientStorage = ingredientStorage;
            _orderStorage = orderStorage;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportSushiIngredientViewModel> GetSushiIngredient()
        {
            var ingredients = _ingredientStorage.GetFullList();
            var printeds = _printedStorage.GetFullList();
            var list = new List<ReportSushiIngredientViewModel>();
            foreach (var printed in printeds)
            {
                var record = new ReportSushiIngredientViewModel
                {
                    SushiName = printed.SushiName,
                    Ingredients = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var ingredient in ingredients)
                {
                    if (printed.SushiIngredients.ContainsKey(ingredient.Id))
                    {
                        record.Ingredients.Add(new Tuple<string, int>(ingredient.IngredientName, printed.SushiIngredients[ingredient.Id].Item2));
                        record.TotalCount += printed.SushiIngredients[ingredient.Id].Item2;
                    }
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
                Title = "Список изделий",
                Sushis = _printedStorage.GetFullList()
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
                Title = "Список изделий",
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
    }
}
