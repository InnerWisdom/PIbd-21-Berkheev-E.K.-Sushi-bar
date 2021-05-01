using System;
using System.Collections.Generic;
using SushiBarBusinessLogic.ViewModels;

namespace SushiBarBusinessLogic.HelperModels
{
    class KitchenWordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<KitchenViewModel> Kitchens { get; set; }
    }
}
