namespace EduAdmin.Feature.Grade.DTO;


public class GradeStudentResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class GradeSubjectTeacherResponseDTO 
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}

public class GradeSubjectResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public GradeSubjectTeacherResponseDTO Teacher { get; set; } = null!;
}

public class GradeResponseDTO
{
    public int Id { get; set; }
    public double Value { get; set; }
    public GradeStudentResponseDTO Student { get; set; } = null!;
    public GradeSubjectResponseDTO Subject { get; set; } = null!;
}
