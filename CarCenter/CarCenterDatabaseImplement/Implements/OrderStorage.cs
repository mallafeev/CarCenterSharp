using CarCenterContracts.BindingModels;
using CarCenterContracts.SearchModels;
using CarCenterContracts.StoragesContracts;
using CarCenterContracts.ViewModels;
using CarCenterDatabaseImplement.Models;
using CarCenterDataModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDatabaseImplement.Implements
{
	public class OrderStorage : IOrderStorage
	{
		public OrderViewModel? Delete(OrderBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var element = context.Orders
				.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				var deletedElement = context.Orders
					.Include(x => x.Worker)
					.Include(x => x.Cars)
					.Include(x => x.Presales)
					.ThenInclude(x => x.Presale)
					.FirstOrDefault(x => x.Id == model.Id)
					?.GetViewModel;
				context.Orders.Remove(element);
				context.SaveChanges();
				return deletedElement;
			}
			return null;
		}

		public OrderViewModel? GetElement(OrderSearchModel model)
		{
			using var context = new CarCenterDatabase();
			if (!model.Id.HasValue)
			{
				return null;
			}
			return context.Orders
				.Include(x => x.Worker)
				.Include(x => x.Cars)//рубрика ЭЭЭЭЭЭЭЭЭКсперименты
				.Include(x => x.Presales)
				.ThenInclude(x => x.Presale)
				.FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
				?.GetViewModel;
		}

		public List<OrderViewModel> GetFilteredList(OrderSearchModel model)
		{
			using var context = new CarCenterDatabase();
            
            if (model.DateFrom.HasValue && model.DateTo.HasValue) //для списка Сагиров
            {
				if(model.WorkerId.HasValue)
				{
                    return context.Orders
                   .Include(x => x.Worker)
                   .Where(x => x.WorkerId == model.WorkerId)
                   .Include(x => x.Cars)
                   .Include(x => x.Presales)
                   .ThenInclude(x => x.Presale)
                   .ThenInclude(x => x.Requests)
                   .Include(x => x.Presales)
                   //.Where(x => x.PaymentDate >= model.DateFrom && x.PaymentDate <= model.DateTo)
                   .Select(x => x.GetViewModel)
                   .ToList();
                }
				else
				{
                    return context.Orders
                   .Include(x => x.Worker)
                   //.Where(x => x.WorkerId == model.WorkerId)
                   .Include(x => x.Cars)
				   .ThenInclude(y => y.Bundlings.Where(b => b.BundlingId == model.BundlingId))
                   .Include(x => x.Presales)
                   .ThenInclude(x => x.Presale)
                   .ThenInclude(x => x.Requests)
                   .Include(x => x.Presales)
                   //.Where(x => x.PaymentDate >= model.DateFrom && x.PaymentDate <= model.DateTo)
                   .Select(x => x.GetViewModel)
                   .ToList();
                }
               
            }
            else if (model.Presales.Count > 0) //для отчета Сагиров
            {
                //будет применятся в ReportLogic
                return context.Orders
                    .Where(x => x.WorkerId == model.WorkerId)
                    .Include(x => x.Worker)
                    .Include(x => x.Cars)
                    .Include(x => x.Presales)
                    .ThenInclude(x => x.Presale)
                    .Where(x => x.OrderPresales.Any(p => model.Presales.Any(presale => p.Value == presale)))
                    .Select(x => x.GetViewModel)
                    .ToList();
            }
            else if (model.Cars.Count > 0) //для отчета Малафеев
            {
                //будет применятся в ReportLogic
                return context.Orders
                    .Where(x => x.WorkerId == model.WorkerId)
                    .Include(x => x.Worker)
                    .Include(x => x.Cars)
                    .Include(x => x.Presales)
                    .ThenInclude(x => x.Presale)
                    .Where(x => x.Cars.Any(car => model.Cars.Any(searchCar => car.Id == searchCar.Id)))
                    .Select(x => x.GetViewModel)
                    .ToList();
            }
            else if (model.WorkerId.HasValue)
			{
				return context.Orders
                    .Where(x => x.WorkerId == model.WorkerId)
                    .Include(x => x.Worker)
					.Include(x => x.Cars)
					.Include(x => x.Presales)
					.ThenInclude(x => x.Presale)
					.Select(x => x.GetViewModel)
					.ToList();
			}
			return new();
		}

		public List<OrderViewModel> GetFullList()
		{
			using var context = new CarCenterDatabase();
			return context.Orders
					.Include(x => x.Worker)
					.Include(x => x.Cars)
					.Include(x => x.Presales)
					.ThenInclude(x => x.Presale)
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public OrderViewModel? Insert(OrderBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var newOrder = Order.Create(context,model);
			if (newOrder == null)
			{
				return null;
			}
			context.Orders.Add(newOrder);
			context.SaveChanges();
			return context.Orders
				   .Include(x => x.Worker)
				   .Include(x => x.Presales)
					.ThenInclude(x => x.Presale)
				   .FirstOrDefault(x => x.Id == newOrder.Id)
				   ?.GetViewModel;
		}

		public OrderViewModel? Update(OrderBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var order = context.Orders.FirstOrDefault(x => x.Id == model.Id);
			if (order == null)
			{
				return null;
			}
			order.Update(model);
			order.UpdateCars(context, model);
			order.UpdatePresales(context, model);
			context.SaveChanges();
			return context.Orders
				   .Include(x => x.Worker)
				   .Include(x => x.Cars)
				   .Include(x => x.Presales)
					.ThenInclude(x => x.Presale)
				   .FirstOrDefault(x => x.Id == model.Id)
				   ?.GetViewModel;
		}
	}
}
