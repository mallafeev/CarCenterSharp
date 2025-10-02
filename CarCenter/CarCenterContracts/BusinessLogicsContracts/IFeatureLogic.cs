using CarCenterContracts.BindingModels;
using CarCenterContracts.SearchModels;
using CarCenterContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.BusinessLogicsContracts
{
	public interface IFeatureLogic
	{
		List<FeatureViewModel>? ReadList(FeatureSearchModel? model);
		FeatureViewModel? ReadElement(FeatureSearchModel model);
		bool Create(FeatureBindingModel model);
		bool Update(FeatureBindingModel model);
		bool Delete(FeatureBindingModel model);
	}
}
