
using System.ComponentModel.DataAnnotations;

namespace EduAdmin.Feature.Grade.DTO;
public class  GradeRequestDTO 
{

    [Required(ErrorMessage = "O Estudante é obrigatório")]
    public int? StudentId { get; set;} 

    [Required(ErrorMessage = "A Disciplina é obrigatório")]
    public int? SubjectId { get; set;} 

    [Required(ErrorMessage = "A Nota é obrigatório")]
    [Range(0, 100, ErrorMessage = "A Nota deve estar entre 0 e 100")]
    public double? Value { get; set;} 
}

