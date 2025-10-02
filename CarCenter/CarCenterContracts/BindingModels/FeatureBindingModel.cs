using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.BindingModels
{
	public class FeatureBindingModel : IFeatureModel
	{
		public int Id { get; set; }
        public int StorekeeperId { get; set; }
        public HelpDevices HelpDevice { get; set; } = HelpDevices.Неизвестно;
		public string CabinColor { get; set; } = string.Empty;
		public DriveTypes DriveType { get; set; } = DriveTypes.Неизвестно;	
		public double Price { get; set; }
	}
}
