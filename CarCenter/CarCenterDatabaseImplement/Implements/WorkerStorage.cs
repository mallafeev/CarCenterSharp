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
	public class WorkerStorage : IWorkerStorage
	{
		public List<WorkerViewModel> GetFullList()
		{
			using var context = new CarCenterDatabase();

			return context.Workers
				.Select(x => x.GetViewModel)
				.ToList();
		}
		public List<WorkerViewModel> GetFilteredList(WorkerSearchModel model)
		{
            if (!model.Id.HasValue)
            {
                return new();
            }
            using var context = new CarCenterDatabase();
            if (model.Id.HasValue)
            {
                return context.Workers.Where(x => x.Id == model.Id).Select(x => x.GetViewModel).ToList();
            }
            else
            {
                return new();
            }
        }

		public WorkerViewModel? GetElement(WorkerSearchModel model)
		{
			using var context = new CarCenterDatabase();
			if (!model.Id.HasValue && string.IsNullOrEmpty(model.Email)) { return null; }
			return context.Workers.FirstOrDefault(x => (model.Id.HasValue && x.Id == model.Id)
			|| (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password) && x.Email.Equals(model.Email) && x.Password.Equals(model.Password)))?.GetViewModel;
		}


		public WorkerViewModel? Delete(WorkerBindingModel model)
		{
			using var context = new CarCenterDatabase();

			var res = context.Workers
				.FirstOrDefault(x => x.Id == model.Id);

			if (res != null)
			{
				context.Workers.Remove(res);
				context.SaveChanges();
			}

			return res?.GetViewModel;
		}

		public WorkerViewModel? Insert(WorkerBindingModel model)
		{
			using var context = new CarCenterDatabase();

			var res = Worker.Create(model);

			if (res != null)
			{
				context.Workers.Add(res);
				context.SaveChanges();
			}

			return res?.GetViewModel;
		}

		public WorkerViewModel? Update(WorkerBindingModel model)
		{
			using var context = new CarCenterDatabase();

			var res = context.Workers.FirstOrDefault(x => x.Id == model.Id);

			if (res != null)
			{
				res.Update(model);
				context.SaveChanges();
			}

			return res?.GetViewModel;
		}
	}
}
