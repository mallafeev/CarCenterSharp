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
	public class BundlingStorage : IBundlingStorage
	{
		public List<BundlingViewModel> GetFullList()
		{
			using var context = new CarCenterDatabase();
			return context.Bundlings
                 .Include(x => x.PresaleBundling)
                .ThenInclude(x => x.Presale)
                    .Select(x => x.GetViewModel)
					.ToList();
		}
		public List<BundlingViewModel> GetFilteredList(BundlingSearchModel model)
		{
            if (!model.StorekeeperId.HasValue && !model.DateFrom.HasValue && !model.DateTo.HasValue)
            {
                return new();
            }
            using var context = new CarCenterDatabase();
            if (model.DateFrom.HasValue)
                return context.Bundlings
                    .Include(x => x.PresaleBundling)
                .ThenInclude(x => x.Presale)
                    .Where(x => x.StorekeeperId == model.StorekeeperId).Where(x => x.DateCreate <= model.DateTo && x.DateCreate >= model.DateFrom).Select(x => x.GetViewModel).ToList();
            else
                return context.Bundlings
                     .Include(x => x.PresaleBundling)
                .ThenInclude(x => x.Presale)
                    .Where(x => x.StorekeeperId == model.StorekeeperId).Select(x => x.GetViewModel).ToList();
		}
		public BundlingViewModel? GetElement(BundlingSearchModel model)
		{
			using var context = new CarCenterDatabase();
			if (!model.Id.HasValue)
			{
				return null;
			}
			return context.Bundlings
                .Include(x => x.PresaleBundling)
                .ThenInclude(x => x.Presale)
                .FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
				?.GetViewModel;
		}
		public BundlingViewModel? Insert(BundlingBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var newBundling = Bundling.Create(context, model);
			if (newBundling == null)
			{
				return null;
			}
			context.Bundlings.Add(newBundling);
			context.SaveChanges();
			return context.Bundlings
                .Include(x => x.PresaleBundling)
                .ThenInclude(x => x.Presale)
                   .FirstOrDefault(x => x.Id == newBundling.Id)
				   ?.GetViewModel;
		}
		public BundlingViewModel? Update(BundlingBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var order = context.Bundlings.FirstOrDefault(x => x.Id == model.Id);
			if (order == null)
			{
				return null;
			}
			order.Update(model);
			order.UpdatePresales(context, model);
			context.SaveChanges();
			return context.Bundlings
				.Include(x => x.PresaleBundling)
				.ThenInclude(x => x.Presale)
				   .FirstOrDefault(x => x.Id == model.Id)
				   ?.GetViewModel;
		}
		public BundlingViewModel? Delete(BundlingBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var element = context.Bundlings
                .Include(x => x.PresaleBundling)
                .ThenInclude(x => x.Presale)
                .FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				var deletedElement = context.Bundlings
                       .Include(x => x.PresaleBundling)
                .ThenInclude(x => x.Presale)
                    .FirstOrDefault(x => x.Id == model.Id)
					?.GetViewModel;
				context.Bundlings.Remove(element);
				context.SaveChanges();
				return deletedElement;
			}
			return null;
		}
	}
}
