using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.ViewModels
{
	public class ReportPresaleViewModel
	{
        public int PresaleId { get; set; }
        public List<string> Cars { get; set; } = new();
    }
}
