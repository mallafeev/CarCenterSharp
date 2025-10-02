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
	public interface IPresaleStorage
	{
		List<PresaleViewModel> GetFullList();
		List<PresaleViewModel> GetFilteredList(PresaleSearchModel model);
		PresaleViewModel? GetElement(PresaleSearchModel model);
		PresaleViewModel? Insert(PresaleBindingModel model);
		PresaleViewModel? Update(PresaleBindingModel model);
		PresaleViewModel? Delete(PresaleBindingModel model);
	}
}
