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
	// создан для БУДУЩЕГО получения списков авто по предпродажной
	public class WorkerReportViewModel
	{
		public int? Id;
		public Dictionary<int, ICarModel> CarsPresale; // список авто по предпродажной
		public int? Count;
	}
}
