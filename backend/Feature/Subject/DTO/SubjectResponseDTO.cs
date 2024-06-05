namespace EduAdmin.Feature.Subject.DTO;


public class SubjectClassResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class SubjectTeacherResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class SubjectResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public SubjectClassResponseDTO Class { get; set; } = null!;
    public SubjectTeacherResponseDTO Teacher { get; set; } = null!;
}
