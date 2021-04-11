using System;
using System.Collections.Generic;
using System.Text;

using SushiBarBusinessLogic.Enums;

namespace SushiBarFileImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int SushiId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }
    }
}
