using System;
using System.Collections.Generic;
using System.Text;

namespace SushiBarFileImplement.Models
{
    public class Cook
    {
        public int Id { get; set; }
        public string CookFIO { get; set; }
        public int WorkingTime { get; set; }
        public int PauseTime { get; set; }
    }
}
