using System;
using System.Collections.Generic;
using System.Text;

namespace SushiBarBusinessLogic.ViewModels
{
    public class ReportSushiIngredientViewModel
    {
        public string SushiName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Ingredients { get; set; }
    }
}
