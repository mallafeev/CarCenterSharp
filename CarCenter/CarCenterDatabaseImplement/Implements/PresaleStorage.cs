using CarCenterContracts.BindingModels;
using CarCenterContracts.SearchModels;
using CarCenterContracts.StoragesContracts;
using CarCenterContracts.ViewModels;
using CarCenterDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDatabaseImplement.Implements
{
	public class PresaleStorage : IPresaleStorage
	{
		public PresaleViewModel? Delete(PresaleBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var element = context.Presales
				.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				var deletedElement = context.Presales
					.Include(x => x.Bundlings)
                    .Include(x => x.Requests)
                    .FirstOrDefault(x => x.Id == model.Id)
					?.GetViewModel;
				context.Presales.Remove(element);
				context.SaveChanges();
				return deletedElement;
			}
			return null;
		}

		public PresaleViewModel? GetElement(PresaleSearchModel model)
		{
			using var context = new CarCenterDatabase();
			if (!model.Id.HasValue)
			{
				return null;
			}
			return context.Presales
				.Include(x => x.Bundlings)
                .Include(x => x.Requests)
                .FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
				?.GetViewModel;
		}

		public List<PresaleViewModel> GetFilteredList(PresaleSearchModel model)
		{
			using var context = new CarCenterDatabase();
			if (model.OrderId.HasValue)
			{
                return context.Presales
                    .Include(x => x.Bundlings)
					.ThenInclude(x => x.Bundling)
                    .Include(x => x.Requests)
					.Include(x => x.OrderPresales.Where(y => y.OrderId == model.OrderId))
                    .Select(x => x.GetViewModel)
                    .ToList();
            }
			else if (model.WorkerId.HasValue)
			{
				return context.Presales
					.Include(x => x.Bundlings)
                    .ThenInclude(x => x.Bundling)
                    .Include(x => x.Requests)
					.Where(x => x.WorkerId == model.WorkerId)
					.Select(x => x.GetViewModel)
					.ToList();
			}
			return new();
		}

		public List<PresaleViewModel> GetFullList()
		{
			using var context = new CarCenterDatabase();
			return context.Presales
					.Include(x => x.Bundlings)
                    .ThenInclude(x => x.Bundling)
                    .Include(x => x.Requests)
                    .Select(x => x.GetViewModel)
					.ToList();
		}

		public PresaleViewModel? Insert(PresaleBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var newPresale = Presale.Create(context, model);
			if (newPresale == null)
			{
				return null;
			}
			context.Presales.Add(newPresale);
			context.SaveChanges();
			return context.Presales
				   .Include(x => x.Bundlings)
                   .ThenInclude(x => x.Bundling)
                    .Include(x => x.Requests)
                   .FirstOrDefault(x => x.Id == newPresale.Id)
				   ?.GetViewModel;
		}

		public PresaleViewModel? Update(PresaleBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var order = context.Presales.FirstOrDefault(x => x.Id == model.Id);
			if (order == null)
			{
				return null;
			}
			order.Update(model);
			order.UpdateBundlings(context, model);
			order.UpdateRequests(context, model);
			context.SaveChanges();
			return context.Presales
				   .Include(x => x.Bundlings)
                   .ThenInclude(x => x.Bundling)
                    .Include(x => x.Requests)
                   .FirstOrDefault(x => x.Id == model.Id)
				   ?.GetViewModel;
		}
	}
}
