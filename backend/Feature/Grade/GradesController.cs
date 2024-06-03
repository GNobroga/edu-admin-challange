using System.Linq.Expressions;
using AutoMapper;
using EduAdmin.Common;
using EduAdmin.Feature.Grade;
using EduAdmin.Feature.Subject;
using EduAdmin.Features.Grade;
using EduAdmin.Features.User;
using HubEscolar.Feature.User;
using Microsoft.AspNetCore.Mvc;

namespace HubEscolar.Feature.Grade;

[ApiController]
[Route("notas")]
public class GradesController(GradeRepository repository, UserRepository userRepository, SubjectRepository subjectRepository,  IMapper mapper) : ControllerBase
{

    [HttpGet]
    public ActionResult<BaseResponse<IEnumerable<GradeEntity>>> Get([FromQuery] PageRequest pageRequest)
    {
        return Ok(BaseResponse<IEnumerable<GradeEntity>>.WithSuccess(repository.FindAll(pageRequest)));
    }

    [HttpGet("{id:int}")]
    public ActionResult<GradeEntity> Get(int id)
    {
        if (!repository.ExistsById(id)) 
        {
            return BadRequest(ResponseError.With($"Nota com ID {id} não existe"));
        }

        return Ok(BaseResponse<GradeEntity>.WithSuccess(repository.FindById(id)));
    }

    [HttpPut("{id:int}")]
    public ActionResult<BaseResponse<bool>> Put(int id, GradeRequestDTO source)
    {
        if (!repository.ExistsById(id)) 
        {
            return BadRequest(ResponseError.With($"Nota com ID {id} não existe"));
        }

        var grade = repository.FindById(id)!;
        var studentId = source.StudentId!.Value;
        var subjectId = source.SubjectId!.Value;

        if (subjectId != grade.SubjectId) 
        {
            return BadRequest(ResponseError.With("Não é permitido alterar a disciplina"));
        }

        if (studentId != grade.StudentId) 
        {
            return BadRequest(ResponseError.With("Não é permitido alterar o estudante"));
        }

        return Ok(
            BaseResponse<GradeEntity>.WithSuccess(repository.Update(mapper.Map(source, repository.FindById(id)!))
        ));
    }

    [HttpPost]
    public ActionResult<BaseResponse<GradeEntity>> Post(GradeRequestDTO record) {

        if (!userRepository.ExistsById(record.StudentId!.Value))
        {
             return BadRequest(ResponseError.With($"Usuário com ID {record.StudentId} não existe."));
        }

        var student = userRepository.FindById(record.StudentId.Value);

        if (student!.Type != UserEntity.UserType.STUDENT)
        {
            return BadRequest(ResponseError.With("O Usuário não é um estudante."));
        }

        if (!subjectRepository.ExistsById(record.SubjectId!.Value))
        {
            return BadRequest(ResponseError.With("A Disciplina não existe."));
        }

        return Ok(
            BaseResponse<GradeEntity>.WithSuccess(repository.Create(mapper.Map<GradeEntity>(record)))
        );
    }

}