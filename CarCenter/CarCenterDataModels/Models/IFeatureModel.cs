using CarCenterDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDataModels.Models
{
	public interface IFeatureModel : IId
	{
		HelpDevices HelpDevice { get; }
		string CabinColor { get; }
        DriveTypes DriveType { get; }
		double Price { get; }
	}
}
