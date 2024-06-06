using AutoMapper;
using EduAdmin.Common.Model;
using EduAdmin.Context;
using EduAdmin.Feature.Grade.DTO;
using EduAdmin.Feature.Grade.Repository;
using EduAdmin.Feature.Subject.Repository;
using EduAdmin.Feature.User.Repository;
using EduAdmin.Features.Grade;
using EduAdmin.Features.User;
using Microsoft.EntityFrameworkCore;

namespace EduAdmin.Feature.Grade.Service;

public class GradeService(
        IGradeRepository repository, 
        IUserRepository userRepository, 
        ISubjectRepository subjectRepository, 
        AppDbContext context,
        IMapper mapper
) : IGradeService
{
    public GradeResponseDTO Create(GradeRequestDTO record)
    {
        if (userRepository.FindByIdAndTypeStudent(record.StudentId!.Value) == null)
            throw new ApplicationException("Estudante não existe.");

        if (!subjectRepository.ExistsById(record.SubjectId!.Value))
            throw new ApplicationException("A Disciplina não existe");

        return mapper.Map<GradeResponseDTO>(repository.Create(mapper.Map<GradeEntity>(record)));
    }

    public bool DeleteById(int id) => repository.DeleteById(id);
    

    public IEnumerable<GradeResponseDTO> FindAll(PageRequest pageRequest) => mapper.Map<List<GradeResponseDTO>>(repository.FindAll(pageRequest));
    

    public GradeResponseDTO FindById(int id) => mapper.Map<GradeResponseDTO>(ThrowIfGradeNotFoundOrGet(id));
    

    public IEnumerable<GradeResponseDTO> FindByStudentId(int id) =>mapper.Map<IEnumerable<GradeResponseDTO>>(repository.FindByStudentId(id));

    public IEnumerable<AverageGradesResponseDTO> GetAverageGradesByStudentId(int id)
    {
        return context.Grades
            .Include(obj => obj.Student)
            .Include(obj => obj.Subject)
            .Where(obj => obj.Student!.Id == id)
            .GroupBy(obj => obj.Subject!.Name)
            .Select(g => new AverageGradesResponseDTO { Name = g.Key, Average = g.Average(obj => obj.Value )})
            .ToList();
    }

    public IEnumerable<GradeResponseDTO> Search(string term) => mapper.Map<List<GradeResponseDTO>>(repository.Search(term));

    public bool Update(int id, GradeRequestDTO source)
    {
        var grade = ThrowIfGradeNotFoundOrGet(id);

        if (grade.StudentId != source.StudentId)
            throw new ApplicationException("Não é possível alterar o Estudante.");

        if (!subjectRepository.ExistsById(source.SubjectId!.Value))
            throw new ApplicationException("A Disciplina não existe");

        return repository.Update(mapper.Map(source, grade));
    }

    private GradeEntity ThrowIfGradeNotFoundOrGet(int id)
    {
        if (!repository.ExistsById(id))
            throw new ApplicationException("A Nota não existe");

        return repository.FindById(id);
    }
}