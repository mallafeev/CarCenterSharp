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
	public class Presale : IPresaleModel
	{
		public int Id { get; set; }
		public int WorkerId { get; set; }
		[Required]
		public PresaleStatus PresaleStatus { get; set; } = PresaleStatus.Неизвестно;
		[Required]
		public string Description { get; set; } = string.Empty;
		[Required]
		public DateTime DueTill { get; set; }
		[Required]
		public double Price { get; set; }
		[ForeignKey("PresaleId")]
		public virtual List<Request>? Requests { get; set; } = new();
		[ForeignKey("PresaleId")]
		public virtual List<OrderPresale> OrderPresales { get; set; } = new();

		private Dictionary<int, IBundlingModel>? _presaleBundlings = null;
		[ForeignKey("PresaleId")]
		public virtual List<PresaleBundling>? Bundlings { get; set; } = new();
        [NotMapped]
        public Dictionary<int, IBundlingModel> PresaleBundlings
		{
			get
			{
				if (_presaleBundlings == null)
				{
                    _presaleBundlings = Bundlings.ToDictionary(recPc => recPc.BundlingId, recPc => recPc.Bundling as IBundlingModel);
                }
				return _presaleBundlings;
			}
		}
		public static Presale? Create(CarCenterDatabase context, PresaleBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
            var presale = new Presale()
			{
				Id = model.Id,
				PresaleStatus = model.PresaleStatus,
				Description = model.Description,
				Price = model.Price,
				DueTill = model.DueTill,
				WorkerId = model.WorkerId,
				Bundlings = model.PresaleBundlings.Select(x => new PresaleBundling
				{
					Bundling = context.Bundlings.First(y => y.Id == x.Value.Id)
				}).ToList()
			};

            foreach (var request in model.Requests)
            {
                var requesttmp = context.Requests.FirstOrDefault(x => x.Id == request.Value.Id);
                if (requesttmp != null)
                {
                    presale.Requests.Add(requesttmp);
                }
            }

            return presale;
		}

		public void UpdateRequests(CarCenterDatabase context, PresaleBindingModel model)
		{
			var presale = context.Presales.First(x => x.Id == Id);
            foreach (var request in model.Requests)
            {
                var requesttmp = context.Requests.FirstOrDefault(x => x.Id == request.Value.Id);
                if (requesttmp != null)
                {
					if (presale.Requests.Contains(requesttmp))
					{
						continue;
					}
                    presale.Requests.Add(requesttmp);
                }
            }
        }

        public void UpdateBundlings(CarCenterDatabase context, PresaleBindingModel model)
        {
            var presale = context.Presales.First(x => x.Id == Id);
            var existingBundling = context.PresaleBundlings.Where(pb => pb.PresaleId == presale.Id).ToList();
            context.PresaleBundlings.RemoveRange(existingBundling);
            context.SaveChanges();
            foreach (var pc in model.PresaleBundlings)
            {
                var tmp = new PresaleBundling
                {
                    Presale = presale,
                    Bundling = context.Bundlings.First(x => x.Id == pc.Value.Id),
                };
				if (context.PresaleBundlings.Contains(tmp))
				{
					continue;
				}
				context.PresaleBundlings.Add(tmp);
                context.SaveChanges();
            }
            _presaleBundlings = null;
        }
        public void Update(PresaleBindingModel? model)
		{
			if (model == null)
			{
				return;
			}
			Description = model.Description;
			Price = model.Price;
			DueTill = model.DueTill;
			PresaleStatus = model.PresaleStatus;

        }

		public PresaleViewModel GetViewModel => new()
		{
			Id = Id,
			PresaleStatus = PresaleStatus,
			Description = Description,
			DueTill = DueTill,
			Price = Price,
			PresaleBundlings = PresaleBundlings,
			Requests = Requests.ToDictionary(x => x.Id, x => x as IRequestModel),
		};
	}
}
