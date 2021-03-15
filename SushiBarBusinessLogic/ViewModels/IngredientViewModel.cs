using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SushiBarBusinessLogic.ViewModels
{
    public class IngredientViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название ингредиента")]
        public string IngredientName { get; set; }
    }

}
