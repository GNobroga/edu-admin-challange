using AutoMapper;
using EduAdmin.Feature.Grade;
using EduAdmin.Features.Grade;

namespace EduAdmin.Feature.Attendance;

public class GradeMapper : Profile
{
    public GradeMapper()
    {
        CreateMap<GradeRequestDTO, GradeEntity>();
    }
}