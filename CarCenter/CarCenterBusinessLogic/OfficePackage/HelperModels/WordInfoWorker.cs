using CarCenterContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfoWorker
    {
        public MemoryStream memoryStream { get; set; } = new MemoryStream();
        public string Title { get; set; } = string.Empty;
        public List<ReportPresaleViewModel> carPresalesReport { get; set; } = new();
    }
}
