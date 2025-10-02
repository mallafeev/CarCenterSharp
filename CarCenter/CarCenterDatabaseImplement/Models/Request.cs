using CarCenterContracts.BindingModels;
using CarCenterContracts.ViewModels;
using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDatabaseImplement.Models
{
	public class Request : IRequestModel
	{
		public int Id { get; set; }
		public int WorkerId { get; set; }
		public int? PresaleId { get; set; }
		[Required]
		public string Description { get; set; } = string.Empty;
		[Required]
		public RequestTypes RequestType { get; set; } = RequestTypes.Неизвестно;
		public virtual Presale? Presale { get; set; }

		public static Request? Create(RequestBindingModel? model)
		{
			if (model == null)
			{
				return null;
			}
			return new Request()
			{
				Id = model.Id,
				PresaleId = model.PresaleId,
				Description = model.Description,
				RequestType = model.RequestType,
				WorkerId = model.WorkerId,
			};
		}

		public void Update(RequestBindingModel? model)
		{
			if (model == null)
			{
				return;
			}
			Description = model.Description;
			RequestType = model.RequestType;
			PresaleId = model.PresaleId;
		}

		public RequestViewModel GetViewModel => new()
		{
			Id = Id,
			Description = Description,
			RequestType = RequestType,
			PresaleId = PresaleId,
		};
	}
}
