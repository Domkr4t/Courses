using Microsoft.EntityFrameworkCore;
using Site.Backend.DAL;
using Site.Backend.DAL.Interfaces;
using Site.Backend.DAL.Repositories;
using Site.Teacher.Domain.Entity;
using Site.Teacher.Service.Implemintation;
using Site.Teacher.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IBaseRepository<TeacherEntity>, TeacherRepository>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

var connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Teacher}/{action=TeacherHandler}");
app.Run();
