using AutoMapper;
using EduAdmin.Features.Attendance;

namespace EduAdmin.Feature.Attendance;

public class AttendanceMapper : Profile
{
    public AttendanceMapper()
    {
        CreateMap<AttendanceRequestDTO, AttendanceEntity>();
    }
}