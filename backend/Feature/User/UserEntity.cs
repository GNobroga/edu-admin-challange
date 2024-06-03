using EduAdmin.Common;
using EduAdmin.Features.Grade;

namespace EduAdmin.Features.User;

public class UserEntity : BaseEntity<int> 
{

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Type { get; set; } = UserType.TEACHER; 

    public List<GradeEntity> Grades { get; set; } = [];

    public static class UserType {

        public const string STUDENT = "Student";

        public const string TEACHER = "Teacher";
    } 
}