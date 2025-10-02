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
	public class OrderLogic : IOrderLogic
	{
		private readonly ILogger _logger;
		private readonly IOrderStorage _orderStorage;
		public OrderLogic(ILogger<OrderLogic> logger, IOrderStorage orderStorage)
		{
			_logger = logger;
			_orderStorage = orderStorage;
		}

		public List<OrderViewModel>? ReadList(OrderSearchModel? model)
		{
			_logger.LogInformation("ReadList. OrderId:Id:{ Id}", model?.Id);
			var list = model == null ? _orderStorage.GetFullList() : _orderStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}

		public OrderViewModel? ReadElement(OrderSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. OrderId:Id:{ Id}", model.Id);
			var element = _orderStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}

		public bool Create(OrderBindingModel model)
		{
			CheckModel(model);
			if (_orderStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}

		public bool Update(OrderBindingModel model)
		{
			CheckModel(model);
			if (_orderStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}

		public bool Delete(OrderBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_orderStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}

		private void CheckModel(OrderBindingModel model, bool withParams = true)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			if (!withParams)
			{
				return;
			}
			if (model.BuyerFCS == string.Empty)
			{
				throw new ArgumentNullException("Нет покупателя", nameof(model.BuyerFCS));
			}
			if(model.Sum < 0)
			{
				throw new ArgumentOutOfRangeException("Сумма меньше нуля",nameof(model.Sum));
			}
			_logger.LogInformation("Order. Order:Id:{ Id}.Sum:{ Sum}", model.Id, model.Sum);
		}
	}
}
