using CarCenterContracts.SearchModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDatabaseImplement.Models
{
	public class PresaleBundling
	{
		public int Id { get; set; }
		[Required]
		public int PresaleId { get; set; }
		[Required]
		public int BundlingId { get; set; }
		[Required]
		public virtual Presale Presale { get; set; } = new();
		public virtual Bundling Bundling { get; set; } = new();
	}
}
