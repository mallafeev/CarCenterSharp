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
	public interface IRequestStorage
	{
		List<RequestViewModel> GetFullList();
		List<RequestViewModel> GetFilteredList(RequestSearchModel model);
		RequestViewModel? GetElement(RequestSearchModel model);
		RequestViewModel? Insert(RequestBindingModel model);
		RequestViewModel? Update(RequestBindingModel model);
		RequestViewModel? Delete(RequestBindingModel model);
	}
}
