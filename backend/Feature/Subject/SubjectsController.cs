using System.Linq.Expressions;
using AutoMapper;
using EduAdmin.Common;
using EduAdmin.Feature.Class;
using EduAdmin.Feature.Subject;
using EduAdmin.Features.Subject;
using EduAdmin.Features.User;
using HubEscolar.Feature.User;
using Microsoft.AspNetCore.Mvc;

namespace HubEscolar.Feature.Subject;

[ApiController]
[Route("disciplinas")]
public class SubjectsController(SubjectRepository repository, ClassRepository classRepository, UserRepository userRepository,  IMapper mapper) : ControllerBase
{

    [HttpGet]
    public ActionResult<BaseResponse<IEnumerable<SubjectEntity>>> Get([FromQuery] PageRequest pageRequest)
    {
        List<Expression<Func<SubjectEntity, object>>> includes = [
            (entity) => entity.Class!,
            (entity) => entity.Teacher!,
        ];
      return  Ok(BaseResponse<IEnumerable<SubjectEntity>>.WithSuccess(repository.FindAll(pageRequest)));
    }

    [HttpGet("{id:int}")]
    public ActionResult<SubjectEntity> Get(int id)
    {
        if (!repository.ExistsById(id)) 
        {
            return BadRequest(ResponseError.With($"Disciplina com ID {id} não existe"));
        }

        return Ok(BaseResponse<SubjectEntity>.WithSuccess(repository.FindById(id)));
    }

    [HttpPut("{id:int}")]
    public ActionResult<BaseResponse<bool>> Put(int id, SubjectRequestDTO source)
    {
        if (!repository.ExistsById(id)) 
        {
            return BadRequest(ResponseError.With($"Disciplina com ID {id} não existe"));
        }

        if (!userRepository.ExistsById(source.TeacherId!.Value))
        {
             return BadRequest(ResponseError.With($"Professor com ID {source.TeacherId} não existe"));
        }

        var student = userRepository.FindById(source.TeacherId.Value);

        if (student!.Type != UserEntity.UserType.TEACHER)
        {
            return BadRequest(ResponseError.With($"Não é possível cadastrar um aluno como professor"));
        }

        var Subject = repository.FindById(id)!;

        if (Subject.TeacherId != source.TeacherId)
        {
            return BadRequest(ResponseError.With("Não é possível alterar o professor"));
        }

        if (!classRepository.ExistsById(source.ClassId!.Value))
        {
             return BadRequest(ResponseError.With($"Disciplina com ID {source.ClassId!.Value} não existe"));
        }


        return Ok(
            BaseResponse<SubjectEntity>.WithSuccess(repository.Update(mapper.Map(source, repository.FindById(id)!))
        ));
    }

    [HttpPost]
    public ActionResult<BaseResponse<SubjectEntity>> Post(SubjectRequestDTO record) {

        if (!userRepository.ExistsById(record.TeacherId!.Value))
        {
             return BadRequest(ResponseError.With($"Estudante com ID {record.TeacherId} não existe"));
        }

        if (!userRepository.ExistsById(record.TeacherId!.Value))
        {
             return BadRequest(ResponseError.With($"Professor com ID {record.TeacherId} não existe"));
        }

        var student = userRepository.FindById(record.TeacherId.Value);

        if (student!.Type != UserEntity.UserType.TEACHER)
        {
            return BadRequest(ResponseError.With($"Não é possível cadastrar um aluno como professor"));
        }

        if (!classRepository.ExistsById(record.ClassId!.Value))
        {
             return BadRequest(ResponseError.With($"Disciplina com ID {record.ClassId!.Value} não existe"));
        }

        return Ok(
            BaseResponse<SubjectEntity>.WithSuccess(repository.Create(mapper.Map<SubjectEntity>(record)))
        );
    }

}