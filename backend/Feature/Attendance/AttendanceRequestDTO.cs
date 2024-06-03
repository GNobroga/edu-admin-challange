using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EduAdmin.Feature.Attendance;

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public record AttendanceRequestDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O Estudante é obrigatório")]
    [JsonPropertyName("student_id")]
    public int? StudentId { get; set; }

    [Required(ErrorMessage = "A Data é obrigatória")]
    public DateOnly Data { get; set; }

    [Required(ErrorMessage = "O Presente é obrigatório")]
    public bool Present { get; set; }
}
