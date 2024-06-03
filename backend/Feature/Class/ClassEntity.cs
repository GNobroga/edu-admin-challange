using EduAdmin.Common;

namespace EduAdmin.Features.Class;

// Turma
public class ClassEntity : BaseEntity<int> 
{

    public string Name { get; set; } = null!;
}