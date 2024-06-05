using AutoMapper;
using EduAdmin.Feature.Class.DTO;
using EduAdmin.Features.Class;

namespace EduAdmin.Feature.Class;

public class ClassMapper : Profile
{
    public ClassMapper()
    {
        CreateMap<ClassRequestDTO, ClassEntity>();
        CreateMap<ClassEntity, ClassResponseDTO>();
    }
}