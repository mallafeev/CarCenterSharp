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
	public interface IPresaleLogic
	{
		List<PresaleViewModel>? ReadList(PresaleSearchModel? model);
		PresaleViewModel? ReadElement(PresaleSearchModel model);
		bool Create(PresaleBindingModel model);
		bool Update(PresaleBindingModel model);
		bool Delete(PresaleBindingModel model);
	}
}
