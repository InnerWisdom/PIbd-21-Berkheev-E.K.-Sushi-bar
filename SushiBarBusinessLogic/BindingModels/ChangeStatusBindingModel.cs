using System;
using System.Collections.Generic;
using System.Text;

namespace SushiBarBusinessLogic.BindingModels
{
    public class ChangeStatusBindingModel
    {
        public int OrderId { get; set; }
        public int? CookId { get; set; }
    }
}
