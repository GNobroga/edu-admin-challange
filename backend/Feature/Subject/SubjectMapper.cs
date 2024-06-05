using AutoMapper;
using EduAdmin.Feature.Subject.DTO;
using EduAdmin.Features.Class;
using EduAdmin.Features.Subject;
using EduAdmin.Features.User;

namespace EduAdmin.Feature.Attendance;

public class SubjectMapper : Profile
{
    public SubjectMapper()
    {
        CreateMap<SubjectRequestDTO, SubjectEntity>();
        CreateMap<SubjectEntity, SubjectResponseDTO>();
        CreateMap<ClassEntity, SubjectClassResponseDTO>();
        CreateMap<UserEntity, SubjectTeacherResponseDTO>();
    }
}