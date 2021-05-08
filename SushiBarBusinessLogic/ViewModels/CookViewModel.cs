using System.ComponentModel;

namespace SushiBarBusinessLogic.ViewModels
{
    public class CookViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО исполнителя")]
        public string CookFIO { get; set; }
        [DisplayName("Время на заказ")]
        public int WorkingTime { get; set; }
        [DisplayName("Время на перерыв")]
        public int PauseTime { get; set; }
    }
}
