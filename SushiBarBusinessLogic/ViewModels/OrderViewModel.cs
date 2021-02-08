using SushiBarBusinessLogic.Enums;
using System;
using System.ComponentModel;

using System.Collections.Generic;
using System.Text;

namespace SushiBarBusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int SushiId { get; set; }

        [DisplayName("Изделие")]
        public string SushiName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
