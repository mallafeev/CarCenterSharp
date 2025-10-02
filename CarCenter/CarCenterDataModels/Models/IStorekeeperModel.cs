using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDataModels.Models
{
	public interface IStorekeeperModel : IId
	{
		string Name { get; }
		string Surname { get; }
		string? Patronymic { get; }
		string Password { get; }
		string Email { get; }
		long PhoneNumber { get; }

	}
}
