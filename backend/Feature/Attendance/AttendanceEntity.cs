using System.Text.Json.Serialization;
using EduAdmin.Common;
using EduAdmin.Features.User;

namespace EduAdmin.Features.Attendance;

public class AttendanceEntity : BaseEntity<int> 
{
    [JsonIgnore]
    public int StudentId { get; set; }

    public UserEntity? Student { get; set; }

    public DateOnly Date { get; set; }

    public bool Present { get; set; } 
}