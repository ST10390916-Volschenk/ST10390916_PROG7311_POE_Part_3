using ST10390916_PROG7311_POE.Data;
using ST10390916_PROG7311_POE.Models;
using ST10390916_PROG7311_POE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(Options =>
{
    Options.IdleTimeout = TimeSpan.FromMinutes(120);
});


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

app.UseSession();

AppDBContext context = new AppDBContext();
UserService userService = new UserService();
ProductService product = new ProductService();

if (context.Users.ToList<User>().Count < 1)
{
    userService.AddEmployee();
    userService.AddFarmer();
}

if (context.Products.ToList<Product>().Count < 1)
{
    product.AddInitialProducts();
}

app.Run();

