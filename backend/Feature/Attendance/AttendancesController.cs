using EduAdmin.Common.Model;
using EduAdmin.Feature.Attendance.DTO;
using EduAdmin.Feature.Attendance.Service;
using Microsoft.AspNetCore.Mvc;

namespace HubEscolar.Feature.Attendance;

[ApiController]
[Route("[controller]")]
public class AttendancesController(IAttendanceService service) : ControllerBase
{
     [HttpGet]
    public ActionResult<BaseResponse<List<AttendanceResponseDTO>>> Get([FromQuery] PageRequest pageRequest)
    {
        return Ok(BaseResponse<List<AttendanceResponseDTO>>.WithSuccess(service.FindAll(pageRequest)));
    }

    [HttpGet("{id:int}")]
    public ActionResult<BaseResponse<AttendanceResponseDTO>> Get(int id)
    {
        return Ok(BaseResponse<AttendanceResponseDTO>.WithSuccess(service.FindById(id)));
    }

    [HttpPost]
    public ActionResult<BaseResponse<bool>> Post(AttendanceRequestDTO request)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.Create(request)));
    }

    [HttpPut("{id:int}")]
    public ActionResult<BaseResponse<bool>> Put(int id, AttendanceRequestDTO request)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.Update(id, request)));
    }

    [HttpDelete("{id:int}")]
    public ActionResult<BaseResponse<bool>> Delete(int id)
    {
        return Ok(BaseResponse<bool>.WithSuccess(service.DeleteById(id)));
    }

    [HttpGet("search")]
    public ActionResult<BaseResponse<List<AttendanceResponseDTO>>> Get([FromQuery] string term)
    {
        return Ok(BaseResponse<List<AttendanceResponseDTO>>.WithSuccess(service.Search(term)));
    }

}