using System.Reflection;
using EduAdmin.Context;
using EduAdmin.Feature.Attendance;
using EduAdmin.Feature.Class;
using EduAdmin.Feature.Grade;
using EduAdmin.Feature.Subject;
using HubEscolar.Feature.User;
using Microsoft.EntityFrameworkCore;

namespace EduAdmin.Extension.IoC;

public static class DependencyInjectionExtension 
{

    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder) 
    {   
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors();
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection")));
        AddRepositories(builder);
        return builder;
    }

    public static void AddRepositories(WebApplicationBuilder builder) {
        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddScoped<ClassRepository>();
        builder.Services.AddScoped<AttendanceRepository>();
        builder.Services.AddScoped<SubjectRepository>();
        builder.Services.AddScoped<GradeRepository>();
    }

    public static WebApplication ConfigureMiddlewares(this WebApplication app) {
        app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseRouting();
        app.MapControllers();
        return app;
    }


}