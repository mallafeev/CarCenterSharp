using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.SearchModels
{
	public class OrderSearchModel
	{
		public int? Id { get; set; }
        public int? BundlingId { get; set; }
        public int? WorkerId { get; set; }
        public List<IPresaleModel> Presales { get; set; } = new();
        public List<ICarModel> Cars { get; set; } = new();
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
