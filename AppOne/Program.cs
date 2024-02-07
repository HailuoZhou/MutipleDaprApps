using AppOne.Services.BackEndAPI;
using Dapr.Client;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ###

// http endpoint reading from configuration appsetting file 
//Console.WriteLine("NOT USING DAPR");
// service AppOne and Service BackEndAPIOne launch by http
//// register the http type client
//builder.Services.AddHttpClient<IWeatherService, WeatherService>(
//    (sp, c) =>
//       c.BaseAddress = new Uri(sp.GetService<IConfiguration>()?["ApiConfigs:WeatherService:Uri"] ?? throw new InvalidOperationException("Missing config")));
//### 

#if DEBUG
Debugger.Launch();
#endif

//Console.WriteLine("USING DAPR");


var daprPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
//builder.Services.AddHttpClient<IWeatherService, WeatherService>(c =>
//c.BaseAddress = new Uri($"http://localhost:{daprPort}/v1.0/invoke/BackEndAPIOne/method/"));

builder.Services.AddDaprClient();
builder.Services.AddSingleton<IWeatherService>(sc =>
       new WeatherService(DaprClient.CreateInvokeHttpClient("BackEndAPIOne")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
