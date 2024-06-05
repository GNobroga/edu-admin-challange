
using EduAdmin.Common.Model;
using EduAdmin.Feature.User.DTO;
using EduAdmin.Feature.User.Service;
using Microsoft.AspNetCore.Mvc;
namespace EduAdmin.Feature.User;

[Route("[controller]")]
[ApiController]
public class UsersController(IUserService service) : ControllerBase
{
    [HttpGet("search")]
    public ActionResult<BaseResponse<List<UserResponseDTO>>> Get([FromQuery] string term)
    {
        return Ok(BaseResponse<List<UserResponseDTO>>.WithSuccess(service.Search(term)));
    }

    [HttpGet]
    public ActionResult<BaseResponse<List<UserResponseDTO>>> Get([FromQuery] PageRequest pageRequest)
    {
        return Ok(BaseResponse<List<UserResponseDTO>>.WithSuccess(service.FindAll(pageRequest)));
    }

    [HttpGet("{id:int}")]
    public ActionResult<BaseResponse<UserResponseDTO>> Get(int id)
    {
        return Ok(BaseResponse<UserResponseDTO>.WithSuccess(service.FindById(id)));
    }

    [HttpPost]
    public ActionResult<BaseResponse<bool>> Post(UserRequestDTO request)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.Create(request)));
    }

    [HttpPut("{id:int}")]
    public ActionResult<BaseResponse<bool>> Put(int id, UserRequestDTO request)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.Update(id, request)));
    }

    [HttpDelete("{id:int}")]
    public ActionResult<BaseResponse<bool>> Delete(int id)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.DeleteById(id)));
    }
}
