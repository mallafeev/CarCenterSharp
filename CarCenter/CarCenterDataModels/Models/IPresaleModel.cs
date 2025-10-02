using CarCenterDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDataModels.Models
{
	public interface IPresaleModel : IId
	{
		PresaleStatus PresaleStatus { get; }

		string Description { get; }

		DateTime DueTill { get; }

		double Price { get; }

		Dictionary<int, IBundlingModel> PresaleBundlings { get; }
	}
}
