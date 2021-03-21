using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SushiBarBusinessLogic.ViewModels
{
    public class KitchenViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Название кухни")]
        public string KitchenName { get; set; }

        [DisplayName("ФИО ответственного")]
        public string ResponsiblePersonFullName { get; set; }

        [DisplayName("Дата создания кухни")]
        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> KitchenIngredients { get; set; }
    }
}
