using AutoMapper;
using EduAdmin.Common;
using EduAdmin.Features.User;
using Microsoft.AspNetCore.Mvc;

namespace HubEscolar.Feature.User;

[ApiController]
[Route("usuarios")]
public class UsersController(UserRepository repository, IMapper mapper) : ControllerBase
{

    [HttpGet]
    public ActionResult<BaseResponse<IEnumerable<UserEntity>>> Get([FromQuery] PageRequest pageRequest) => Ok(BaseResponse<IEnumerable<UserEntity>>.WithSuccess(repository.FindAll(pageRequest)));

    [HttpGet("{id:int}")]
    public ActionResult<UserEntity> Get(int id)
    {
        if (!repository.ExistsById(id)) 
        {
            return BadRequest(ResponseError.With($"Usuário com ID {id} não existe"));
        }

        return Ok(BaseResponse<UserEntity>.WithSuccess(repository.FindById(id)));
    }

    [HttpPut("{id:int}")]
    public ActionResult<BaseResponse<bool>> Put(int id, UserRequestDTO source)
    {
         if (!repository.ExistsById(id)) 
        {
            return BadRequest(ResponseError.With($"Usuário com ID {id} não existe"));
        }

        var user = repository.FindById(id)!;

        if (repository.ExistsByEmail(source.Email) && !user.Email.Equals(source.Email, StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(ResponseError.With("Email já em uso"));
        }

        return Ok(
            BaseResponse<UserEntity>.WithSuccess(repository.Update(mapper.Map(source, user))
        ));
    }

    [HttpPost]
    public ActionResult<BaseResponse<UserEntity>> Post(UserRequestDTO record) {
        if (repository.ExistsByEmail(record.Email))
        {
            return BadRequest(ResponseError.With("Email já em uso"));
        }

        return Ok(
            BaseResponse<UserEntity>.WithSuccess(repository.Create(mapper.Map<UserEntity>(record)))
        );
    }

    [HttpDelete("{id:int}")]
    public ActionResult<BaseResponse<bool>> Delete(int id) 
    {
        return Ok(BaseResponse<bool>.WithSuccess(repository.DeleteById(id)));
    }

}