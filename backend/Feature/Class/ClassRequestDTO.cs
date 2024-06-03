using System.ComponentModel.DataAnnotations;

namespace EduAdmin.Feature.Class;

public record ClassRequestDTO(
    [Required(ErrorMessage = "O Nome é obrigatório")]
    string Name
) {}