using AutoMapper;
using EduAdmin.Feature.Subject;
using EduAdmin.Features.Subject;

namespace EduAdmin.Feature.Attendance;

public class SubjectMapper : Profile
{
    public SubjectMapper()
    {
        CreateMap<SubjectRequestDTO, SubjectEntity>();
    }
}