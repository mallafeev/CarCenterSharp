using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.BindingModels
{
	public class ReportBindingModel
	{
		public string FileName { get; set; } = string.Empty;
		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
	}

}
