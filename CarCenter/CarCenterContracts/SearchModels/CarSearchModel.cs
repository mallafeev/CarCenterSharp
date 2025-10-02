using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.SearchModels
{
	public class CarSearchModel
	{
		public int? Id { get; set; }
		public long? VINnumber { get; set; }
        public int? StorekeeperId { get; set; }
        public int? FeatureId {  get; set; }
        public int? OrderId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
