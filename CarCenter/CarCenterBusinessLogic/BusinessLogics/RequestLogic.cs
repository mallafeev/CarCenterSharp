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
	public class RequestLogic : IRequestLogic
	{
		private readonly ILogger _logger;
		private readonly IRequestStorage _requestStorage;
		public RequestLogic(ILogger<RequestLogic> logger, IRequestStorage requestStorage)
		{
			_logger = logger;
			_requestStorage = requestStorage;
		}

		public List<RequestViewModel>? ReadList(RequestSearchModel? model)
		{
			_logger.LogInformation("ReadList. RequestId:Id:{ Id}", model?.Id);
			var list = model == null ? _requestStorage.GetFullList() : _requestStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}

		public RequestViewModel? ReadElement(RequestSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. RequestId:Id:{ Id}", model.Id);
			var element = _requestStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}

		public bool Create(RequestBindingModel model)
		{
			CheckModel(model);
			if (_requestStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}

		public bool Update(RequestBindingModel model)
		{
			CheckModel(model);
			if (_requestStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}

		public bool Delete(RequestBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_requestStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}

		private void CheckModel(RequestBindingModel model, bool withParams = true)
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
			_logger.LogInformation("Request. Request:Id:{ Id}.Description:{ Description}", model.Id, model.Description);
		}
	}
}
