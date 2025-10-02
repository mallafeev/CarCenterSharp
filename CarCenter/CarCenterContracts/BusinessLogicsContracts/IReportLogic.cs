using CarCenterContracts.BindingModels;
using CarCenterContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.BusinessLogicsContracts
{
	public interface IReportLogic
	{

		List<ReportBundlingViewModel> GetBundligs(ReportBindingModel model);
		List<ReportCarViewModel> GetCars(ReportBindingModel model);
		List<ReportOrderViewModel> GetOrders(ReportBindingModel model);
		List<ReportPresaleViewModel> GetPresales(ReportBindingModel model);

	}
}