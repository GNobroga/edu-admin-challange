using EduAdmin.Common.Base;
using EduAdmin.Features.Attendance;


namespace EduAdmin.Feature.Attendance.Repository;

public interface IAttendanceRepository : IBaseRepository<AttendanceEntity> {

    IEnumerable<AttendanceEntity> Search(string term);
}