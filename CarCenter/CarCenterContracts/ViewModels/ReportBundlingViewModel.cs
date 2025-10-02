using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.ViewModels
{
	public class ReportBundlingViewModel
	{
		public int BundlingId { get; set; }
		public List<string> Orders { get; set; } = new();
		public List<string> Features { get; set; } = new();
	}
}
