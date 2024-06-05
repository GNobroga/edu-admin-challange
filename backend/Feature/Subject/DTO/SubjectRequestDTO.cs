namespace EduAdmin.Feature.Subject.DTO;


using System.ComponentModel.DataAnnotations;

public class SubjectRequestDTO
{   
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O professor é obrigatório.")]
    public int? TeacherId { get; set; }

    [Required(ErrorMessage = "A turma é obrigatório.")]
    public int? ClassId { get; set; }
}   
