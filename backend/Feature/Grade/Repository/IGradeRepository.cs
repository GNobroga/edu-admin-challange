using EduAdmin.Common.Base;
using EduAdmin.Features.Grade;

namespace EduAdmin.Feature.Grade.Repository;

public interface IGradeRepository : IBaseRepository<GradeEntity> {

    IEnumerable<GradeEntity> FindByStudentId(int id);
    IEnumerable<GradeEntity> Search(string term);

}