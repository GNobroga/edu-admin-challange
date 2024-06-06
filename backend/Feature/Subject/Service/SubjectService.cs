using AutoMapper;
using EduAdmin.Common.Model;
using EduAdmin.Feature.Class.Repository;
using EduAdmin.Feature.Subject.DTO;
using EduAdmin.Feature.Subject.Repository;
using EduAdmin.Feature.User.Repository;
using EduAdmin.Features.Subject;
using EduAdmin.Features.User;

namespace EduAdmin.Feature.Subject.Service;

public class SubjectService(ISubjectRepository repository, IUserRepository userRepository, IClassRepository classRepository, IMapper mapper) : ISubjectService
{
    public SubjectResponseDTO Create(SubjectRequestDTO record)
    {
        if (userRepository.FindByIdAndTypeTeacher(record.TeacherId!.Value) == null) 
            throw new ApplicationException("O Professor não existe");

        if (repository.ExistsByName(record.Name!))
            throw new ApplicationException("Já existe uma disciplina com esse nome cadastrada"); 

        if (!classRepository.ExistsById(record.ClassId!.Value))
            throw new ApplicationException("A Turma não existe");

        return mapper.Map<SubjectResponseDTO>(repository.Create(mapper.Map<SubjectEntity>(record)));
    }

    public IEnumerable<SubjectResponseDTO> Search(string term) => mapper.Map<IEnumerable<SubjectResponseDTO>>(repository.Search(term));

    public bool DeleteById(int id)
    {
        return repository.DeleteById(id);
    }

    public IEnumerable<SubjectResponseDTO> FindAll(PageRequest pageRequest)
    {
       return mapper.Map<List<SubjectResponseDTO>>(repository.FindAll(pageRequest));
    }

    public SubjectResponseDTO FindById(int id)
    {
        return mapper.Map<SubjectResponseDTO>(ThrowIfSubjectNotFoundOrGet(id));
    }

    public bool Update(int id, SubjectRequestDTO source)
    {
        var subject = ThrowIfSubjectNotFoundOrGet(id);

        if (userRepository.FindByIdAndTypeTeacher(source.TeacherId!.Value) == null) 
            throw new ApplicationException("O Professor não existe");

        if (repository.ExistsByName(source.Name!) && !string.Equals(source.Name, subject.Name, StringComparison.OrdinalIgnoreCase)) 
            throw new ApplicationException("Já existe uma disciplina com esse nome cadastrada"); 

        if (!classRepository.ExistsById(source.ClassId!.Value))
            throw new ApplicationException("A Turma não existe");

        return repository.Update(mapper.Map(source, subject));
    }

    private SubjectEntity ThrowIfSubjectNotFoundOrGet(int id) 
    {
        if (!repository.ExistsById(id)) 
            throw new ApplicationException("A Disciplina não existe");

        return repository.FindById(id);
    }

    public IEnumerable<SubjectResponseDTO> FindByTeacherId(int id)
    {
        return mapper.Map<List<SubjectResponseDTO>>(repository.FindByTeacherId(id));
    }
}