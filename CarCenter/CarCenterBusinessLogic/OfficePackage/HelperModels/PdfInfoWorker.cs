using CarCenterContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfoWorker
    {
        public MemoryStream FileName { get; set; } = new();
        public string Title { get; set; } = string.Empty;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportOrderViewModel> reportBundling { get; set; } = new();
    }
}
