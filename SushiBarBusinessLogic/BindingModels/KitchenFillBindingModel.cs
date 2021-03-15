using System;
using System.Collections.Generic;
using System.Text;

namespace SushiBarBusinessLogic.BindingModels
{
    public class KitchenFillBindingModel
    {
        public int KitchenId { get; set; }

        public int IngredientId { get; set; }

        public int Count { get; set; }
    }
}
