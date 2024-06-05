using AutoMapper;
using EduAdmin.Common.Model;
using EduAdmin.Feature.Attendance.DTO;
using EduAdmin.Feature.Attendance.Repository;
using EduAdmin.Feature.Subject.Repository;
using EduAdmin.Feature.User.Repository;
using EduAdmin.Features.Attendance;


namespace EduAdmin.Feature.Attendance.Service;


public class AttendanceService(IAttendanceRepository repository, IUserRepository userRepository, ISubjectRepository subjectRepository, IMapper mapper) : IAttendanceService
{
    public AttendanceResponseDTO Create(AttendanceRequestDTO record)
    {
        _ = userRepository.FindByIdAndTypeStudent(record.StudentId!.Value) ?? throw new ApplicationException("O estudante não existe.");
        _ = subjectRepository.FindById(record.SubjectId!.Value) ?? throw new ApplicationException("O disciplina não existe.");
        return mapper.Map<AttendanceResponseDTO>(repository.Create(mapper.Map<AttendanceEntity>(record)));
    }

    public bool DeleteById(int id)
    {
        return repository.DeleteById(id);
    }

    public IEnumerable<AttendanceResponseDTO> FindAll(PageRequest pageRequest) => mapper.Map<List<AttendanceResponseDTO>>(repository.FindAll(pageRequest));
    

    public AttendanceResponseDTO FindById(int id)
    {
        if (!repository.ExistsById(id)) throw new ApplicationException("Chamada não encontrada");
        return mapper.Map<AttendanceResponseDTO>(repository.FindById(id));
    }

    public bool Update(int id, AttendanceRequestDTO source)
    {
        var entity = repository.FindById(id) ?? throw new ApplicationException("Chamada não encontrada");

        if (entity.StudentId != source.StudentId)
            throw new ApplicationException("Não é permitido alterar o estudante da chamada.");
        
        _ = subjectRepository.FindById(source.SubjectId!.Value) ?? throw new ApplicationException("O disciplina não existe.");

        return repository.Update(mapper.Map(source, entity));
    }
}