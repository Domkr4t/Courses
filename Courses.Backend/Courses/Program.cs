using Microsoft.EntityFrameworkCore;
using Site.Backend.DAL;
using Site.Backend.DAL.Interfaces;
using Site.Backend.DAL.Repositories;
using Site.Domain.Course.Entity;
using Site.Domain.User.Entity;
using Site.Services.Users.Implementations;
using Site.Services.Users.Interfaces;
using Site.Servises.Courses.Implementations;
using Site.Servises.Courses.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
    )
);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBaseRepository<UserEntity>, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBaseRepository<CourseEntity>, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

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
app.UseCors("AllowAllOrigins");
//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Course}/{action=CourseHandler}");
app.Run();
