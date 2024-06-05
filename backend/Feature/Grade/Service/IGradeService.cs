using EduAdmin.Common.Base;
using EduAdmin.Feature.Grade.DTO;

namespace EduAdmin.Feature.Grade.Service;

public interface IGradeService : IBaseService<GradeRequestDTO, GradeResponseDTO>
{
    IEnumerable<GradeResponseDTO> FindByStudentId(int id);
}