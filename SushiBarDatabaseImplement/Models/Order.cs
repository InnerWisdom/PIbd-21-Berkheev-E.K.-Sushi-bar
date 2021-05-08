using SushiBarBusinessLogic.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SushiBarDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int SushiId { get; set; }
        public int? CookId { get; set; }
        public int ClientId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public virtual Sushi Sushi { get; set; }
        public virtual Client Client { get; set; }
        public virtual Cook Cook { get; set; }
        public DateTime? DateImplement { get; set; }
    }
}
