using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.BindingModels
{
	public class PresaleBindingModel : IPresaleModel
	{
		public int Id { get; set; }
		public int WorkerId { get; set; }
		public PresaleStatus PresaleStatus { get; set; } = PresaleStatus.Неизвестно;

		public string Description {  get; set; } = string.Empty;

		public DateTime DueTill {  get; set; }

		public double Price {  get; set; }
		public Dictionary<int, IBundlingModel> PresaleBundlings { get; set; } = new();

        public Dictionary<int, IRequestModel> Requests { get; set; } = new();
    }
}
