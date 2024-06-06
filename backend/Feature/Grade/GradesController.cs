
using EduAdmin.Common.Model;
using EduAdmin.Feature.Grade.DTO;
using EduAdmin.Feature.Grade.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HubEscolar.Feature.Grade;


[Route("[controller]")]
[ApiController]
public class GradesController(IGradeService service) : ControllerBase
{
    [HttpGet]
    public ActionResult<BaseResponse<List<GradeResponseDTO>>> Get([FromQuery] PageRequest pageRequest)
    {
        return Ok(BaseResponse<List<GradeResponseDTO>>.WithSuccess(service.FindAll(pageRequest)));
    }

    [HttpGet("{id:int}")]
    public ActionResult<BaseResponse<GradeResponseDTO>> Get(int id)
    {
        return Ok(BaseResponse<GradeResponseDTO>.WithSuccess(service.FindById(id)));
    }

    [HttpPost]
    public ActionResult<BaseResponse<bool>> Post(GradeRequestDTO request)
    {   
        return Ok(BaseResponse<bool>.WithSuccess(service.Create(request)));
    }

    [HttpPut("{id:int}")]
    public ActionResult<BaseResponse<bool>> Put(int id, GradeRequestDTO request)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.Update(id, request)));
    }

    [HttpDelete("{id:int}")]
    public ActionResult<BaseResponse<bool>> Delete(int id)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.DeleteById(id)));
    }

    [HttpGet("student/{id:int}")]
    public ActionResult<BaseResponse<IEnumerable<GradeResponseDTO>>> GetByStudentId(int id)
    {
        return Ok(BaseResponse<IEnumerable<GradeResponseDTO>>.WithSuccess(service.FindByStudentId(id)));
    }

    [HttpGet("search")]
    public ActionResult<BaseResponse<IEnumerable<GradeResponseDTO>>> Search(string term)
    {
        return Ok(BaseResponse<IEnumerable<GradeResponseDTO>>.WithSuccess(service.Search(term)));
    }

    [HttpGet("student/{id:int}/average")]
    public ActionResult<BaseResponse<IEnumerable<AverageGradesResponseDTO>>> Average(int id)
    {
        return Ok(BaseResponse<IEnumerable<AverageGradesResponseDTO>>.WithSuccess(service.GetAverageGradesByStudentId(id)));
    }

}