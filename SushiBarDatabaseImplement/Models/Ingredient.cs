using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiBarDatabaseImplement.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string IngredientName { get; set; }
        [ForeignKey("IngredientId")]
        public virtual List<SushiIngredient> SushiIngredients { get; set; }
    }
}
