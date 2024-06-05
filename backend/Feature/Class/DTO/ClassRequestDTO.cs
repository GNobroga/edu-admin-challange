using System.ComponentModel.DataAnnotations;

namespace EduAdmin.Feature.Class.DTO;

public class ClassRequestDTO
{   
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string? Name { get; set; }
}