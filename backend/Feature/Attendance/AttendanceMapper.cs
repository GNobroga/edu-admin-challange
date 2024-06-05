using AutoMapper;
using EduAdmin.Feature.Attendance.DTO;
using EduAdmin.Features.Attendance;
using EduAdmin.Features.Subject;
using EduAdmin.Features.User;


namespace EduAdmin.Feature.Attendance;

public class AttendanceMapper : Profile
{
    public AttendanceMapper()
    {
        CreateMap<AttendanceRequestDTO, AttendanceEntity>();
        CreateMap<AttendanceEntity, AttendanceResponseDTO>();
        CreateMap<UserEntity, AttendanceStudentResponseDTO>();
        CreateMap<SubjectEntity, AttendanceSubjectResponseDTO>();
    }
}