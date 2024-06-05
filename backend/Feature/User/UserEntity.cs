using EduAdmin.Common.Base;
using EduAdmin.Features.Grade;

namespace EduAdmin.Features.User;

public static class UserType {

    public const string STUDENT = "STUDENT";

    public const string TEACHER = "TEACHER";
} 
public class UserEntity : BaseEntity<int> 
{

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Type { get; set; } = null!;

    public List<GradeEntity> Grades { get; set; } = [];

}