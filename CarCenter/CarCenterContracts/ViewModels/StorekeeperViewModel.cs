using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.ViewModels
{
	public class StorekeeperViewModel : IStorekeeperModel
	{
		public int Id { get; set; }
		[DisplayName("Имя")]
		public string Name { get; set; } = string.Empty;
		[DisplayName("Фамилия")]
		public string Surname { get; set; } = string.Empty;
		[DisplayName("Отчество")]
		public string? Patronymic { get; set; }
		[DisplayName("Пароль")]
		public string Password { get; set; } = string.Empty;
		[DisplayName("Почта")]
		public string Email { get; set; } = string.Empty;
		[DisplayName("Номер телефона")]
		public long PhoneNumber { get; set; }
	}
}
