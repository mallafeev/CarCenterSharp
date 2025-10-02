using CarCenterContracts.BindingModels;
using CarCenterContracts.ViewModels;
using CarCenterStorekeeperApp.Models;
using StorekeeperApp;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using CarCenterDataModels.Enums;
using CarCenterDatabaseImplement.Models;

namespace CarCenterStorekeeperApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly StorekeeperData _data;
		public HomeController(ILogger<HomeController> logger, StorekeeperData data)
		{
			_logger = logger;
			_data = data;
		}
		private bool IsLoggedIn { get { return UserStorekeeper.user != null; } }
		private int UserId { get { return UserStorekeeper.user!.Id; } }
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
				UserStorekeeper.user = user;
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
			UserStorekeeper.user = null;
			return RedirectToAction("IndexNonReg");
		}
		[HttpPost]
		public void Register(string name, string surname, string patronymic, string email, long phone, string password1, string password2)
		{
			if (password1 == password2 && _data.Register(new() { Email = email, Name = name, Password = password1, Surname = surname,
			Patronymic = patronymic, PhoneNumber = phone}))
			{
				Response.Redirect("Index");
			}
		}
		[HttpGet]
		public IActionResult IndexBundling()
		{
			if (UserStorekeeper.user != null)
			{
				var list = _data.GetBundlings(UserStorekeeper.user.Id);
				if (list != null)
					return View(list);
				return View(new List<BundlingViewModel>());
			}
			return RedirectToAction("IndexNonReg");
		}
		[HttpPost]
		public void IndexBundling(int id)
		{
			if (UserStorekeeper.user != null)
			{
				_data.DeleteBundling(id);
			}
			Response.Redirect("IndexBundling");
		}
		[HttpGet]
		public IActionResult CreateBundling(int id)
		{
            var presales = _data.GetPresales();
            ViewBag.AllPresales = presales;
			if (id != 0)
			{
				var value = _data.GetBundling(id);
				if (value != null)
					return View(value);
			}
			return View(new BundlingViewModel());
		}
		[HttpPost]
		public IActionResult CreateBundling(BundlingBindingModel model, int[] presaleIds)
		{
            var presales = _data.GetPresales();
            if (model.Id == 0)
			{
                model.DateCreate = DateTime.Now;
                model.StorekeeperId = UserStorekeeper.user!.Id;
                for (int i = 0; i < presaleIds.Length; i++)
                {
                    var presale = presales!.FirstOrDefault(x => x.Id == presaleIds[i])!;
                    model.BundlingsPresale.Add(i, presale);
                }
                if (_data.CreateBundling(model))
					return RedirectToAction("IndexBundling");
			}
			else
			{
                for (int i = 0; i < presaleIds.Length; i++)
                {
                    var presale = presales!.FirstOrDefault(x => x.Id == presaleIds[i])!;
                    model.BundlingsPresale.Add(i, presale);
                }
                model.StorekeeperId = UserStorekeeper.user!.Id;
                if (_data.UpdateBundling(model))
					return RedirectToAction("IndexBundling");
			}
			return View();
		}

        [HttpGet]
        public IActionResult IndexFeature()
        {
            if (UserStorekeeper.user != null)
            {
                var list = _data.GetFeatures(UserStorekeeper.user.Id);
                if (list != null)
                    return View(list);
                return View(new List<FeatureViewModel>());
            }
            return RedirectToAction("IndexNonReg");
        }
        [HttpPost]
        public void IndexFeature(int id)
        {
            if (UserStorekeeper.user != null)
            {
                _data.DeleteFeature(id);
            }
            Response.Redirect("IndexFeature");
        }
        [HttpGet]
        public IActionResult CreateFeature(int id)
        {
            if (id != 0)
            {
                var value = _data.GetFeature(id);
                if (value != null)
                    return View(value);
            }
            return View(new FeatureViewModel());
        }
        [HttpPost]
        public IActionResult CreateFeature(FeatureBindingModel model)
        {
            if (model.Id == 0)
            {
                model.StorekeeperId = UserStorekeeper.user!.Id;
                if (_data.CreateFeature(model))
                    return RedirectToAction("IndexFeature");
            }
            else
            {
                model.StorekeeperId = UserStorekeeper.user!.Id;
                if (_data.UpdateFeature(model))
                    return RedirectToAction("IndexFeature");
            }
            return View();
        }
        [HttpGet]
        public IActionResult IndexCar()
        {
            if (UserStorekeeper.user != null)
            {
                var productions = _data.GetCars(UserStorekeeper.user.Id);
                return View(productions);
            }
            return RedirectToAction("IndexNonReg");
        }
        [HttpPost]
        public IActionResult IndexCar(int id)
        {
            _data.DeleteCar(id);
            return RedirectToAction("IndexCar");
        }
        [HttpGet]
        public IActionResult CreateCar(int id)
        {
            var bundlings = _data.GetBundlings(UserStorekeeper.user!.Id);
			var features = _data.GetFeatures(UserStorekeeper.user!.Id);
            ViewBag.AllBundlings = bundlings;
			ViewBag.Features = features;
            if (id != 0)
            {
                var value = _data.GetCar(id);
                if (value != null)
                    return View(value);
            }
            return View(new CarViewModel());
        }
		[HttpPost]
		public IActionResult CreateCar(int id, int year, long VINnumber, CarBrand CarBrand, CarClass CarClass, int FeatureId, string Model, int[] bundlingIds)
        {
            CarBindingModel model = new CarBindingModel();
            model.Id = id;
			model.Year = year;
			model.VINnumber = VINnumber;
			model.CarBrand = CarBrand;
			model.CarClass = CarClass;
			model.FeatureID = FeatureId;
            model.DateCreate = DateTime.Now;
            model.Model = Model;
            model.StorekeeperId = UserStorekeeper.user!.Id;
            var bundlings = _data.GetBundlings(UserStorekeeper.user!.Id);
            double sum = 0;
            for (int i = 0; i < bundlingIds.Length; i++)
            {
                var bundling = bundlings!.FirstOrDefault(x => x.Id == bundlingIds[i])!;
                model.CarBundlings[bundlingIds[i]] = bundling;
                sum += bundling.Price;
            }
            model.Price = sum;
            bool changed = false;
            if (model.CarBundlings.Count > 0)
            {
                if (id != 0)
                {
                    changed = _data.UpdateCar(model);
                }
                else
                {
                    changed = _data.CreateCar(model);
                }
            }
            if (changed)
                return RedirectToAction("IndexCar");
            else
            {
                ViewBag.AllBundlings = bundlings;
                return View(model);
            }
        }

        [HttpGet]
		public IActionResult Privacy()
		{
			if (IsLoggedIn)
				return View(UserStorekeeper.user);
			return RedirectToAction("IndexNonReg");
		}
		[HttpPost]
		public IActionResult Privacy(int id, string surname, string patronymic, string email, long phone, string password, string name)
		{
			if (!IsLoggedIn)
				return RedirectToAction("IndexNonReg");
			StorekeeperBindingModel user = new() { Id = id,
				Email = email,
				Name = name,
				Surname = surname,
				Patronymic = patronymic,
				PhoneNumber = phone
			};
			if (_data.UpdateUser(user))
			{
				UserStorekeeper.user = new StorekeeperViewModel { Id = id,
					Email = email,
					Name = name,
					Surname = surname,
					Patronymic = patronymic,
					PhoneNumber = phone
				};
			}
			return View(user);
		}
        [HttpGet]
        public IActionResult BundlingTimeChoose()
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
        public IActionResult BundlingTimeReport()
        {
            var startDateStr = HttpContext.Session.GetString("StartDate");
            var endDateStr = HttpContext.Session.GetString("EndDate");

            if (string.IsNullOrEmpty(startDateStr) || string.IsNullOrEmpty(endDateStr))
            {
                return RedirectToAction("BundlingTimeChoose");
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

            return RedirectToAction("BundlingTimeReport");
        }
        [HttpPost]
        public void BundlingTimeMail()
        {
            var startDateStr = HttpContext.Session.GetString("StartDate");
            var endDateStr = HttpContext.Session.GetString("EndDate");
            var startDate = DateTime.Parse(startDateStr);
            var endDate = DateTime.Parse(endDateStr).AddDays(1);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                _data.SendMailReport(startDate, endDate, UserId, memoryStream);
            }
            Response.Redirect("BundlingTimeReport");
        }
        [HttpGet]
        public IActionResult CarPresaleChoose()
        {
            if (!IsLoggedIn)
                return RedirectToAction("IndexNonReg");
            var details = _data.GetCars(UserId);
            return View(details);
        }
        [HttpPost]
        public IActionResult CarPresaleChoose(List<int> selectedItems, string reportType)
        {
            string value = string.Join("/", selectedItems);
            HttpContext.Session.SetString("Cars", value);
            if (reportType.Equals("default"))
                return RedirectToAction("CarPresaleReport");
            else if (reportType.Equals("excel"))
                return RedirectToAction("ExcelGenerate");
            else
                return RedirectToAction("WordGenerate");
        }
        public async Task<IActionResult> ExcelGenerate()
        {
            var value = HttpContext.Session.GetString("Cars");
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
            return RedirectToAction("CarPresaleChoose");
        }
        public async Task<IActionResult> WordGenerate()
        {
            var value = HttpContext.Session.GetString("Cars");
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
            return RedirectToAction("CarPresaleChoose");
        }
        [HttpGet]
        public IActionResult CarPresaleReport()
        {
            var value = HttpContext.Session.GetString("Cars");
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}