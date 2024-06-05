using EduAdmin.Common.Base;
using EduAdmin.Feature.Class.DTO;

namespace EduAdmin.Feature.Class.Service;

public interface IClassService : IBaseService<ClassRequestDTO, ClassResponseDTO> {

    IEnumerable<ClassResponseDTO> Search(string term);
}