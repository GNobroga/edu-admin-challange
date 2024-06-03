using EduAdmin.Features.Attendance;
using EduAdmin.Features.Class;
using EduAdmin.Features.Grade;
using EduAdmin.Features.Subject;
using EduAdmin.Features.User;
using Microsoft.EntityFrameworkCore;

namespace EduAdmin.Context;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options) 
{

    public DbSet<AttendanceEntity> Attendances { get; set; }

    public DbSet<ClassEntity> Classes { get; set; }

    public DbSet<GradeEntity> Grades { get; set; }
    public DbSet<SubjectEntity> Subjects { get; set; }
    public DbSet<UserEntity> Users { get; set; }

}