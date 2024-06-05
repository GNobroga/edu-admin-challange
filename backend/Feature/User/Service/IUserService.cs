using EduAdmin.Common.Base;
using EduAdmin.Feature.User.DTO;

namespace EduAdmin.Feature.User.Service;

public interface IUserService : IBaseService<UserRequestDTO, UserResponseDTO>
{
    public IEnumerable<UserResponseDTO> Search(string term);
}