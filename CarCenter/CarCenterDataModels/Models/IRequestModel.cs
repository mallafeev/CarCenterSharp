using CarCenterDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDataModels.Models
{
	public interface IRequestModel : IId
	{
		string Description { get; }

		RequestTypes RequestType { get; }
	}
}
