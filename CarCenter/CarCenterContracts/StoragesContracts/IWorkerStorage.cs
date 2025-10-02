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
	public interface IWorkerStorage
	{
		List<WorkerViewModel> GetFullList();
		List<WorkerViewModel> GetFilteredList(WorkerSearchModel model);
		WorkerViewModel? GetElement(WorkerSearchModel model);
		WorkerViewModel? Insert(WorkerBindingModel model);
		WorkerViewModel? Update(WorkerBindingModel model);
		WorkerViewModel? Delete(WorkerBindingModel model);
	}
}
