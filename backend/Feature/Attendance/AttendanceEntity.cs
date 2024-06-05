using System.Text.Json.Serialization;
using EduAdmin.Common.Base;
using EduAdmin.Features.Subject;
using EduAdmin.Features.User;

namespace EduAdmin.Features.Attendance;

public class AttendanceEntity : BaseEntity<int> 
{
    public int StudentId { get; set; }

    public UserEntity? Student { get; set; }

    public DateOnly Date { get; set; }

    public bool Present { get; set; } 

    public int SubjectId { get; set; }

    public SubjectEntity? Subject { get; set; }

}