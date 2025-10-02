using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.BindingModels
{
	public class BundlingBindingModel : IBundlingModel
	{
		public int Id { get; set; }
		public EquipmentPackage EquipmentPackage { get; set; } = EquipmentPackage.Неизвестно;
		public TirePackage TirePackage { get; set; } = TirePackage.Неизвестно;
		public ToolKit ToolKit { get; set; } = ToolKit.Неизвестно;
		public double Price { get; set; }
		public int StorekeeperId { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, IPresaleModel> BundlingsPresale { get; set; } = new();
    }
}
