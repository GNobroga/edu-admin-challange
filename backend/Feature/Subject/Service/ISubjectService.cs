using EduAdmin.Common.Base;
using EduAdmin.Feature.Subject.DTO;

namespace EduAdmin.Feature.Subject.Service;

public interface ISubjectService : IBaseService<SubjectRequestDTO, SubjectResponseDTO>
{
    IEnumerable<SubjectResponseDTO> Search(string term);

    IEnumerable<SubjectResponseDTO> FindByTeacherId(int id);
}