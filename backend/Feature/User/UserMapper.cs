using AutoMapper;
using EduAdmin.Feature.User.DTO;
using EduAdmin.Features.User;
namespace HubEscolar.Feature.User;

public class UserMapper : Profile 
{

    public UserMapper() {
        CreateMap<UserRequestDTO, UserEntity>();
        CreateMap<UserEntity, UserResponseDTO>();
    }
}