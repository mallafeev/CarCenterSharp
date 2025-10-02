using CarCenterContracts.BindingModels;
using CarCenterContracts.ViewModels;
using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDatabaseImplement.Models
{
	public class Feature : IFeatureModel
	{
		public int Id { get; private set; }
        public int StorekeeperId { get; set; }
        [Required]
		public HelpDevices HelpDevice { get; set; } = HelpDevices.Неизвестно;
		[Required]
		public string CabinColor { get; set; } = string.Empty;
		[Required]
		public DriveTypes DriveType { get; set; } = DriveTypes.Неизвестно;
		[Required]
		public double Price { get; set; }
		[ForeignKey("FeatureId")]
		public virtual List<Car> Cars { get; set; } = new();

		public static Feature? Create(FeatureBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			return new Feature()
			{
				Id = model.Id,
				HelpDevice = model.HelpDevice,
				CabinColor = model.CabinColor,
				DriveType = model.DriveType,
				Price = model.Price,
				StorekeeperId = model.StorekeeperId,
			};
		}
		public static Feature Create(FeatureViewModel model)
		{
			return new Feature
			{
				Id = model.Id,
				StorekeeperId = model.StorekeeperId,
				HelpDevice = model.HelpDevice,
				CabinColor = model.CabinColor,
				DriveType = model.DriveType,
				Price = model.Price,
			};
		}
		public void Update(FeatureBindingModel model)
		{
			if (model == null)
			{
				return;
			}
			StorekeeperId = model.StorekeeperId;
			HelpDevice = model.HelpDevice;
			CabinColor = model.CabinColor;
			DriveType = model.DriveType;
			Price = model.Price;
		}
		public FeatureViewModel GetViewModel => new()
		{
			Id = Id,
			StorekeeperId = StorekeeperId,
			HelpDevice = HelpDevice,
			CabinColor = CabinColor,
			DriveType = DriveType,
			Price = Price,
		};
	}
}
