using System.ComponentModel.DataAnnotations;

namespace EduAdmin.Feature.User.DTO;

public class UserRequestDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    public string? Email { get; set; }

    [RegularExpression(@"^(TEACHER|STUDENT)$", ErrorMessage = "O tipo da pessoa deve ser TEACHER ou STUDENT")]
    [Required(ErrorMessage = "O tipo da pessoa é obrigatório.")]
    public string? Type { get; set; }
}
