using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.SearchModels
{
	public class FeatureSearchModel
	{
		public int? Id { get; set; }
        public int? StorekeeperId { get; set; }
		public int? BundlingId { get; set; }
    }
}
