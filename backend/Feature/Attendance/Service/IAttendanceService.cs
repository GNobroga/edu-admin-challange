using EduAdmin.Common.Base;
using EduAdmin.Feature.Attendance.DTO;

namespace EduAdmin.Feature.Attendance.Service;

public interface IAttendanceService : IBaseService<AttendanceRequestDTO, AttendanceResponseDTO> {
   
   IEnumerable<AttendanceResponseDTO> Search(string term);

   AttendancePresentCountDTO GetCountPresentByStudentId(int id);
}