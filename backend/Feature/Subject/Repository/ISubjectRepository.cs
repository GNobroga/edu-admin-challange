using EduAdmin.Common.Base;
using EduAdmin.Features.Subject;

namespace EduAdmin.Feature.Subject.Repository;

public interface ISubjectRepository : IBaseRepository<SubjectEntity> {

    IEnumerable<SubjectEntity> Search(string term);

    IEnumerable<SubjectEntity> FindByTeacherId(int id);
}