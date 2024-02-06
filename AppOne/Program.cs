using AppOne.Services.BackEndAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ###


Console.WriteLine("NOT USING DAPR");

// register the http type client
builder.Services.AddHttpClient<IWeatherService, WeatherService>(
    (sp, c) =>
       c.BaseAddress = new Uri(sp.GetService<IConfiguration>()?["ApiConfigs:WeatherService:Uri"] ?? throw new InvalidOperationException("Missing config")));
//### 





var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
