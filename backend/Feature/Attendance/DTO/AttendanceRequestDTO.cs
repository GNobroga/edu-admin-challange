namespace EduAdmin.Feature.Attendance.DTO;

using System;
using System.ComponentModel.DataAnnotations;


public record AttendanceRequestDTO
{
    [Required(ErrorMessage = "O Estudante é obrigatório")]
    public int? StudentId { get; set; }

    [Required(ErrorMessage = "A Data é obrigatória")]
    public DateOnly? Date { get; set; }

    [Required(ErrorMessage = "O Presente é obrigatório")]
    public bool? Present { get; set; }

    [Required(ErrorMessage = "A Disciplina é obrigatório")]
    public int? SubjectId { get; set; }
}
