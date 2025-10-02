using CarCenterContracts.BindingModels;
using CarCenterContracts.ViewModels;
using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDatabaseImplement.Models
{
	public class Order : IOrderModel
	{
		public int Id { get; set; }
		public int WorkerId { get; set; }
		[Required]
		public PaymentType PaymentType { get; set; } = PaymentType.Неизвестно;
		[Required]
		public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Неизвестно;
		[Required]
		public string BuyerFCS { get; set; } = string.Empty;
		public DateTime PaymentDate { get; set; }
		[Required]
		public double Sum { get; set; }
		public virtual Worker Worker { get; set; }
		[ForeignKey("OrderId")]
		public virtual List<Car> Cars { get; set; } = new();
		private Dictionary<int, IPresaleModel>? _orderPresales = null;
		[ForeignKey("OrderId")]
		public virtual List<OrderPresale> Presales { get; set; } = new();
		[NotMapped]
		public Dictionary<int, IPresaleModel> OrderPresales
		{
			get
			{
				if(_orderPresales == null)
				{
					_orderPresales = Presales
                         .GroupBy(recPc => recPc.PresaleId)
						.ToDictionary(g => g.Key, g => g.First().Presale as IPresaleModel);
				}
				return _orderPresales;
			}
		}
		public static Order? Create(CarCenterDatabase context, OrderBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			var order = new Order()
			{
				Id = model.Id,
				WorkerId = model.WorkerId,
				PaymentType = model.PaymentType,
				PaymentStatus = model.PaymentStatus,
				BuyerFCS = model.BuyerFCS,
				PaymentDate = model.PaymentDate,
				Sum = model.Sum,
				Presales = model.OrderPresales.Select(x => new OrderPresale
				{
					Presale = context.Presales.First(y => y.Id == x.Value.Id)
				}).ToList()
			};

            foreach (var car in model.Cars)
            {
                var cartmp = context.Cars.FirstOrDefault(x => x.Id == car.Value.Id);
                if (cartmp != null)
                {
                    order.Cars.Add(cartmp);
                }
            }

            return order;
		}

		public void Update(OrderBindingModel? model)
		{
			if (model == null)
			{
				return;
			}
			WorkerId = model.WorkerId;
			PaymentDate = model.PaymentDate;
			PaymentType = model.PaymentType;
			BuyerFCS = model.BuyerFCS;
			Sum = model.Sum;
		}
		public void UpdatePresales(CarCenterDatabase context, OrderBindingModel model)
		{
			var order = context.Orders.First(x => x.Id == Id);
            var existingPresale = context.OrderPresales.Where(pb => pb.OrderId == order.Id).ToList();
            context.OrderPresales.RemoveRange(existingPresale);
            context.SaveChanges();
            foreach (var pc in model.OrderPresales)
			{
				var tmp = new OrderPresale
				{
					Order = order,
					Presale = context.Presales.First(x => x.Id == pc.Value.Id),
				};
				if (context.OrderPresales.Contains(tmp))
				{
					continue;
				}
				context.OrderPresales.Add(tmp);
				context.SaveChanges();
			}
			_orderPresales = null;
		}
        public void UpdateCars(CarCenterDatabase context, OrderBindingModel model)
        {
            var order = context.Orders.First(x => x.Id == Id);
            order.Cars.Clear();
            foreach (var car in model.Cars)
            {
                var cartmp = context.Cars.FirstOrDefault(x => x.Id == car.Value.Id);
                if (cartmp != null)
                {
					if (order.Cars.Contains(cartmp))
					{
						continue;
					}
                    order.Cars.Add(cartmp);
                }
            }
            context.SaveChanges();
        }
        public OrderViewModel GetViewModel => new()
		{
			Id = Id,
			WorkerId = WorkerId,
			WorkerName = Worker?.Name ?? string.Empty,
			PaymentType = PaymentType,
			PaymentStatus = PaymentStatus,
			BuyerFCS = BuyerFCS,
			PaymentDate = PaymentDate,
			Sum = Sum,
			OrderPresales = OrderPresales,
			Cars = Cars.ToDictionary(x => x.Id, x => x as ICarModel),
        };
	}
}
