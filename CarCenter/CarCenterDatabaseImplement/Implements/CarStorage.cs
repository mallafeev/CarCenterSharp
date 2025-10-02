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
	public class CarStorage : ICarStorage
	{
		public List<CarViewModel> GetFullList()
		{
			using var context = new CarCenterDatabase();
			return context.Cars
					.Include(x => x.Bundlings)
					.ThenInclude(x => x.Bundling)
					.Include(x => x.Feature)
					.Include(x => x.Order)
					.Include(x => x.Storekeeper)
					.Select(x => x.GetViewModel)
					.ToList();
		}
		public List<CarViewModel> GetFilteredList(CarSearchModel model)
		{
			using var context = new CarCenterDatabase();
            if (model.DateFrom.HasValue) // Для отчета списка
            {
                //будет применятся в ReportLogic
                return context.Cars
					.Where(x => x.StorekeeperId == model.StorekeeperId)
					.Where(x => x.DateCreate <= model.DateTo && x.DateCreate >= model.DateFrom)
					.Select(x => x.GetViewModel)
					.ToList();
            }
            else if (model.StorekeeperId.HasValue)
			{
				return context.Cars
                    .Where(x => x.StorekeeperId == model.StorekeeperId)
                    .Include(x => x.Bundlings)
					.ThenInclude(x => x.Bundling)
					.Include(x => x.Feature)
					.Include(x => x.Order)
					.Include(x => x.Storekeeper)
					.Select(x => x.GetViewModel)
					.ToList();
			}
			else if (model.FeatureId.HasValue)
			{
				return context.Cars
					.Include(x => x.Bundlings)
					.ThenInclude(x => x.Bundling)
					.Include(x => x.Feature)
					.Where(x => x.FeatureID == model.FeatureId && x.OrderId == model.OrderId)
					.Include(x => x.Order)
					.Include(x => x.Storekeeper)
					.Select(x => x.GetViewModel)
					.ToList();

            }
			return new();
		}
		public CarViewModel? GetElement(CarSearchModel model)
		{
			using var context = new CarCenterDatabase();
			if (!model.Id.HasValue)
			{
				return null;
			}
			return context.Cars
				.Include(x => x.Bundlings) // здесь эксперименты))
				.ThenInclude(x => x.Bundling)
				.Include(x => x.Feature)
				.Include(x => x.Order)
				.Include(x => x.Storekeeper)
				.FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
				?.GetViewModel;
		}
		public CarViewModel? Insert(CarBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var newCar = Car.Create(context, model);
			if (newCar == null)
			{
				return null;
			}
			context.Cars.Add(newCar);
			context.SaveChanges();
			return context.Cars
				   .Include(x => x.Bundlings)
					.ThenInclude(x => x.Bundling)
					.Include(x => x.Feature)
					.Include(x => x.Order)
					.Include(x => x.Storekeeper)
				   .FirstOrDefault(x => x.Id == newCar.Id)
				   ?.GetViewModel;
		}
		public CarViewModel? Update(CarBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var car = context.Cars.FirstOrDefault(x => x.Id == model.Id);
			if (car == null)
			{
				return null;
			}
			car.Update(model);
			car.UpdateBundlings(context, model);
			context.SaveChanges();
			return context.Cars
				   .Include(x => x.Bundlings)
					.ThenInclude(x => x.Bundling)
					.Include(x => x.Feature)
					.Include(x => x.Order)
					.Include(x => x.Storekeeper)
				   .FirstOrDefault(x => x.Id == model.Id)
				   ?.GetViewModel;
		}
		public CarViewModel? Delete(CarBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var element = context.Cars
				.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				var deletedElement = context.Cars
					.Include(x => x.Bundlings)
					.ThenInclude(x => x.Bundling)
					.Include(x => x.Feature)
					.Include(x => x.Order)
					.Include(x => x.Storekeeper)
					.FirstOrDefault(x => x.Id == model.Id)
					?.GetViewModel;
				context.Cars.Remove(element);
				context.SaveChanges();
				return deletedElement;
			}
			return null;
		}
	}
}
