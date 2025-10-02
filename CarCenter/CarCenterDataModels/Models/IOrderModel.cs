using CarCenterDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDataModels.Models
{
	public interface IOrderModel : IId
	{
		PaymentType PaymentType { get; }

		PaymentStatus PaymentStatus { get; }

		string BuyerFCS { get; }

		DateTime PaymentDate { get; }

		double Sum {  get; }

		Dictionary<int, IPresaleModel> OrderPresales { get; }

	}
}
