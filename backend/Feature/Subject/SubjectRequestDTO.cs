using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EduAdmin.Feature.Subject;

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public record SubjectRequestDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O Professor é obrigatório")]
    [JsonPropertyName("teacher_id")]
    public int? TeacherId { get; set; }

    [Required(ErrorMessage = "A Turma é obrigatório")]
    [JsonPropertyName("class_id")]
    public int? ClassId { get; set; }
}   
