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
	public class RequestViewModel : IRequestModel
	{
		public int Id { get; set; }
		public int? PresaleId { get; set; }
		[DisplayName("Описание пожелания")]
		public string Description { get; set; } = string.Empty;
		[DisplayName("Тип пожелания")]
		public RequestTypes RequestType { get; set; } = RequestTypes.Неизвестно;
	}
}
