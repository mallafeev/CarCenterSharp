using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.ViewModels
{
	public class ReportCarViewModel
	{
		public int CarId { get; set; }
		public int VINnumber { get; set; }
        public List<string> Presales { get; set; } = new();
	}
}
