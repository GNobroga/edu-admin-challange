using EduAdmin.Common.Model;
using EduAdmin.Feature.Subject.DTO;
using EduAdmin.Feature.Subject.Service;
using Microsoft.AspNetCore.Mvc;

namespace HubEscolar.Feature.Subject;

[ApiController]
[Route("[controller]")]
public class SubjectsController(ISubjectService service) : ControllerBase
{

    [HttpGet("teacher/{id:int}")]
    public ActionResult<BaseResponse<IEnumerable<SubjectResponseDTO>>> GetByTeacherId(int id)
    {
        return Ok(BaseResponse<IEnumerable<SubjectResponseDTO>>.WithSuccess(service.FindByTeacherId(id)));
    }


    [HttpGet("search")]
    public ActionResult<BaseResponse<List<SubjectResponseDTO>>> Get([FromQuery] string term)
    {
        return Ok(BaseResponse<List<SubjectResponseDTO>>.WithSuccess(service.Search(term)));
    }

    [HttpGet]
    public ActionResult<BaseResponse<List<SubjectResponseDTO>>> Get([FromQuery] PageRequest pageRequest)
    {
        return Ok(BaseResponse<List<SubjectResponseDTO>>.WithSuccess(service.FindAll(pageRequest)));
    }

    [HttpGet("{id:int}")]
    public ActionResult<BaseResponse<SubjectResponseDTO>> Get(int id)
    {
        return Ok(BaseResponse<SubjectResponseDTO>.WithSuccess(service.FindById(id)));
    }

    [HttpPost]
    public ActionResult<BaseResponse<bool>> Post(SubjectRequestDTO request)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.Create(request)));
    }

    [HttpPut("{id:int}")]
    public ActionResult<BaseResponse<bool>> Put(int id, SubjectRequestDTO request)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.Update(id, request)));
    }

    [HttpDelete("{id:int}")]
    public ActionResult<BaseResponse<bool>> Delete(int id)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.DeleteById(id)));
    }

}