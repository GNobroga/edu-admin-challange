
namespace EduAdmin.Common;

public class BaseResponse<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; }
    public T Data { get; set; }

    public BaseResponse(string message, T data)
    {
        Message = message;
        Data = data;
    }

    public static BaseResponse<U> WithSuccess<U>(U data) => new BaseResponse<U>("Operação realizada com sucesso.", data);

}
