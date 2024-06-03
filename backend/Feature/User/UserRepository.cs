using AutoMapper;
using EduAdmin.Common;
using EduAdmin.Context;
using EduAdmin.Features.User;

namespace HubEscolar.Feature.User;

public class UserRepository(AppDbContext context) : GenericRepository<UserEntity>(context) {

    public bool ExistsByEmail(string email) 
    {
        return Context.Users.SingleOrDefault(obj => obj.Email.Equals(email.Trim())) != null;
    }

}