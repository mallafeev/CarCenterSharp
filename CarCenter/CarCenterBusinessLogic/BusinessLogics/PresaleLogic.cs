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
	public class PresaleLogic : IPresaleLogic
	{
		private readonly ILogger _logger;
		private readonly IPresaleStorage _presaleStorage;
		public PresaleLogic(ILogger<PresaleLogic> logger, IPresaleStorage presaleStorage)
		{
			_logger = logger;
			_presaleStorage = presaleStorage;
		}

		public List<PresaleViewModel>? ReadList(PresaleSearchModel? model)
		{
			_logger.LogInformation("ReadList. PresaleId:Id:{ Id}", model?.Id);
			var list = model == null ? _presaleStorage.GetFullList() : _presaleStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}

		public PresaleViewModel? ReadElement(PresaleSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. PresaleId:Id:{ Id}", model.Id);
			var element = _presaleStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}

		public bool Create(PresaleBindingModel model)
		{
			CheckModel(model);
			if (_presaleStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}

		public bool Update(PresaleBindingModel model)
		{
			CheckModel(model);
			if (_presaleStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}

		public bool Delete(PresaleBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_presaleStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}

		private void CheckModel(PresaleBindingModel model, bool withParams = true)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			if (!withParams)
			{
				return;
			}
			if (model.Description == string.Empty)
			{
				throw new ArgumentNullException("Нет описания", nameof(model.Description));
			}
			if (model.DueTill < DateTime.Now)
			{
				throw new InvalidOperationException("Срок выполнения раньше текущего времени");
			}
			_logger.LogInformation("Presale. Presale:Id:{ Id}.Price:{ Price}", model.Id, model.Description);
		}
	}
}
