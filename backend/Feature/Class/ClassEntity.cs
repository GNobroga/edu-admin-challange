using EduAdmin.Common;
using EduAdmin.Common.Base;

namespace EduAdmin.Features.Class;

// Turma
public class ClassEntity : BaseEntity<int> 
{
    public string Name { get; set; } = null!;
}