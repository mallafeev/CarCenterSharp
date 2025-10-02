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
	public class StorekeeperStorage : IStorekeeperStorage
	{
		public List<StorekeeperViewModel> GetFullList()
		{
			using var context = new CarCenterDatabase();

			return context.Storekeepers
				.Select(x => x.GetViewModel)
				.ToList();
		}
		public List<StorekeeperViewModel> GetFilteredList(StorekeeperSearchModel model)
		{
			if (!model.Id.HasValue)
			{
				return new();
			}
            using var context = new CarCenterDatabase();
            if (model.Id.HasValue)
			{
                return context.Storekeepers.Where(x => x.Id == model.Id).Select(x => x.GetViewModel).ToList();
            }
            else
            {
				return new();
            }
		}

		public StorekeeperViewModel? GetElement(StorekeeperSearchModel model)
		{
			using var context = new CarCenterDatabase();
            if (!model.Id.HasValue && string.IsNullOrEmpty(model.Email)) { return null; }
			return context.Storekeepers.FirstOrDefault(x => (model.Id.HasValue && x.Id == model.Id)
			|| (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password) && x.Email.Equals(model.Email) && x.Password.Equals(model.Password)))?.GetViewModel; 
		}


		public StorekeeperViewModel? Delete(StorekeeperBindingModel model)
		{
			using var context = new CarCenterDatabase();

			var res = context.Storekeepers
				.FirstOrDefault(x => x.Id == model.Id);

			if (res != null)
			{
				context.Storekeepers.Remove(res);
				context.SaveChanges();
			}

			return res?.GetViewModel;
		}

		public StorekeeperViewModel? Insert(StorekeeperBindingModel model)
		{
			using var context = new CarCenterDatabase();

			var res = Storekeeper.Create(model);

			if (res != null)
			{
				context.Storekeepers.Add(res);
				context.SaveChanges();
			}

			return res?.GetViewModel;
		}

		public StorekeeperViewModel? Update(StorekeeperBindingModel model)
		{
			using var context = new CarCenterDatabase();

			var res = context.Storekeepers.FirstOrDefault(x => x.Id == model.Id);

			if (res != null)
			{
				res.Update(model);
				context.SaveChanges();
			}

			return res?.GetViewModel;
		}
	}
}
