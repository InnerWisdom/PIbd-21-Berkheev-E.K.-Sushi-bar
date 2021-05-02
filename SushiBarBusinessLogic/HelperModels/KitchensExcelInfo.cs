using System;
using System.Collections.Generic;
using SushiBarBusinessLogic.ViewModels;

namespace SushiBarBusinessLogic.HelperModels
{
    class KitchensExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportKitchenIngredientViewModel> KitchenIngredients { get; set; }
    }
}
