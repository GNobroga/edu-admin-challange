using AutoMapper;
using EduAdmin.Common.Model;
using EduAdmin.Feature.User.DTO;
using EduAdmin.Feature.User.Repository;
using EduAdmin.Features.User;

namespace EduAdmin.Feature.User.Service;

public class UserService(IUserRepository repository, IMapper mapper) : IUserService
{
    public UserResponseDTO Create(UserRequestDTO record)
    {
        if (repository.ExistsByEmail(record.Email!)) 
            throw new ApplicationException("O email já está sendo utilizado.");

        var entity = mapper.Map<UserEntity>(record);

        return mapper.Map<UserResponseDTO>(repository.Create(entity));
    }

    public bool DeleteById(int id)
    {
        return repository.DeleteById(id);
    }


    public IEnumerable<UserResponseDTO> FindAll(PageRequest pageRequest)
    {
       return mapper.Map<IEnumerable<UserResponseDTO>>(repository.FindAll(pageRequest));
    }

    public UserResponseDTO FindById(int id)
    {
        return mapper.Map<UserResponseDTO>(ThrowIfUserNotFoundOrGet(id));
    }

    public IEnumerable<UserResponseDTO> Search(string term)
    {
        return mapper.Map<List<UserResponseDTO>>(repository.Search(term));
    }

    public bool Update(int id, UserRequestDTO source)
    {
       var user = ThrowIfUserNotFoundOrGet(id);
        Console.WriteLine("user.Email != source.Email" + user.Email != source.Email);
       if (repository.ExistsByEmail(source.Email!) && user.Email != source.Email)
            throw new ApplicationException("O email já está sendo utilizado.");

        if (user.Type != source.Type)
            throw new ApplicationException("Não é permitido alterar o tipo do usuário");

        return repository.Update(mapper.Map(source, user));
    }

    private UserEntity ThrowIfUserNotFoundOrGet(int id)
    {
         if (!repository.ExistsById(id)) 
            throw new ApplicationException("Usuário não encontrado.");
        
        return repository.FindById(id);
    }
}