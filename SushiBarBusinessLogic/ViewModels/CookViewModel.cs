using SushiBarBusinessLogic.Attributes;

namespace SushiBarBusinessLogic.ViewModels
{
    public class CookViewModel
    {
        [Column(title: "Number", width: 100)]
        public int Id { get; set; }
        [Column(title: "Implementer FIO", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string CookFIO { get; set; }
        [Column(title: "Time to work", width: 100)]
        public int WorkingTime { get; set; }
        [Column(title: "Time to pause", width: 100)]
        public int PauseTime { get; set; }
    }
}
