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
	public class FeatureViewModel : IFeatureModel
	{
		public int Id { get; set; }
        public int StorekeeperId { get; set; }
        [DisplayName("Вспомогательные устройства")]
		public HelpDevices HelpDevice { get; set; }
		[DisplayName("Цвет салона")]
		public string CabinColor { get; set; } = string.Empty;
		[DisplayName("Тип привода")]
		public DriveTypes DriveType { get; set; }
		[DisplayName("Цена")]
		public double Price { get; set; }
	}
}
