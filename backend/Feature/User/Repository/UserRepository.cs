
using EduAdmin.Common.Model;
using EduAdmin.Context;
using EduAdmin.Feature.User.Repository;
using EduAdmin.Features.User;

namespace HubEscolar.Feature.User.Repository;

public class UserRepository(AppDbContext context) : IUserRepository  {

    public UserEntity Create(UserEntity record) 
    {
        context.Users.Add(record);
        context.SaveChanges();
        return record;
    }

    public bool Update(UserEntity source) {
        context.Users.Update(source);
        return context.SaveChanges() > 0;
    }

    public IEnumerable<UserEntity> FindAll(PageRequest pageRequest) 
    {
         return context.Users.Skip((pageRequest.Page - 1) * pageRequest.Size).Take(pageRequest.Size).OrderBy(obj => obj.Id).ToList();
    }

    public UserEntity FindById(int id) => context.Users.FirstOrDefault(entity => entity.Id == id)!;
    
    public bool ExistsById(int id) => FindById(id) != null;

    public bool DeleteById(int id)
    {
        if (ExistsById(id)) {
            context.Users.Remove(FindById(id));
            return context.SaveChanges() > 0;
        }
        return false;
    }


    public bool ExistsByEmail(string email) 
    {
        return context.Users.SingleOrDefault(obj => obj.Email.Equals(email.Trim())) != null;
    }

    public IEnumerable<UserEntity> Search(string term) 
    {
        if (string.IsNullOrWhiteSpace(term)) return [];
        term = term.ToLower();
        return context.Users
            .Where(user => user.Name.ToLower().Contains(term) || user.Email.ToLower().Contains(term))
            .ToList();
    }

    public UserEntity? FindByIdAndTypeStudent(int id)
    {
        return context.Users.SingleOrDefault(user => user.Id == id && user.Type == UserType.STUDENT);
    }

    public UserEntity? FindByIdAndTypeTeacher(int id)
    {
        return context.Users.SingleOrDefault(user => user.Id == id && user.Type == UserType.TEACHER);
    }
}