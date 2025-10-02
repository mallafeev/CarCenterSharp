using CarCenterContracts.BindingModels;
using CarCenterContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarCenterDataModels.Models;

namespace CarCenterDatabaseImplement.Models
{
	public class Storekeeper : IStorekeeperModel
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
		[ForeignKey("StorekeeperId")]
		public virtual List<Car> Cars { get; set; } = new();

		public static Storekeeper? Create(StorekeeperBindingModel? model)
		{
			if (model == null)
			{
				return null;
			}
			return new Storekeeper()
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

		public void Update(StorekeeperBindingModel? model)
		{
			if (model == null)
			{
				return;
			}
			Password = model.Password;
			Name = model.Name;
			Surname = model.Surname;
			Patronymic = model.Patronymic;
			Email = model.Email;
			PhoneNumber = model.PhoneNumber;
		}

		public StorekeeperViewModel GetViewModel => new()
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
