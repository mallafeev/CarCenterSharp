using CarCenterContracts.BindingModels;
using CarCenterContracts.ViewModels;
using CarCenterDatabaseImplement.Models;
using CarCenterDataModels.Enums;
using CarCenterWorkerApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkerApp;

namespace CarCenterWorkerApp.Controllers
{
    public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		private readonly WorkerData _data;
		public HomeController(ILogger<HomeController> logger, WorkerData data)
		{
			_logger = logger;
			_data = data;
		}
		private bool IsLoggedIn { get { return UserWorker.user != null; } }
		private int UserId { get { return UserWorker.user!.Id; } }
		public IActionResult IndexNonReg()
		{
			if (!IsLoggedIn)
				return View();
			return RedirectToAction("Index");
		}
		public IActionResult Index()
		{
			if (!IsLoggedIn)
				return RedirectToAction("IndexNonReg");
			return View();
		}
		[HttpGet]
		public IActionResult Enter()
		{
			if (!IsLoggedIn)
				return View();
			return RedirectToAction("Index");
		}
		[HttpPost]
		public void Enter(string login, string password)
		{
			var user = _data.Login(login, password);
			if (user != null)
			{
				UserWorker.user = user;
				Response.Redirect("Index");
			}
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		public IActionResult Logout()
		{
			UserWorker.user = null;
			return RedirectToAction("IndexNonReg");
		}
		[HttpPost]
		public void Register(string name, string surname, string patronymic, string email, long phone, string password1, string password2)
		{
			if (password1 == password2 && _data.Register(new()
			{
				Email = email,
				Name = name,
				Password = password1,
				Surname = surname,
				Patronymic = patronymic,
				PhoneNumber = phone
			}))
			{
				Response.Redirect("Index");
			}
		}
		[HttpGet]
		public IActionResult IndexRequest()
		{
			if (UserWorker.user != null)
			{
				var list = _data.GetRequests(UserWorker.user.Id);
				if (list != null)
					return View(list);
				return View(new List<RequestViewModel>());
			}
			return RedirectToAction("IndexNonReg");
		}
		[HttpPost]
		public void IndexRequest(int id)
		{
			if (UserWorker.user != null)
			{
				_data.DeleteRequest(id);
			}
			Response.Redirect("IndexRequest");
		}
		[HttpGet]
		public IActionResult CreateRequest(int id)
		{
			if (id != 0)
			{
				var value = _data.GetRequest(id);
				if (value != null)
					return View(value);
			}
			return View(new RequestViewModel());
		}
		[HttpPost]
		public IActionResult CreateRequest(RequestBindingModel model)
		{
			if (model.Id == 0)
			{
				model.PresaleId = null;
				model.WorkerId = UserWorker.user!.Id;
				if (_data.CreateRequest(model))
					return RedirectToAction("IndexRequest");
			}
			else
			{
				model.WorkerId = UserWorker.user!.Id;
				if (_data.UpdateRequest(model))
					return RedirectToAction("IndexRequest");
			}
			return View();
		}

        [HttpGet]
        public IActionResult IndexPresale()
        {
            if (UserWorker.user != null)
            {
                var productions = _data.GetPresales(UserWorker.user.Id);
                return View(productions);
            }
            return RedirectToAction("IndexNonReg");
        }
        [HttpPost]
        public IActionResult IndexPresale(int id)
        {
            _data.DeletePresale(id);
            return RedirectToAction("IndexPresale");
        }
        [HttpGet]
        public IActionResult CreatePresale(int id)
        {
            var bundlings = _data.GetBundlings();
            var requests = _data.GetRequests(UserWorker.user!.Id);
            ViewBag.AllBundlings = bundlings;
            ViewBag.AllRequests = requests;
            if (id != 0)
            {
                var value = _data.GetPresale(id);
                if (value != null)
                    return View(value);
            }
            return View(new PresaleViewModel());
        }
        [HttpPost]
        public IActionResult CreatePresale(PresaleBindingModel model,int[] bundlingIds, int[] requestIds)
        {
            var bundlings = _data.GetBundlings();
            for (int i = 0; i < bundlingIds.Length; i++)
            {
                var bundling = bundlings!.FirstOrDefault(x => x.Id == bundlingIds[i])!;
                model.PresaleBundlings.TryAdd(bundling.Id, bundling);
            }
            var requests = _data.GetRequests(UserWorker.user!.Id);
            for (int i = 0; i < requestIds.Length; i++)
            {
                var request = requests!.FirstOrDefault(x => x.Id == requestIds[i])!;
                model.Requests.TryAdd(request.Id, request);
            }
            model.WorkerId = UserWorker.user!.Id;
            bool changed = false;
            if (model.PresaleBundlings.Count > 0)
            {
                if (model.Id != 0)
                {
                    changed = _data.UpdatePresale(model);
                }
                else
                {
                    changed = _data.CreatePresale(model);
                }
            }
            if (changed)
                return RedirectToAction("IndexPresale");
            else
            {
                ViewBag.AllBundlings = bundlings;
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult IndexOrder()
        {
            if (UserWorker.user != null)
            {
                var productions = _data.GetOrders(UserWorker.user.Id);
                return View(productions);
            }
            return RedirectToAction("IndexNonReg");
        }
        [HttpPost]
        public IActionResult IndexOrder(int id)
        {
            _data.DeleteOrder(id);
            return RedirectToAction("IndexOrder");
        }
        [HttpGet]
        public IActionResult CreateOrder(int id)
        {
            var cars = _data.GetCars();
            var presales = _data.GetPresales(UserWorker.user!.Id);
            ViewBag.AllCars = cars;
            ViewBag.AllPresales = presales;
            if (id != 0)
            {
                var value = _data.GetOrder(id);
                if (value != null)
                    return View(value);
            }
            return View(new OrderViewModel());
        }
        [HttpPost]
        public IActionResult CreateOrder(OrderBindingModel model, int[] carIds, int[] presaleIds, string Sum)
        {
            var cars = _data.GetCars();
            for (int i = 0; i < carIds.Length; i++)
            {
                var car = cars!.FirstOrDefault(x => x.Id == carIds[i])!;
                model.Cars.Add(i, car);
            }
            var presales = _data.GetPresales(UserWorker.user!.Id);
            for (int i = 0; i < presaleIds.Length; i++)
            {
                var presale = presales!.FirstOrDefault(x => x.Id == presaleIds[i])!;
                model.OrderPresales.Add(i, presale);
            }
            model.WorkerId = UserWorker.user!.Id;
            if(double.TryParse(Sum, System.Globalization.CultureInfo.InvariantCulture, out double val))
            {
                model.Sum = val;
            }
            bool changed = false;
            if (model.OrderPresales.Count > 0)
            {
                if (model.Id != 0)
                {
                    changed = _data.UpdateOrder(model);
                }
                else
                {
                    changed = _data.CreateOrder(model);
                }
            }
            if (changed)
                return RedirectToAction("IndexOrder");
            else
            {
                ViewBag.AllCars = cars;
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult OrderTimeChoose()
        {
            if (!IsLoggedIn)
                return RedirectToAction("IndexNonReg");
            return View();
        }
        [HttpPost]
        public IActionResult SendReport(DateTime startDate, DateTime endDate)
        {

            return Ok();
        }
        [HttpGet]
        public IActionResult OrderTimeReport()
        {
            var startDateStr = HttpContext.Session.GetString("StartDate");
            var endDateStr = HttpContext.Session.GetString("EndDate");

            if (string.IsNullOrEmpty(startDateStr) || string.IsNullOrEmpty(endDateStr))
            {
                return RedirectToAction("OrderTimeChoose");
            }

            var startDate = DateTime.Parse(startDateStr);
            var endDate = DateTime.Parse(endDateStr).AddDays(1);

            var values = _data.GetTimeReport(startDate, endDate, UserId);

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            return View(values);
        }
        [HttpPost]
        public IActionResult TimeReportWeb(DateTime startDate, DateTime endDate)
        {
            if (!IsLoggedIn)
                return RedirectToAction("IndexNonReg");

            HttpContext.Session.SetString("StartDate", startDate.ToString("yyyy-MM-dd"));
            HttpContext.Session.SetString("EndDate", endDate.ToString("yyyy-MM-dd"));

            return RedirectToAction("OrderTimeReport");
        }
        [HttpPost]
        public void OrderTimeMail()
        {
            var startDateStr = HttpContext.Session.GetString("StartDate");
            var endDateStr = HttpContext.Session.GetString("EndDate");
            var startDate = DateTime.Parse(startDateStr);
            var endDate = DateTime.Parse(endDateStr).AddDays(1);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                _data.SendMailReport(startDate, endDate, UserId, memoryStream);
            }
            Response.Redirect("OrderTimeReport");
        }
        [HttpGet]
        public IActionResult PresaleCarChoose()
        {
            if (!IsLoggedIn)
                return RedirectToAction("IndexNonReg");
            var details = _data.GetPresales(UserId);
            return View(details);
        }
        [HttpPost]
        public IActionResult PresaleCarChoose(List<int> selectedItems, string reportType)
        {
            string value = string.Join("/", selectedItems);
            HttpContext.Session.SetString("Presales", value);
            if (reportType.Equals("default"))
                return RedirectToAction("PresaleCarReport");
            else if (reportType.Equals("excel"))
                return RedirectToAction("ExcelGenerate");
            else
                return RedirectToAction("WordGenerate");
        }
        public async Task<IActionResult> ExcelGenerate()
        {
            var value = HttpContext.Session.GetString("Presales");
            if (value != null)
            {
                List<int> rawReports = value!.Split('/').Select(x => int.Parse(x)).ToList();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    _data.SaveReportExcel(rawReports, memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var outputStream = new MemoryStream();
                    await memoryStream.CopyToAsync(outputStream);
                    outputStream.Seek(0, SeekOrigin.Begin);
                    return File(outputStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReportExcel.xlsx");
                }
            }
            return RedirectToAction("PresaleCarChoose");
        }
        public async Task<IActionResult> WordGenerate()
        {
            var value = HttpContext.Session.GetString("Presales");
            if (value != null)
            {
                List<int> rawReports = value!.Split('/').Select(x => int.Parse(x)).ToList();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    _data.SaveReportWord(rawReports, memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var outputStream = new MemoryStream();
                    await memoryStream.CopyToAsync(outputStream);
                    outputStream.Seek(0, SeekOrigin.Begin);
                    return File(outputStream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ReportWord.docx");
                }
            }
            return RedirectToAction("PresaleCarChoose");
        }
        [HttpGet]
        public IActionResult PresaleCarReport()
        {
            var value = HttpContext.Session.GetString("Presales");
            if (value != null)
            {
                List<int> rawReports = value!.Split('/').Select(x => int.Parse(x)).ToList();
                var reports = _data.GetPresaleReports(rawReports);
                return View(reports);
            }
            return View(new List<ReportCarViewModel>());
        }
        public IActionResult ReportMenu()
        {
            return View();
        }
    }
}