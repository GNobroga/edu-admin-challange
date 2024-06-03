using AutoMapper;
using EduAdmin.Features.Class;

namespace EduAdmin.Feature.Class;

public class ClassMapper : Profile
{
    public ClassMapper()
    {
        CreateMap<ClassRequestDTO, ClassEntity>();
    }
}