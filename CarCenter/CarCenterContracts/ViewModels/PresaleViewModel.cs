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
	public class PresaleViewModel : IPresaleModel
	{
		public int Id { get; set; }
		[DisplayName("Статус работы")]
		public PresaleStatus PresaleStatus { get; set; } = PresaleStatus.Неизвестно;
		[DisplayName("Описание")]
		public string Description { get; set; } = string.Empty;
		[DisplayName("Выполнить до")]
		public DateTime DueTill { get; set; }
		[DisplayName("Цена")]
		public double Price { get; set; }
		public Dictionary<int, IBundlingModel> PresaleBundlings { get; set; } = new();
        public Dictionary<int, IRequestModel> Requests { get; set; } = new();
    }
}
