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
	public interface IBundlingStorage
	{
		List<BundlingViewModel> GetFullList();
		List<BundlingViewModel> GetFilteredList(BundlingSearchModel model);
		BundlingViewModel? GetElement(BundlingSearchModel model);
		BundlingViewModel? Insert(BundlingBindingModel model);
		BundlingViewModel? Update(BundlingBindingModel model);
		BundlingViewModel? Delete(BundlingBindingModel model);
	}
}
