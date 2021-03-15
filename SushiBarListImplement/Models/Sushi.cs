using System;
using System.Collections.Generic;
using System.Text;

namespace SushiBarListImplement.Models
{
    public class Sushi
    {
        public int Id { get; set; }

        public string SushiName { get; set; }

        public decimal Price { get; set; }

        public Dictionary<int, int> SushiIngredients { get; set; }
    }
}
