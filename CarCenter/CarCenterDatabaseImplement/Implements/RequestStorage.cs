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
	public class RequestStorage : IRequestStorage
	{
		public RequestViewModel? Delete(RequestBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var element = context.Requests
				.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				var deletedElement = context.Requests
					.Include(x => x.Presale)
					.FirstOrDefault(x => x.Id == model.Id)
					?.GetViewModel;
				context.Requests.Remove(element);
				context.SaveChanges();
				return deletedElement;
			}
			return null;
		}

		public RequestViewModel? GetElement(RequestSearchModel model)
		{
			using var context = new CarCenterDatabase();
			if (!model.Id.HasValue)
			{
				return null;
			}
			return context.Requests
				.Include(x => x.Presale)
				.FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
				?.GetViewModel;
		}

		public List<RequestViewModel> GetFilteredList(RequestSearchModel model)
		{
			using var context = new CarCenterDatabase();
			if (model.Id.HasValue)
			{
				return context.Requests
					.Include(x => x.Presale)
					.Where(x => x.Id == model.Id)
					.Select(x => x.GetViewModel)
					.ToList();
			}
			else if (model.WorkerId.HasValue)
			{
                return context.Requests
                    .Include(x => x.Presale)
                    .Where(x => x.WorkerId == model.WorkerId)
                    .Select(x => x.GetViewModel)
                    .ToList();
            }
			return new();
		}

		public List<RequestViewModel> GetFullList()
		{
			using var context = new CarCenterDatabase();
			return context.Requests
					.Include(x => x.Presale)
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public RequestViewModel? Insert(RequestBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var newRequest = Request.Create(model);
			if (newRequest == null)
			{
				return null;
			}
			context.Requests.Add(newRequest);
			context.SaveChanges();
			return context.Requests
				   .Include(x => x.Presale)
				   .FirstOrDefault(x => x.Id == newRequest.Id)
				   ?.GetViewModel;
		}

		public RequestViewModel? Update(RequestBindingModel model)
		{
			using var context = new CarCenterDatabase();
			var order = context.Requests.FirstOrDefault(x => x.Id == model.Id);
			if (order == null)
			{
				return null;
			}
			order.Update(model);
			context.SaveChanges();
			return context.Requests
				   .Include(x => x.Presale)
				   .FirstOrDefault(x => x.Id == model.Id)
				   ?.GetViewModel;
		}
	}
}
