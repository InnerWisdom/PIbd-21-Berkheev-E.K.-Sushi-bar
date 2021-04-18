using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiBarDatabaseImplement.Models
{
    public class Cook
    {
        public int Id { get; set; }
        public string CookFIO { get; set; }
        public int WorkingTime { get; set; }
        public int PauseTime { get; set; }
        [ForeignKey("CookId")]
        public List<Order> Order { get; set; }
    }
}
