
using EduAdmin.Common.Base;
using EduAdmin.Features.User;

namespace EduAdmin.Feature.User.Repository;

public interface IUserRepository : IBaseRepository<UserEntity> 
{
    public bool ExistsByEmail(string email);
    public IEnumerable<UserEntity> Search(string term);

    public UserEntity? FindByIdAndTypeStudent(int id);

    public UserEntity? FindByIdAndTypeTeacher(int id);

}