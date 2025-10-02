using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.BindingModels
{
	public class OrderBindingModel : IOrderModel
	{
		public int Id {  get; set; }
		public int WorkerId { get; set; }
		public PaymentType PaymentType { get; set; } = PaymentType.Неизвестно;

		public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Неизвестно;

		public string BuyerFCS {  get; set; } = string.Empty;

		public DateTime PaymentDate { get; set; }

		public double Sum {  get; set; }

		public Dictionary<int, IPresaleModel> OrderPresales { get; set; } = new();

        public Dictionary<int, ICarModel> Cars { get; set; } = new();
    }
}
