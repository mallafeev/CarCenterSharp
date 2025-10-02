using CarCenterContracts.BindingModels;
using CarCenterContracts.SearchModels;
using CarCenterContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.StoragesContracts
{
	public interface ICarStorage
	{
		List<CarViewModel> GetFullList();
		List<CarViewModel> GetFilteredList(CarSearchModel model);
		CarViewModel? GetElement(CarSearchModel model);
		CarViewModel? Insert(CarBindingModel model);
		CarViewModel? Update(CarBindingModel model);
		CarViewModel? Delete(CarBindingModel model);
	}
}
