using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.ViewModels
{
	public class BundlingViewModel : IBundlingModel
	{
		public int Id { get; set; }
        public int StorekeeperId { get; set; }
        [DisplayName("Пакет оборудования")]
		public EquipmentPackage EquipmentPackage { get; set; }
		[DisplayName("Пакет шин")]
		public TirePackage TirePackage { get; set; }
		[DisplayName("Пакет инструментов")]
		public ToolKit ToolKit { get; set; }
		[DisplayName("Цена")]
		public double Price { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        public Dictionary<int, IPresaleModel> BundlingsPresale { get; set; } = new();
    }
}
