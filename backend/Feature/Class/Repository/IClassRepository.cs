using EduAdmin.Common.Base;
using EduAdmin.Features.Class;

namespace EduAdmin.Feature.Class.Repository;

public interface IClassRepository : IBaseRepository<ClassEntity> {

    IEnumerable<ClassEntity> Search(string term);
}