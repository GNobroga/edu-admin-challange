using System.ComponentModel.DataAnnotations;

namespace HubEscolar.Feature.User;

public record UserRequestDTO(
    [Required(ErrorMessage = "O nome é obrigatório")]
    string Name,
    [EmailAddress(ErrorMessage = "O email é obrigatório")]
    string Email,
    [RegularExpression(@"^(TEACHER|STUDENT)$", ErrorMessage = "O tipo deve ser TEACHER ou STUDENT")]
    string Type
) {}