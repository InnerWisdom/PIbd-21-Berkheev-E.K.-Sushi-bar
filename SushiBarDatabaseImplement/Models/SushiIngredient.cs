using System.ComponentModel.DataAnnotations;

namespace SushiBarDatabaseImplement.Models
{
    public class SushiIngredient
    {
        public int Id { get; set; }
        public int SushiId { get; set; }
        public int IngredientId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public virtual Sushi Sushi { get; set; }
    }
}
