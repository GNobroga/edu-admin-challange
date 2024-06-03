using AutoMapper;
using EduAdmin.Features.User;
namespace HubEscolar.Feature.User;

public class UserMapper : Profile 
{

    public UserMapper() {
        CreateMap<UserRequestDTO, UserEntity>();
    }
}