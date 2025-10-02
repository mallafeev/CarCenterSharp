using CarCenterDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDataModels.Models
{
	public interface IBundlingModel : IId
	{
		EquipmentPackage EquipmentPackage {  get; }
		TirePackage TirePackage { get; }
		ToolKit ToolKit { get; }
		double Price { get; }
        DateTime DateCreate { get; }
    }
}
