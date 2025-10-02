using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.BindingModels
{
	public class CarBindingModel : ICarModel
	{
		public int Id { get; set; }
		public int StorekeeperId { get; set; }
		public int? OrderId { get; set; }
		public CarBrand CarBrand { get; set; } = CarBrand.Неизвестно;
		public string Model { get; set; } = string.Empty;
		public CarClass CarClass { get; set; } = CarClass.Неизвестно;
		public int Year { get; set; }
		public double Price { get; set; }
		public long VINnumber { get; set; }
		public int FeatureID { get; set; }
		public Dictionary<int, IBundlingModel> CarBundlings { get; set; } = new();
        public DateTime DateCreate { get; set; }
    }
}
