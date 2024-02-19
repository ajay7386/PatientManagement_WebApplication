using PatientManagement.Common.AppSettings;
using PatientManagement.ExternalService.Wrapper;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IWrapper, Wrapper>();
builder.Services.AddHttpClient<IWrapper, Wrapper>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.Configure<AppSetting>(
    builder.Configuration.GetSection("AppSetting"));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
