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
	public interface IBundlingLogic
	{
		List<BundlingViewModel>? ReadList(BundlingSearchModel? model);
		BundlingViewModel? ReadElement(BundlingSearchModel model);
		bool Create(BundlingBindingModel model);
		bool Update(BundlingBindingModel model);
		bool Delete(BundlingBindingModel model);
	}
}
