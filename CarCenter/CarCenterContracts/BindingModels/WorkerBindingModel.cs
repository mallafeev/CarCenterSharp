using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.BindingModels
{
	public class WorkerBindingModel : IWorkerModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Surname { get; set; } = string.Empty;
		public string? Patronymic { get; set; }
		public string Password { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public long PhoneNumber { get; set; }
	}
}
