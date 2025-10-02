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
	public class BundlingLogic : IBundlingLogic
	{
		private readonly ILogger _logger;
		private readonly IBundlingStorage _bundlingStorage;
		public BundlingLogic(ILogger<BundlingLogic> logger, IBundlingStorage bundlingStorage) 
		{
			_logger = logger;
			_bundlingStorage = bundlingStorage;
		}

		public List<BundlingViewModel>? ReadList(BundlingSearchModel? model)
		{
			_logger.LogInformation("ReadList. BundlingId:Id:{ Id}", model?.Id);
			var list = model == null ? _bundlingStorage.GetFullList() : _bundlingStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}

		public BundlingViewModel? ReadElement(BundlingSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. BundlingId:Id:{ Id}", model.Id);
			var element = _bundlingStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}

		public bool Create(BundlingBindingModel model)
		{
			CheckModel(model);
			if (_bundlingStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}

		public bool Update(BundlingBindingModel model)
		{
			CheckModel(model);
			if (_bundlingStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}

		public bool Delete(BundlingBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_bundlingStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}

		private void CheckModel(BundlingBindingModel model, bool withParams = true)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			if (!withParams)
			{
				return;
			}
			if (model.Price <= 0)
			{
				throw new ArgumentNullException("Цена должна быть больше 0", nameof(model.Price));
		    }
			_logger.LogInformation("Bundling. Bundling:Id:{ Id}.Price:{ Price}", model.Id, model.Price);
		}

	}
}
