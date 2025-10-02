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
	public class StorekeeperLogic : IStorekeeperLogic
	{
		private readonly ILogger _logger;
		private readonly IStorekeeperStorage _storekeeperStorage;
		public StorekeeperLogic(ILogger<StorekeeperLogic> logger, IStorekeeperStorage storekeeperStorage)
		{
			_logger = logger;
			_storekeeperStorage = storekeeperStorage;
		}

		public List<StorekeeperViewModel>? ReadList(StorekeeperSearchModel? model)
		{
			_logger.LogInformation("ReadList. StorekeeperId:Id:{ Id}", model?.Id);
			var list = model == null ? _storekeeperStorage.GetFullList() : _storekeeperStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}

		public StorekeeperViewModel? ReadElement(StorekeeperSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. StorekeeperId:Id:{ Id}", model.Id);
			var element = _storekeeperStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}

		public bool Create(StorekeeperBindingModel model)
		{
			CheckModel(model);
			if (_storekeeperStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}

		public bool Update(StorekeeperBindingModel model)
		{
			CheckModel(model);
			if (_storekeeperStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}

		public bool Delete(StorekeeperBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_storekeeperStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}

		private void CheckModel(StorekeeperBindingModel model, bool withParams = true)
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
            var elem = _storekeeperStorage.GetElement(new StorekeeperSearchModel
            {
                Email = model.Email
            });
            if (elem != null && model.Id != elem.Id)
            {
                throw new InvalidOperationException("Такая почта уже используется в системе");
            }
            _logger.LogInformation("Storekeeper. Storekeeper:Id:{ Id}.PhoneNumber:{ PhoneNumber}", model.Id, model.PhoneNumber);
		}
	}
}
