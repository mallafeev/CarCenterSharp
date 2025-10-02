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
	public interface IRequestLogic
	{
		List<RequestViewModel>? ReadList(RequestSearchModel? model);
		RequestViewModel? ReadElement(RequestSearchModel model);
		bool Create(RequestBindingModel model);
		bool Update(RequestBindingModel model);
		bool Delete(RequestBindingModel model);
	}
}
