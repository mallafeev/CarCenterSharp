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
	public interface IWorkerLogic
	{
		List<WorkerViewModel>? ReadList(WorkerSearchModel? model);
		WorkerViewModel? ReadElement(WorkerSearchModel model);
		bool Create(WorkerBindingModel model);
		bool Update(WorkerBindingModel model);
		bool Delete(WorkerBindingModel model);
	}
}
