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
	public interface IStorekeeperLogic
	{
		List<StorekeeperViewModel>? ReadList(StorekeeperSearchModel? model);
		StorekeeperViewModel? ReadElement(StorekeeperSearchModel model);
		bool Create(StorekeeperBindingModel model);
		bool Update(StorekeeperBindingModel model);
		bool Delete(StorekeeperBindingModel model);
	}
}
