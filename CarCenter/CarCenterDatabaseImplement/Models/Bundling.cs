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
	public class Bundling : IBundlingModel
	{
		public int Id { get; private set; }
        [Required]
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public int StorekeeperId { get; set; }
        [Required]
		public EquipmentPackage EquipmentPackage { get; set; } = EquipmentPackage.Неизвестно;
		[Required]
		public TirePackage TirePackage { get; set; } = TirePackage.Неизвестно;
		[Required]
		public ToolKit ToolKit { get; set; } = ToolKit.Неизвестно;
		[Required]
		public double Price { get; set; }
		[ForeignKey("BundlingId")]
		public virtual List<CarBundling> CarBundling { get; set; } = new();
		[ForeignKey("BundlingId")]
		public virtual List<PresaleBundling> PresaleBundling { get; set; } = new();
        private Dictionary<int, IPresaleModel>? _bundlingPresales = null;
        public Dictionary<int, IPresaleModel> BundlingPresales
        {
            get
            {
                if (_bundlingPresales == null)
                {
                    _bundlingPresales = PresaleBundling
                        .GroupBy(recPc => recPc.PresaleId)
                        .ToDictionary(g => g.Key, g => g.First().Presale as IPresaleModel);
                }
                return _bundlingPresales;
            }
        }
        public static Bundling? Create(CarCenterDatabase context, BundlingBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			return new Bundling()
			{
				Id = model.Id,
				StorekeeperId = model.StorekeeperId,
				EquipmentPackage = model.EquipmentPackage,
				TirePackage = model.TirePackage,
				ToolKit = model.ToolKit,
				Price = model.Price,		
				DateCreate = model.DateCreate,
                PresaleBundling = model.BundlingsPresale.Select(x => new PresaleBundling
                {
                    Presale = context.Presales.First(y => y.Id == x.Value.Id)
                }).ToList()
            };
		}
		public void Update(BundlingBindingModel model)
		{
			if (model == null)
			{
				return;
			}
			DateCreate = model.DateCreate;
			StorekeeperId = model.StorekeeperId;
			EquipmentPackage = model.EquipmentPackage;
			TirePackage = model.TirePackage;
			ToolKit = model.ToolKit;
			Price = model.Price;
		}
        public void UpdatePresales(CarCenterDatabase context, BundlingBindingModel model)
        {
            var bundling = context.Bundlings.First(x => x.Id == Id);
            var existingPresaleBundlings = context.PresaleBundlings.Where(pb => pb.BundlingId == bundling.Id).ToList();
            context.PresaleBundlings.RemoveRange(existingPresaleBundlings);
            context.SaveChanges();
            foreach (var pc in model.BundlingsPresale)
            {
                var tmp = new PresaleBundling
                {
                    Bundling = bundling,
                    Presale = context.Presales.First(x => x.Id == pc.Value.Id),
                };
                if (context.PresaleBundlings.Contains(tmp))
                {
                    continue;
                }
                context.PresaleBundlings.Add(tmp);
                context.SaveChanges();
            }
			_bundlingPresales = null;
        }
        public BundlingViewModel GetViewModel => new()
		{
			Id = Id,
			StorekeeperId = StorekeeperId,
			EquipmentPackage = EquipmentPackage,
			TirePackage = TirePackage,
			ToolKit = ToolKit,
			Price = Price,		
			DateCreate = DateCreate,
			BundlingsPresale = BundlingPresales,
		};
	}
}
