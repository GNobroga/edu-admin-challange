using EduAdmin.Common;
using EduAdmin.Common.Base;
using EduAdmin.Features.Subject;
using EduAdmin.Features.User;

namespace EduAdmin.Features.Grade;

public class GradeEntity : BaseEntity<int> 
{
    public double Value { get; set; } 

    public int StudentId { get; set; }

    public UserEntity? Student { get; set; }

    public int SubjectId { get; set; }

    public SubjectEntity? Subject { get; set; }
}