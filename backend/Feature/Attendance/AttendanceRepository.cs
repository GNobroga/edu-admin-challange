using EduAdmin.Common;
using EduAdmin.Context;
using EduAdmin.Features.Attendance;

namespace EduAdmin.Feature.Attendance;

public class AttendanceRepository(AppDbContext context) : GenericRepository<AttendanceEntity>(context) {}