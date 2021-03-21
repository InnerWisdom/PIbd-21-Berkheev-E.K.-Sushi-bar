﻿using SushiBarBusinessLogic.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SushiBarDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int SushiId { get; set; }
        public virtual Sushi Sushi { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
    }
}
