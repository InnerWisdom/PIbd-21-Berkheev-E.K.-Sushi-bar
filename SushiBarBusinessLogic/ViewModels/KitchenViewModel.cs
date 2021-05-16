using System;
using System.Collections.Generic;
using System.ComponentModel;
using SushiBarBusinessLogic.Attributes;

namespace SushiBarBusinessLogic.ViewModels
{
    public class KitchenViewModel
    {
        [Column(title: "Number", gridViewAutoSize: GridViewAutoSize.Fill)]
        public int Id { get; set; }

        [DisplayName("Название кухни")]
        [Column(title: "Название кухни", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string KitchenName { get; set; }

        [DisplayName("ФИО ответственного")]
        [Column(title: "ФИО ответственного", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ResponsiblePersonFullName { get; set; }

        [DisplayName("Дата создания кухни")]
        [Column(title: "Дата создания кухни", width: 250, format: "D")]
        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> KitchenIngredients { get; set; }
    }
}
