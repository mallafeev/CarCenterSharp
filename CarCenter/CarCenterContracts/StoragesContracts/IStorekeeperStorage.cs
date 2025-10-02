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
	public interface IStorekeeperStorage
	{
		List<StorekeeperViewModel> GetFullList();
		List<StorekeeperViewModel> GetFilteredList(StorekeeperSearchModel model);
		StorekeeperViewModel? GetElement(StorekeeperSearchModel model);
		StorekeeperViewModel? Insert(StorekeeperBindingModel model);
		StorekeeperViewModel? Update(StorekeeperBindingModel model);
		StorekeeperViewModel? Delete(StorekeeperBindingModel model);
	}
}
