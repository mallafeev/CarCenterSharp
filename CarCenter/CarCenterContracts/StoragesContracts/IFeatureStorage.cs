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
	public interface IFeatureStorage
	{
		List<FeatureViewModel> GetFullList();
		List<FeatureViewModel> GetFilteredList(FeatureSearchModel model);
		FeatureViewModel? GetElement(FeatureSearchModel model);
		FeatureViewModel? Insert(FeatureBindingModel model);
		FeatureViewModel? Update(FeatureBindingModel model);
		FeatureViewModel? Delete(FeatureBindingModel model);
	}
}
