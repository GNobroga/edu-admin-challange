namespace EduAdmin.Feature.Attendance.DTO;

public class AttendanceStudentResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class AttendanceSubjectResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

public class AttendanceResponseDTO
{
    public int Id { get; set; }
    public AttendanceStudentResponseDTO Student { get; set; } = null!;
    public DateOnly Date { get; set; }
    public bool Present { get; set; }
    public AttendanceSubjectResponseDTO Subject { get; set; } = null!;
}
