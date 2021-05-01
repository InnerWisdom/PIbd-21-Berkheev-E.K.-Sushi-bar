using SushiBarBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace SushiBarBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportSushiIngredientViewModel> SushiIngredients { get; set; }
    }
}
