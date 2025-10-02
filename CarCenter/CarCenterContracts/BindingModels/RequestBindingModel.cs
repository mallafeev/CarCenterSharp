using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.BindingModels
{
	public class RequestBindingModel : IRequestModel
	{
		public int Id {  get; set; }
		public int WorkerId { get; set; }
		public int? PresaleId { get; set; }
		public string Description {  get; set; } = string.Empty;
		public RequestTypes RequestType { get; set; } = RequestTypes.Неизвестно;

	}
}
