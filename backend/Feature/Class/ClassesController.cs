using AutoMapper;
using EduAdmin.Common;
using EduAdmin.Common.Model;
using EduAdmin.Feature.Class;
using EduAdmin.Feature.Class.DTO;
using EduAdmin.Feature.Class.Service;
using EduAdmin.Features.Class;
using Microsoft.AspNetCore.Mvc;

namespace HubEscolar.Feature.Class;

[ApiController]
[Route("[controller]")]
public class ClassesController(IClassService service) : ControllerBase
{

      [HttpGet]
    public ActionResult<BaseResponse<List<ClassResponseDTO>>> Get([FromQuery] PageRequest pageRequest)
    {
        return Ok(BaseResponse<List<ClassResponseDTO>>.WithSuccess(service.FindAll(pageRequest)));
    }

    [HttpGet("{id:int}")]
    public ActionResult<BaseResponse<ClassResponseDTO>> Get(int id)
    {
        return Ok(BaseResponse<ClassResponseDTO>.WithSuccess(service.FindById(id)));
    }

    [HttpPost]
    public ActionResult<BaseResponse<bool>> Post(ClassRequestDTO request)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.Create(request)));
    }

    [HttpPut("{id:int}")]
    public ActionResult<BaseResponse<bool>> Put(int id, ClassRequestDTO request)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.Update(id, request)));
    }

    [HttpDelete("{id:int}")]
    public ActionResult<BaseResponse<bool>> Delete(int id)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.DeleteById(id)));
    }

}