using EduAdmin.Common.Model;

namespace EduAdmin.Common.Base;

public interface IBaseService<Request, Response> 
{
    Response Create(Request record);

    bool Update(int id, Request source);

    IEnumerable<Response> FindAll(PageRequest pageRequest);
    Response FindById(int id);

    bool DeleteById(int id);
}