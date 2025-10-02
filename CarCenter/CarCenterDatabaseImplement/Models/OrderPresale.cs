using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDatabaseImplement.Models
{
	public class OrderPresale
	{
		public int Id { get; set; }
		[Required]
		public int OrderId { get; set; }
		[Required]
		public int PresaleId { get; set; }
		[Required]
		public virtual Order Order { get; set; } = new();
		public virtual Presale Presale { get; set; } = new();
	}
}
