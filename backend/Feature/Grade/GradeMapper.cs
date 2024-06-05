using AutoMapper;
using EduAdmin.Feature.Grade.DTO;
using EduAdmin.Features.Grade;
using EduAdmin.Features.Subject;
using EduAdmin.Features.User;

namespace EduAdmin.Feature.Attendance;

public class GradeMapper : Profile
{
    public GradeMapper()
    {
        CreateMap<GradeRequestDTO, GradeEntity>();
        CreateMap<GradeEntity, GradeResponseDTO>();
        CreateMap<UserEntity, GradeStudentResponseDTO>();
        CreateMap<SubjectEntity, GradeSubjectResponseDTO>();
    }
}