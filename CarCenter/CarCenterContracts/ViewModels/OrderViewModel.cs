using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.ViewModels
{
	public class OrderViewModel : IOrderModel
	{
		public int Id { get; set; }
		public int WorkerId { get; set; }
		[DisplayName("Имя работника")]
		public string WorkerName { get; set; } = string.Empty;
		[DisplayName("Тип оплаты")]
		public PaymentType PaymentType { get; set; } = PaymentType.Неизвестно;
		[DisplayName("Статус оплаты")]
		public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Неизвестно;
		[DisplayName("ФИО покупателя")]
		public string BuyerFCS { get; set; } = string.Empty;
		[DisplayName("Дата оплаты")]
		public DateTime PaymentDate { get; set; }
		[DisplayName("Сумма")]
		public double Sum { get; set; }
		public Dictionary<int, IPresaleModel> OrderPresales { get; set; } = new();
        public Dictionary<int, ICarModel> Cars { get; set; } = new();
    }
}
