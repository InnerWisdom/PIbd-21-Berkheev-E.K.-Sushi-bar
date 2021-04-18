using System;
using System.Collections.Generic;
using System.Text;

namespace SushiBarBusinessLogic.BindingModels
{
    public class KitchenBindingModel
    {
        public int? Id { get; set; }

        public string KitchenName { get; set; }

        public string ResponsiblePersonFullName { get; set; }

        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> KitchenIngredients { get; set; }
    }
}
