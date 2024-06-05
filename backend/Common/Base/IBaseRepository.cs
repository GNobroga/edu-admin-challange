using EduAdmin.Common.Model;

namespace EduAdmin.Common.Base;

public interface IBaseRepository<T> where T: BaseEntity<int> 
{
    T Create(T record);

    bool Update(T source);

    IEnumerable<T> FindAll(PageRequest pageRequest);
    T FindById(int id);
    bool ExistsById(int id);

    bool DeleteById(int id);
}