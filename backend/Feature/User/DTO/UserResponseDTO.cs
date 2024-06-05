namespace EduAdmin.Feature.User.DTO;

public class UserResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Type { get; set; } = null!;
}
