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
	public class FeatureStorage : IFeatureStorage
	{
		public List<FeatureViewModel> GetFullList()
		{
			using var context = new CarCenterDatabase();
			return context.Features
					.Select(x => x.GetViewModel)
					.ToList();
		}
		public List<FeatureViewModel> GetFilteredList(FeatureSearchModel model)
		{
			using var context = new CarCenterDatabase();
			if (model.StorekeeperId.HasValue)
			{
				return context.Features
                    .Where(x => x.StorekeeperId == model.StorekeeperId)
					.Select(x => x.GetViewModel)
					.ToList();
			}
			return new();
		}
		public FeatureViewModel? GetElement(FeatureSearchModel model)
		{
			using var context = new CarCenterDatabase();
			if (!model.Id.HasValue)
			{
				return null;
			}
			return context.Features
				.FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
				?.GetViewModel;
		}
		public FeatureViewModel? Insert(FeatureBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var newFeature = Feature.Create(model);
			if (newFeature == null)
			{
				return null;
			}
			context.Features.Add(newFeature);
			context.SaveChanges();
			return context.Features
				   .FirstOrDefault(x => x.Id == newFeature.Id)
				   ?.GetViewModel;
		}
		public FeatureViewModel? Update(FeatureBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var order = context.Features.FirstOrDefault(x => x.Id == model.Id);
			if (order == null)
			{
				return null;
			}
			order.Update(model);
			context.SaveChanges();
			return context.Features
				   .FirstOrDefault(x => x.Id == model.Id)
				   ?.GetViewModel;
		}
		public FeatureViewModel? Delete(FeatureBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var element = context.Features
				.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				var deletedElement = context.Features
					.FirstOrDefault(x => x.Id == model.Id)
					?.GetViewModel;
				context.Features.Remove(element);
				context.SaveChanges();
				return deletedElement;
			}
			return null;
		}
	}
}
