using EduAdmin.Common;
using EduAdmin.Features.Class;
using EduAdmin.Features.User;

namespace EduAdmin.Features.Subject;

public class SubjectEntity : BaseEntity<int> 
{

    public string Name { get; set; } = null!;

    public int ClassId { get; set; }

    public ClassEntity? Class { get; set; }

    public int TeacherId { get; set; }

    public UserEntity? Teacher { get; set; }


}