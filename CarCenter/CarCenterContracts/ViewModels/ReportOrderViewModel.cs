using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.ViewModels
{
	public class ReportOrderViewModel
	{
        public int OrderId { get; set; }
        public List<string> Bundlings { get; set; } = new();
        public List<string> Requests { get; set; } = new();
    }
}
