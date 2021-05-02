using System.ComponentModel.DataAnnotations;

namespace SushiBarDatabaseImplement.Models
{
    public class KitchenIngredient
    {
        public int Id { get; set; }

        public int KitchenId { get; set; }

        public int IngredientId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public virtual Kitchen Kitchen { get; set; }
    }
}
