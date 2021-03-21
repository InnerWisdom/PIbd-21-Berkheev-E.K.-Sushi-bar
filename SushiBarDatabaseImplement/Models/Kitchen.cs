using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiBarDatabaseImplement.Models
{
    public class Kitchen
    {
        public int Id { get; set; }

        [Required]
        public string KitchenName { get; set; }

        [Required]
        public string ResponsiblePersonFullName { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        [ForeignKey("KitchenId")]
        public virtual List<KitchenIngredient> KitchenIngredients { get; set; }
    }
}
