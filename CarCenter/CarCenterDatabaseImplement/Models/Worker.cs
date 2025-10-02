using CarCenterContracts.BindingModels;
using CarCenterContracts.ViewModels;
using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDatabaseImplement.Models
{
	public class Worker : IWorkerModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public string Surname { get; set; } = string.Empty;
		public string? Patronymic { get; set; }
		[Required]
		public string Password { get; set; } = string.Empty;
		[Required]
		public string Email { get; set; } = string.Empty;
		[Required]
		public long PhoneNumber { get; set; }
		[ForeignKey("WorkerId")]
		public virtual List<Order> Orders { get; set; } = new();

		public static Worker? Create(WorkerBindingModel? model)
		{
			if (model == null)
			{
				return null;
			}
			return new Worker()
			{
				Id = model.Id,
				Name = model.Name,
				Surname = model.Surname,
				Patronymic = model.Patronymic,
				Password = model.Password,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
			};
		}

		public void Update(WorkerBindingModel? model)
		{
			if (model == null)
			{
				return;
			}
			Password = model.Password;
			Email = model.Email;
			PhoneNumber = model.PhoneNumber;
		}

		public WorkerViewModel GetViewModel => new()
		{
			Id = Id,
			Name = Name,
			Surname = Surname,
			Patronymic = Patronymic,
			Password = Password,
			Email = Email,
			PhoneNumber = PhoneNumber,
		};
	}
}
