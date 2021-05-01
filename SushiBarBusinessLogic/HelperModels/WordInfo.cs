using System.Collections.Generic;
using SushiBarBusinessLogic.ViewModels;

namespace SushiBarBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<SushiViewModel> Sushis { get; set; }
    }
}
