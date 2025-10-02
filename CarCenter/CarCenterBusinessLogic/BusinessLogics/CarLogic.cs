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
	public class CarLogic : ICarLogic
	{
		private readonly ILogger _logger;
		private readonly ICarStorage _carStorage;
		public CarLogic(ILogger<CarLogic> logger, ICarStorage carStorage)
		{
			_logger = logger;
			_carStorage = carStorage;
		}

		public List<CarViewModel>? ReadList(CarSearchModel? model)
		{
			_logger.LogInformation("ReadList. VINnumber:{VINnumber}.Id:{ Id}", model?.VINnumber, model?.Id);
			var list = model == null ? _carStorage.GetFullList() : _carStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}

		public CarViewModel? ReadElement(CarSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. VINnumber:{VINnumber}.Id:{ Id}", model?.VINnumber, model?.Id);
			var element = _carStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}

		public bool Create(CarBindingModel model)
		{
			CheckModel(model);
			if (_carStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}

		public bool Update(CarBindingModel model)
		{
			CheckModel(model);
			if (_carStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}

		public bool Delete(CarBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_carStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}

		private void CheckModel(CarBindingModel model, bool withParams = true)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			if (!withParams)
			{
				return;
			}
			if (model.VINnumber <= 0)
			{
				throw new ArgumentNullException("Нет VIN-номера", nameof(model.VINnumber));
		    }
			_logger.LogInformation("Car. VINnumber:{VINnumber}. Id: { Id}", model.VINnumber, model.Id);
			var element = _carStorage.GetElement(new CarSearchModel
			{
				VINnumber = model.VINnumber,
			});
			if (element != null && element.Id != model.Id)
			{
				throw new InvalidOperationException("Компонент с таким VIN-номером уже есть");
		    }
		}
	}
}
