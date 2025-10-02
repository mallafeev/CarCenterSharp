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
	public class CarViewModel : ICarModel
	{
		public int Id { get; set; }
		public int? OrderId { get; set; }
		[DisplayName("ФИО покупателя")]
		public string BuyerFCS { get; set; } = string.Empty;
		public int StorekeeperId { get; set; }
		[DisplayName("Марка")]
		public CarBrand CarBrand { get; set; }
		[DisplayName("Модель")]
		public string Model { get; set; } = string.Empty;
		[DisplayName("Класс")]
		public CarClass CarClass { get; set; }
		[DisplayName("Год выпуска")]
		public int Year { get; set; }
		[DisplayName("Цена")]
		public double Price { get; set; }
		[DisplayName("VIN-номер")]
		public long VINnumber { get; set; }
		public int FeatureID { get; set; }
		public Dictionary<int, IBundlingModel> CarBundlings { get; set; } = new();
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
    }
}
