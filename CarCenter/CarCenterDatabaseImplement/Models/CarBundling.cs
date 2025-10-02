using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDatabaseImplement.Models
{
	public class CarBundling
	{
		public int Id { get; set; }
		[Required]
		public int CarId { get; set; }
		[Required]
		public int BundlingId { get; set; }
		[Required]
		public virtual Car Car { get; set; } = new();
		public virtual Bundling Bundling { get; set; } = new();
	}
}
