using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using EduAdmin.Common;
using EduAdmin.Feature.Attendance;
using EduAdmin.Features.Attendance;
using EduAdmin.Features.User;
using HubEscolar.Feature.User;
using Microsoft.AspNetCore.Mvc;

namespace HubEscolar.Feature.Attendance;

[ApiController]
[Route("chamadas")]
public class AttendancesController(AttendanceRepository repository, UserRepository userRepository,  IMapper mapper) : ControllerBase
{

    [HttpGet]
    public ActionResult<BaseResponse<IEnumerable<AttendanceEntity>>> Get([FromQuery] PageRequest pageRequest) 
    {
        List<Expression<Func<AttendanceEntity, object>>> includes = [
            (entity) => entity.Student!,
        ];
        return Ok(BaseResponse<IEnumerable<AttendanceEntity>>.WithSuccess(repository.FindAll(pageRequest, includes)));
    }

    [HttpGet("{id:int}")]
    public ActionResult<AttendanceEntity> Get(int id)
    {
        if (!repository.ExistsById(id)) 
        {
            return BadRequest(ResponseError.With($"Frequência com ID {id} não existe"));
        }

        List<Expression<Func<AttendanceEntity, object>>> includes = [
            (entity) => entity.Student!,
        ];

        return Ok(BaseResponse<AttendanceEntity>.WithSuccess(repository.FindById(id, includes)));
    }

    [HttpPut("{id:int}")]
    public ActionResult<BaseResponse<bool>> Put(int id, AttendanceRequestDTO source)
    {
        if (!repository.ExistsById(id)) 
        {
            return BadRequest(ResponseError.With($"Frequência com ID {id} não existe"));
        }

        if (!userRepository.ExistsById(source.StudentId!.Value))
        {
             return BadRequest(ResponseError.With($"Estudante com ID {source.StudentId} não existe"));
        }
        
        var attendance = repository.FindById(id)!;

        if (attendance.StudentId != source.StudentId)
        {
            return BadRequest(ResponseError.With("Não é possível alterar o estudante"));
        }

        return Ok(
            BaseResponse<AttendanceEntity>.WithSuccess(repository.Update(mapper.Map(source, repository.FindById(id)!))
        ));
    }

    [HttpPost]
    public ActionResult<BaseResponse<AttendanceEntity>> Post(AttendanceRequestDTO record) {

        if (!userRepository.ExistsById(record.StudentId!.Value))
        {
             return BadRequest(ResponseError.With($"Estudante com ID {record.StudentId} não existe"));
        }

        var student = userRepository.FindById(record.StudentId.Value);

        if (student!.Type != UserEntity.UserType.STUDENT)
        {
            return BadRequest(ResponseError.With($"Não é possível cadastrar um professor como estudante"));
        }

        return Ok(
            BaseResponse<AttendanceEntity>.WithSuccess(repository.Create(mapper.Map<AttendanceEntity>(record)))
        );
    }

}