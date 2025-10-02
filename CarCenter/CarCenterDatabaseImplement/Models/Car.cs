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
	public class Car : ICarModel
	{
		public int Id { get; private set; }
		public int StorekeeperId { get; set; }
		public int? OrderId { get; set; }
		public int? FeatureId { get; set; }
		[Required]
		public CarBrand CarBrand { get; set; } = CarBrand.Неизвестно;
		[Required]
		public string Model { get; set; } = string.Empty;
		[Required]
		public CarClass CarClass { get; set; } = CarClass.Неизвестно;
		[Required]
		public int Year { get; set; }
		[Required]
		public double Price { get; set; }
		[Required]
		public long VINnumber { get; set; }
        [Required]
        public DateTime DateCreate { get; set; } = DateTime.Now;
        [Required]
		public int FeatureID { get; set; }
		public virtual Storekeeper Storekeeper { get; set; }
		public virtual Feature Feature { get; set; }
		public virtual Order? Order {  get; set; }

		private Dictionary<int, IBundlingModel>? _carBundlings = null;
		[ForeignKey("CarId")]
		public virtual List<CarBundling> Bundlings { get; set; } = new();
		[NotMapped]
		public Dictionary<int, IBundlingModel> CarBundlings
		{
			get
			{
				if (_carBundlings == null)
				{
					_carBundlings = Bundlings.ToDictionary(recPc => recPc.BundlingId, recPc => recPc.Bundling as IBundlingModel);
				}
				return _carBundlings;
			}
		}
		public static Car? Create(CarCenterDatabase context, CarBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			return new Car()
			{
				Id = model.Id,
				StorekeeperId = model.StorekeeperId,
				FeatureId = model.FeatureID,
				OrderId = model.OrderId,
				CarBrand = model.CarBrand,
				Model = model.Model,
				CarClass = model.CarClass,
				Year = model.Year,
				Price = model.Price,
				VINnumber = model.VINnumber,
				FeatureID = model.FeatureID,
                DateCreate = model.DateCreate,

                Bundlings = model.CarBundlings.Select(x => new CarBundling
				{
					Bundling = context.Bundlings.First(y => y.Id == x.Key)
				}).ToList()
			};
		}

		public void UpdateBundlings(CarCenterDatabase context, CarBindingModel model)
		{
			var car = context.Cars.First(x => x.Id == Id);
            var existingBundling = context.CarBundlings.Where(pb => pb.CarId == car.Id).ToList();
            context.CarBundlings.RemoveRange(existingBundling);
            context.SaveChanges();
            foreach (var pc in model.CarBundlings)
			{
				context.CarBundlings.Add(new CarBundling
				{
					Car = car,
					Bundling = context.Bundlings.First(x => x.Id == pc.Key),
				});
				context.SaveChanges();
			}
			_carBundlings = null;
		}

		public void Update(CarBindingModel model)
		{
			if (model == null)
			{
				return;
			}
			StorekeeperId = model.StorekeeperId;
			FeatureId = model.FeatureID;
			OrderId = model.OrderId;
			CarBrand = model.CarBrand;
			Model = model.Model;
			CarClass = model.CarClass;
			Year = model.Year;
			Price = model.Price;
			DateCreate = model.DateCreate;
			VINnumber = model.VINnumber;
			FeatureID = model.FeatureID;
		}
		public CarViewModel GetViewModel => new()
		{
			Id = Id,
			StorekeeperId = StorekeeperId,
			OrderId = OrderId,
			BuyerFCS = Order?.BuyerFCS ?? string.Empty,
			CarBrand = CarBrand,
			Model = Model,
			CarClass = CarClass,
			Year = Year,
			Price = Price,
			VINnumber = VINnumber,
			FeatureID = FeatureID,
			CarBundlings = CarBundlings,
			DateCreate = DateCreate,
		};
	}
}
