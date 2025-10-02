using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.SearchModels
{
	public class PresaleSearchModel
	{
		public int? Id { get; set; }
        public int? WorkerId { get; set; }
        public int? CarId { get; set; }
        public int? OrderId { get; set; } 
    }
}
