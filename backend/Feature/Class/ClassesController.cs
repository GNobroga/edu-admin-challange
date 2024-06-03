using AutoMapper;
using EduAdmin.Common;
using EduAdmin.Feature.Class;
using EduAdmin.Features.Class;
using Microsoft.AspNetCore.Mvc;

namespace HubEscolar.Feature.Class;

[ApiController]
[Route("turmas")]
public class ClassesController(ClassRepository repository, IMapper mapper) : ControllerBase
{

    [HttpGet]
    public ActionResult<BaseResponse<IEnumerable<ClassEntity>>> Get([FromQuery] PageRequest pageRequest) => Ok(BaseResponse<IEnumerable<ClassEntity>>.WithSuccess(repository.FindAll(pageRequest)));

    [HttpGet("{id:int}")]
    public ActionResult<ClassEntity> Get(int id)
    {
        if (!repository.ExistsById(id)) 
        {
            return BadRequest(ResponseError.With($"Turma com ID {id} não existe"));
        }

        return Ok(BaseResponse<ClassEntity>.WithSuccess(repository.FindById(id)));
    }

    [HttpPut("{id:int}")]
    public ActionResult<BaseResponse<bool>> Put(int id, ClassRequestDTO source)
    {
         if (!repository.ExistsById(id)) 
        {
            return BadRequest(ResponseError.With($"Turma com ID {id} não existe"));
        }
        return Ok(
            BaseResponse<ClassEntity>.WithSuccess(repository.Update(mapper.Map(source, repository.FindById(id)!))
        ));
    }

    [HttpPost]
    public ActionResult<BaseResponse<ClassEntity>> Post(ClassRequestDTO record) {
        return Ok(
            BaseResponse<ClassEntity>.WithSuccess(repository.Create(mapper.Map<ClassEntity>(record)))
        );
    }

}