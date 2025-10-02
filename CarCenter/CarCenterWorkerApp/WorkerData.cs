using CarCenterContracts.BusinessLogicsContracts;
using CarCenterContracts.ViewModels;
using CarCenterContracts.BindingModels;
using CarCenterContracts.StoragesContracts;
using CarCenterContracts.SearchModels;
using CarCenterBusinessLogic.BusinessLogics;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using CarCenterBusinessLogic.MailWorker;
using CarCenterBusinessLogic.OfficePackage;

namespace WorkerApp
{
	public class WorkerData
	{
		private readonly ILogger _logger;
		private readonly IWorkerLogic _storekeeperLogic;
		private readonly IPresaleLogic _presaleLogic;
		private readonly IRequestLogic _requestLogic;
		private readonly IOrderLogic _orderLogic;
		private readonly ICarLogic _carLogic;
        private readonly IBundlingLogic _bundlingLogic;
        private readonly AbstractSaveToExcelWorker _excel;
        private readonly AbstractSaveToWordWorker _word;
        private readonly AbstractSaveToPdfWorker _pdf;
        private readonly AbstractMailWorker _mail;

        public WorkerData(ILogger<WorkerData> logger, IWorkerLogic storekeeperLogic, IPresaleLogic presaleLogic, IRequestLogic requestLogic, IOrderLogic orderLogic, IBundlingLogic bundlingLogic, ICarLogic carLogic,
            AbstractSaveToExcelWorker excel,
                         AbstractSaveToWordWorker word,
                         AbstractMailWorker mail,
                         AbstractSaveToPdfWorker pdf)
        {
            _logger = logger;
            _storekeeperLogic = storekeeperLogic;
            _presaleLogic = presaleLogic;
            _requestLogic = requestLogic;
            _orderLogic = orderLogic;
            _bundlingLogic = bundlingLogic;
            _carLogic = carLogic;
            _excel = excel;
            _word = word;
            _mail = mail;
            _pdf = pdf;
        }

        public WorkerViewModel? Login(string email, string password)
		{
			return _storekeeperLogic.ReadElement(new()
			{
				Email = email,
				Password = password
			});
		}
		public bool Register(WorkerBindingModel model)
		{
			return _storekeeperLogic.Create(model);
		}
		public bool UpdateUser(WorkerBindingModel model)
		{
			return _storekeeperLogic.Update(model);
		}

		public List<PresaleViewModel>? GetPresales(int userId)
		{
			return _presaleLogic.ReadList(new PresaleSearchModel() { WorkerId = userId });
		}
		public bool DeletePresale(int presaleId)
		{
			return _presaleLogic.Delete(new() { Id = presaleId });
		}
		public bool CreatePresale(PresaleBindingModel model)
		{
			return _presaleLogic.Create(model);
		}
		public bool UpdatePresale(PresaleBindingModel model)
		{
			return _presaleLogic.Update(model);
		}
		public PresaleViewModel? GetPresale(int id)
		{
			return _presaleLogic.ReadElement(new() { Id = id });
		}
		public List<RequestViewModel>? GetRequests(int userId)
		{
			return _requestLogic.ReadList(new RequestSearchModel() { WorkerId = userId });
		}
		public bool DeleteRequest(int presaleId)
		{
			return _requestLogic.Delete(new() { Id = presaleId });
		}
		public bool CreateRequest(RequestBindingModel model)
		{
			return _requestLogic.Create(model);
		}
		public bool UpdateRequest(RequestBindingModel model)
		{
			return _requestLogic.Update(model);
		}
		public RequestViewModel? GetRequest(int id)
		{
			return _requestLogic.ReadElement(new() { Id = id });
		}
		public List<OrderViewModel>? GetOrders(int userId)
		{
			return _orderLogic.ReadList(new() { WorkerId = userId });
		}
		public OrderViewModel? GetOrder(int id)
		{
			return _orderLogic.ReadElement(new() { Id = id });
		}
		public bool CreateOrder(OrderBindingModel model)
		{
			return _orderLogic.Create(model);
		}
		public bool UpdateOrder(OrderBindingModel model)
		{
			return _orderLogic.Update(model);
		}
		public bool DeleteOrder(int orderId)
		{
			return _orderLogic.Delete(new() { Id = orderId });
		}
        public List<BundlingViewModel>? GetBundlings()
        {
            return _bundlingLogic.ReadList(null);
        }

