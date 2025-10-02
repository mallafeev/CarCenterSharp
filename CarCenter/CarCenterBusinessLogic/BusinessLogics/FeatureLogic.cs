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
	public class FeatureLogic : IFeatureLogic
	{
		private readonly ILogger _logger;
		private readonly IFeatureStorage _fatureStorage;
		public FeatureLogic(ILogger<FeatureLogic> logger, IFeatureStorage fatureStorage)
		{
			_logger = logger;
			_fatureStorage = fatureStorage;
		}

		public List<FeatureViewModel>? ReadList(FeatureSearchModel? model)
		{
			_logger.LogInformation("ReadList. FeatureId:Id:{ Id}", model?.Id);
			var list = model == null ? _fatureStorage.GetFullList() : _fatureStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}

		public FeatureViewModel? ReadElement(FeatureSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. FeatureId:Id:{ Id}", model.Id);
			var element = _fatureStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}

		public bool Create(FeatureBindingModel model)
		{
			CheckModel(model);
			if (_fatureStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}

		public bool Update(FeatureBindingModel model)
		{
			CheckModel(model);
			if (_fatureStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}

		public bool Delete(FeatureBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_fatureStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}

		private void CheckModel(FeatureBindingModel model, bool withParams = true)
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
			_logger.LogInformation("Feature. Feature:Id:{ Id}.Price:{ Price}", model.Id, model.Price);
		}
	}
}
