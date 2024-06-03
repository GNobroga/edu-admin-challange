using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EduAdmin.Feature.Grade;

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public record GradeRequestDTO
{
    [Required(ErrorMessage = "O Estudante é obrigatório")]
    [JsonPropertyName("student_id")]
    public int? StudentId { get; set; }

    [Required(ErrorMessage = "A Disciplina é obrigatório")]
    [JsonPropertyName("subject_id")]
    public int? SubjectId { get; set; }

    [Required(ErrorMessage = "A Nota é obrigatório")]
    [Range(0, 100, ErrorMessage = "A Nota deve estar entre 0 e 100")]
    public double? Value { get; set; }

    [Required(ErrorMessage = "A Data é obrigatória")]
    public DateOnly Data { get; set; }
}
