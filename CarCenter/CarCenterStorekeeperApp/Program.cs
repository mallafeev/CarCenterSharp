using CarCenterBusinessLogic.BusinessLogics;
using CarCenterBusinessLogic.MailWorker;
using CarCenterBusinessLogic.OfficePackage;
using CarCenterBusinessLogic.OfficePackage.Implements;
using CarCenterContracts.BusinessLogicsContracts;
using CarCenterContracts.StoragesContracts;
using CarCenterDatabaseImplement.Implements;
using StorekeeperApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddLog4Net("log4net.config");

builder.Services.AddTransient<IBundlingStorage, BundlingStorage>();
builder.Services.AddTransient<ICarStorage, CarStorage>();
builder.Services.AddTransient<IFeatureStorage, FeatureStorage>();
builder.Services.AddTransient<IOrderStorage, OrderStorage>();
builder.Services.AddTransient<IPresaleStorage, PresaleStorage>();
builder.Services.AddTransient<IRequestStorage, RequestStorage>();
builder.Services.AddTransient<IStorekeeperStorage, StorekeeperStorage>();
builder.Services.AddTransient<IWorkerStorage, WorkerStorage>();
builder.Services.AddTransient<IBundlingLogic, BundlingLogic>();
builder.Services.AddTransient<ICarLogic, CarLogic>();
builder.Services.AddTransient<IFeatureLogic, FeatureLogic>();
builder.Services.AddTransient<IOrderLogic, OrderLogic>();
builder.Services.AddTransient<IPresaleLogic, PresaleLogic>();
builder.Services.AddTransient<IRequestLogic, RequestLogic>();
builder.Services.AddTransient<IStorekeeperLogic, StorekeeperLogic>();
builder.Services.AddTransient<IWorkerLogic, WorkerLogic>();
builder.Services.AddTransient<AbstractSaveToExcelStorekeeper, SaveToExcelStorekeeper>();
builder.Services.AddTransient<AbstractSaveToWordStorekeeper, SaveToWordStorekeeper>();
builder.Services.AddTransient<AbstractSaveToPdfStorekeeper, SaveToPdfStorekeeper>();
builder.Services.AddSingleton<AbstractMailWorker, MailKitWorker>();
builder.Services.AddTransient<StorekeeperData>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();
var mailSender = app.Services.GetService<AbstractMailWorker>();
mailSender?.MailConfig(new()
{
    MailLogin = builder.Configuration?.GetSection("MailLogin")?.Value?.ToString() ?? string.Empty,
    MailPassword = builder.Configuration?.GetSection("MailPassword")?.Value?.ToString() ?? string.Empty,
    SmtpClientHost = builder.Configuration?.GetSection("SmtpClientHost")?.Value?.ToString() ?? string.Empty,
    SmtpClientPort = Convert.ToInt32(builder.Configuration?.GetSection("SmtpClientPort")?.Value?.ToString()),
    PopHost = builder.Configuration?.GetSection("PopHost")?.Value?.ToString() ?? string.Empty,
    PopPort = Convert.ToInt32(builder.Configuration?.GetSection("PopPort")?.Value?.ToString())

});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
