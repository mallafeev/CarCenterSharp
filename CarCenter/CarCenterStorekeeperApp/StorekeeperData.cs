using CarCenterContracts.BusinessLogicsContracts;
using CarCenterContracts.ViewModels;
using CarCenterContracts.BindingModels;
using CarCenterContracts.StoragesContracts;
using CarCenterContracts.SearchModels;
using CarCenterBusinessLogic.MailWorker;
using CarCenterBusinessLogic.OfficePackage;
using CarCenterDatabaseImplement.Models;
using DocumentFormat.OpenXml.Bibliography;

namespace StorekeeperApp
{
	public class StorekeeperData
	{
		private readonly ILogger _logger;
		private readonly IStorekeeperLogic _storekeeperLogic;
		private readonly IBundlingLogic _bundlingLogic;
        private readonly IFeatureLogic _featureLogic;
		private readonly ICarLogic _carLogic;
        private readonly IPresaleLogic _presaleLogic;
        private readonly IOrderLogic _orderLogic;
        private readonly AbstractSaveToExcelStorekeeper _excel;
        private readonly AbstractSaveToWordStorekeeper _word;
        private readonly AbstractSaveToPdfStorekeeper _pdf;
        private readonly AbstractMailWorker _mail;

        public StorekeeperData(ILogger<StorekeeperData> logger, IStorekeeperLogic storekeeperLogic,
			IBundlingLogic bundlingLogic, IFeatureLogic featureLogic, ICarLogic carLogic,
            IPresaleLogic presaleLogic, IOrderLogic orderLogic,
                         AbstractSaveToExcelStorekeeper excel,
                         AbstractSaveToWordStorekeeper word,
                         AbstractMailWorker mail,
                         AbstractSaveToPdfStorekeeper pdf)
		{
			_logger = logger;
			_storekeeperLogic = storekeeperLogic;
			_bundlingLogic = bundlingLogic;
			_featureLogic = featureLogic;
			_carLogic = carLogic;
            _presaleLogic = presaleLogic;
            _orderLogic = orderLogic;
            _excel = excel;
            _word = word;
            _mail = mail;
            _pdf = pdf;
        }

		public StorekeeperViewModel? Login(string email, string password)
		{
			return _storekeeperLogic.ReadElement(new()
			{
				Email = email,
				Password = password
			});
		}
		public bool Register(StorekeeperBindingModel model)
		{
			return _storekeeperLogic.Create(model);
		}
		public bool UpdateUser(StorekeeperBindingModel model)
		{
			return _storekeeperLogic.Update(model);
		}

		public List<BundlingViewModel>? GetBundlings(int userId)
		{
			return _bundlingLogic.ReadList(new BundlingSearchModel() { StorekeeperId = userId });
		}
		public bool DeleteBundling(int bundlingId)
		{
			return _bundlingLogic.Delete(new() { Id = bundlingId });
		}
		public bool CreateBundling(BundlingBindingModel model)
		{
			return _bundlingLogic.Create(model);
		}
		public bool UpdateBundling(BundlingBindingModel model)
		{
			return _bundlingLogic.Update(model);
		}
		public BundlingViewModel? GetBundling(int id)
		{
			return _bundlingLogic.ReadElement(new() { Id = id });
		}
        public List<FeatureViewModel>? GetFeatures(int userId)
        {
            return _featureLogic.ReadList(new FeatureSearchModel() { StorekeeperId = userId });
        }
        public bool DeleteFeature(int bundlingId)
        {
            return _featureLogic.Delete(new() { Id = bundlingId });
        }
        public bool CreateFeature(FeatureBindingModel model)
        {
            return _featureLogic.Create(model);
        }
        public bool UpdateFeature(FeatureBindingModel model)
        {
            return _featureLogic.Update(model);
        }
        public FeatureViewModel? GetFeature(int id)
        {
            return _featureLogic.ReadElement(new() { Id = id });
        }
        public List<CarViewModel>? GetCars(int userId)
        {
            return _carLogic.ReadList(new CarSearchModel() { StorekeeperId = userId });
        }
        public CarViewModel? GetCar(int id)
        {
            return _carLogic.ReadElement(new() { Id = id });
        }
        public bool CreateCar(CarBindingModel model)
        {
            return _carLogic.Create(model);
        }
        public bool UpdateCar(CarBindingModel model)
        {
            return _carLogic.Update(model);
        }
        public bool DeleteCar(int carId)
        {
            return _carLogic.Delete(new() { Id = carId });
        }
        public List<ReportBundlingViewModel> GetTimeReport(DateTime? startDate, DateTime? endDate, int userId)
        {
            var bundlings = _bundlingLogic.ReadList(new BundlingSearchModel { StorekeeperId = userId });
            if (bundlings == null)
                return new List<ReportBundlingViewModel>();

            List<ReportBundlingViewModel> bundlingTimeReports = new List<ReportBundlingViewModel>();

            foreach (var bundling in bundlings)
            {
                var report = new ReportBundlingViewModel
                {
                    BundlingId = bundling.Id,
                    Features = new List<string>(),
                    Orders = new List<string>()
                };

                // Получение Orders
                var orders = _orderLogic.ReadList(new OrderSearchModel { BundlingId = bundling.Id, DateFrom = startDate, DateTo = endDate });
                if (orders?.Count != 0 && orders != null) 
                {
                    // Получение Features из машин в заказах
                    var features = new List<string>();
                    foreach (var order in orders)
                    {
                        bool fnd = false;
                        var cars = order.Cars; 
                        foreach (var car in cars) { 
                            if(car.Value.CarBundlings.Count <= 0)
                            {
                                continue;
                            }
                            var carFeatures = _carLogic.ReadList(new CarSearchModel { FeatureId = car.Value.FeatureID, OrderId = order.Id });
                            if (carFeatures?.Count != 0 && carFeatures != null)
                            {
                                fnd = true;
                                features.AddRange(carFeatures.Select(f => f.FeatureID.ToString()));
                            }
                        }
                        if (fnd)
                        {
                            report.Orders.Add(order.Id.ToString());
                        }
                    }
                    report.Features = features.Distinct().ToList(); // Удаление дубликатов
                }

                bundlingTimeReports.Add(report);
            }

            return bundlingTimeReports;
        }
        public List<PresaleViewModel>? GetPresales()
        {
            return _presaleLogic.ReadList(null);
        }
        public List<OrderViewModel>? GetOrders()
        {
            return _orderLogic.ReadList(null);
        }

        public List<ReportCarViewModel>? GetPresaleReports(List<int> cars)
        {
            List<ReportCarViewModel> reports = new();

            var orders = _orderLogic.ReadList(null);
            foreach (var order in orders)
            {
                var Cars = order.Cars;
                foreach (var car in Cars)
                {
                    ReportCarViewModel report = new();
                    report.VINnumber = (int)car.Value.VINnumber;
                    if (cars.Contains(car.Value.Id))
                    {
                        report.Presales = order.OrderPresales.Select(p => p.Value.Id.ToString()).ToList();
                    }
                    reports.Add(report);
                }
                
            }
            return reports;
        }

        public void SaveReportExcel(List<int> bundlings, MemoryStream stream)
        {
            var reports = GetPresaleReports(bundlings);
            if (reports == null)
                return;
            int maxsize = 0;
            foreach (var report in reports) { maxsize = Math.Max(maxsize, report.Presales.Count); }
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
            _mail.MailSendAsync(new() { MailAddress = UserStorekeeper.user!.Email, Subject = "Отчет", FileName = "PdfReport.pdf", Pdf = report });
        }
    }
}
