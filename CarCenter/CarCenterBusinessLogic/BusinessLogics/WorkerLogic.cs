using CarCenterContracts.BindingModels;
using CarCenterContracts.BusinessLogicsContracts;
using CarCenterContracts.SearchModels;
using CarCenterContracts.StoragesContracts;
using CarCenterContracts.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterBusinessLogic.BusinessLogics
{
	public class WorkerLogic : IWorkerLogic
	{
		private readonly ILogger _logger;
		private readonly IWorkerStorage _workerStorage;
		public WorkerLogic(ILogger<WorkerLogic> logger, IWorkerStorage workerStorage)
		{
			_logger = logger;
			_workerStorage = workerStorage;
		}

		public List<WorkerViewModel>? ReadList(WorkerSearchModel? model)
		{
			_logger.LogInformation("ReadList. WorkerId:Id:{ Id}", model?.Id);
			var list = model == null ? _workerStorage.GetFullList() : _workerStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}

		public WorkerViewModel? ReadElement(WorkerSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. ComponentId:Id:{ Id}", model.Id);
			var element = _workerStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}

		public bool Create(WorkerBindingModel model)
		{
			CheckModel(model);
			if (_workerStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}

		public bool Update(WorkerBindingModel model)
		{
			CheckModel(model);
			if (_workerStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}

		public bool Delete(WorkerBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_workerStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}

		private void CheckModel(WorkerBindingModel model, bool withParams = true)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			if (!withParams)
			{
				return;
			}
			if (model.PhoneNumber <= 0)
			{
				throw new ArgumentNullException("Нет телефона", nameof(model.PhoneNumber));
			}
			_logger.LogInformation("Worker. Worker:Id:{ Id}.PhoneNumber:{ PhoneNumber}", model.Id, model.PhoneNumber);
		}
	}
}
