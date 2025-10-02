using CarCenterDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDataModels.Models
{
	public interface ICarModel : IId
	{
		CarBrand CarBrand { get; }
		string Model { get; }
		CarClass CarClass { get; }
		int Year { get; }
		double Price { get; }
		long VINnumber { get; }
		int FeatureID { get; }
		Dictionary<int, IBundlingModel> CarBundlings { get; }
        public DateTime DateCreate { get; set; }
    }
}
