using System.Reflection;
using EduAdmin.Common.Model;
using EduAdmin.Context;
using EduAdmin.Feature.Attendance.Repository;
using EduAdmin.Feature.Attendance.Service;
using EduAdmin.Feature.Class.Repository;
using EduAdmin.Feature.Class.Service;
using EduAdmin.Feature.Grade.Repository;
using EduAdmin.Feature.Grade.Service;
using EduAdmin.Feature.Subject.Repository;
using EduAdmin.Feature.Subject.Service;
using EduAdmin.Feature.User.DTO;
using EduAdmin.Feature.User.Repository;
using EduAdmin.Feature.User.Service;
using EduAdmin.Feature.User.Validator;
using EduAdmin.Middleware;
using FluentValidation;
using HubEscolar.Feature.User.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduAdmin.Extension;

public static class DependencyInjectionExtension 
{

    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder) 
    {   
        builder.Services.AddControllers();
        builder.Services.Configure<ApiBehaviorOptions>(options => {
            options.InvalidModelStateResponseFactory = HandleInvalidModelState;
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors();
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection")));
        AddRepositoriesAndServices(builder);
        return builder;
    }

    private static IActionResult HandleInvalidModelState(ActionContext context) 
    {
         var errors =  context.ModelState
            .Where(e => e.Value!.Errors.Any())
            .SelectMany(e => e.Value!.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

            return new BadRequestObjectResult(
                new ResponseError("Error", StatusCodes.Status400BadRequest, errors)
        );
    }

    public static void AddRepositoriesAndServices(WebApplicationBuilder builder) {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IClassRepository, ClassRepository>();
        builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
        builder.Services.AddScoped<IGradeRepository, GradeRepository>();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IClassService, ClassService>();
        builder.Services.AddScoped<IAttendanceService, AttendanceService>();
        builder.Services.AddScoped<ISubjectService, SubjectService>();
        builder.Services.AddScoped<IGradeService, GradeService>();
    }



    public static WebApplication ConfigureMiddlewares(this WebApplication app) {
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseRouting();
        app.MapControllers();
        return app;
    }


} 