using System;
using System.Collections.Generic;
using System.Text;

namespace SushiBarFileImplement.Models
{
    public class Kitchen
    {
        public int Id { get; set; }

        public string KitchenName { get; set; }

        public string ResponsiblePersonFullName { get; set; }

        public DateTime DateCreate { get; set; }

        public Dictionary<int, int> KitchenIngredients{ get; set; }
    }
}
