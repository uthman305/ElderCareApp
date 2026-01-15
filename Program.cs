using ElderCareApp.Data;
using ElderCareApp.Implementations.Repositories;
using ElderCareApp.Implementations.Services;
using ElderCareApp.Interfaces.Repositories;
using ElderCareApp.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<ICareHomeRepository, CareHomeRepository>();
builder.Services.AddScoped<ICareHomeService, CareHomeService>();
builder.Services.AddScoped<IElderRepository, ElderRepository>();
builder.Services.AddScoped<IElderService, ElderService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
