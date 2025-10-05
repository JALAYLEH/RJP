using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.BLL.Interfaces;
using SubscriptionManagement.BLL.Services;
using SubscriptionManagement.DAL.AppDBContext;
using SubscriptionManagement.DAL.Infrasructure.Interfaces;
using SubscriptionManagement.DAL.Infrasructure.UnitOfWork;
using SubscriptionManagement.Models.MappingProfiles;
using SubscriptionManagement.Models.MappingProfiles.PlanProfile;
using SubscriptionManagement.Models.MappingProfiles.SubscriptionProfile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // MVC support

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Services
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IUserService, UserService>();

// Map AutoMapper profiles
builder.Services.AddAutoMapper(typeof(PlanProfile).Assembly);
builder.Services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(SubscriptionProfile).Assembly);

var app = builder.Build();

// Apply migrations automatically at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Map default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
