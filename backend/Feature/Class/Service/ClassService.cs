using AutoMapper;
using EduAdmin.Common.Model;
using EduAdmin.Feature.Class.DTO;
using EduAdmin.Feature.Class.Repository;
using EduAdmin.Features.Class;
using EduAdmin.Features.User;

namespace EduAdmin.Feature.Class.Service;

public class ClassService(IClassRepository repository, IMapper mapper) : IClassService
{
    public ClassResponseDTO Create(ClassRequestDTO record)
    {
        return mapper.Map<ClassResponseDTO>(repository.Create(mapper.Map<ClassEntity>(record)));
    }

    public bool DeleteById(int id)
    {
        return repository.DeleteById(id);
    }

    public IEnumerable<ClassResponseDTO> FindAll(PageRequest pageRequest)
    {
        return mapper.Map<List<ClassResponseDTO>>(repository.FindAll(pageRequest));
    }

    public ClassResponseDTO FindById(int id)
    {
        if (!repository.ExistsById(id))
            throw new ApplicationException("A sala não existe.");
        
        return mapper.Map<ClassResponseDTO>(repository.FindById(id));
    }

    public bool Update(int id, ClassRequestDTO source)
    {
        if (!repository.ExistsById(id))
            throw new ApplicationException("A sala não existe.");
        
        var entity = repository.FindById(id);

        return repository.Update(mapper.Map(source, entity));
    }
}