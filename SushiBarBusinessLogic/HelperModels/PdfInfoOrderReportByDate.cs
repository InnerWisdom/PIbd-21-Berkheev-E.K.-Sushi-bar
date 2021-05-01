using System;
using System.Collections.Generic;
using SushiBarBusinessLogic.ViewModels;

namespace SushiBarBusinessLogic.HelperModels
{
    class PdfInfoOrderReportByDate
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<OrderReportByDateViewModel> Orders { get; set; }
    }
}