        public List<CarViewModel>? GetCars()
        {
            return _carLogic.ReadList(null);
        }
        public List<ReportPresaleViewModel>? GetPresaleReports(List<int> presales)
        {
            List<ReportPresaleViewModel> reports = new();

            var orders = _orderLogic.ReadList(null);
            foreach (var order in orders)
            {
                ReportPresaleViewModel report = new();
                if (order.OrderPresales.Select(x => x.Value).Any(y => presales.Contains(y.Id)))
                {
                    report.PresaleId = order.OrderPresales.Keys.FirstOrDefault();
                    report.Cars = order.Cars.Select(x => x.Key.ToString()).ToList();
                }
                reports.Add(report);

            }
            return reports;
        }
        public List<ReportOrderViewModel> GetTimeReport(DateTime? startDate, DateTime? endDate, int userId)
        {
            var orders = _orderLogic.ReadList(new OrderSearchModel { WorkerId = userId, DateFrom = startDate, DateTo = endDate});
            if (orders == null)
                return new List<ReportOrderViewModel>();

            List<ReportOrderViewModel> bundlingTimeReports = new List<ReportOrderViewModel>();

            foreach (var order in orders)
            {
                var report = new ReportOrderViewModel
                {
                    OrderId = order.Id,
                    Bundlings = new List<string>(),
                    Requests = new List<string>()
                };

                // Получение Presales
                var presales = _presaleLogic.ReadList(new PresaleSearchModel { OrderId = order.Id});
                if (presales?.Count != 0 && presales != null)
                {
                    var Requests = new List<string>();
                    foreach (var p in presales)
                    {
                        report.Bundlings = p.PresaleBundlings.Select(x => x.Key.ToString()).ToList();
                        report.Requests = p.Requests.Select(x => x.Value.Description.ToString()).ToList()!;
                    }
                }
                bundlingTimeReports.Add(report);
            }

            return bundlingTimeReports;
        }
        public void SaveReportExcel(List<int> bundlings, MemoryStream stream)
        {
            var reports = GetPresaleReports(bundlings);
            if (reports == null)
                return;
            int maxsize = 0;
            foreach (var report in reports) { maxsize = Math.Max(maxsize, report.Cars.Count); }
            _excel.CreateReport(new()
            {
                carPresalesReport = reports,
                Title = "Отчет Номер_автомобиля предпродажные",
                memoryStream = stream,
                maxleng = maxsize
            });
        }

        public void SaveReportWord(List<int> bundlings, MemoryStream stream)
        {
            var reports = GetPresaleReports(bundlings);
            if (reports == null)
                return;
            _word.CreateDoc(new()
            {
                memoryStream = stream,
                Title = "Отчет.VIN-Номер автомобиля: - предпродажные",
                carPresalesReport = reports
            });
        }

        public void SendMailReport(DateTime? startDate, DateTime? endDate, int UserId, MemoryStream stream)
        {
            var reports = GetTimeReport(startDate, endDate, UserId);
            if (reports == null)
                return;
            _pdf.CreateDoc(new()
            {
                DateFrom = startDate!.Value,
                DateTo = endDate!.Value,
                FileName = stream,
                reportBundling = reports,
                Title = "Отчет"
            });
            byte[] report = stream.GetBuffer();
            _mail.MailSendAsync(new() { MailAddress = UserWorker.user!.Email, Subject = "Отчет", FileName = "PdfReport.pdf", Pdf = report });
        }
    }
}
