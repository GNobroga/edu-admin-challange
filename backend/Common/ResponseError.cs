namespace EduAdmin.Common;

public record ResponseError(
    string Title,
    string Message,
    int StatusCode
) {

    public static ResponseError With(string title, string message, int statusCode) => new(title, message, statusCode);
    public static ResponseError With(string message, int statusCode = 400) => With("Application Error", message, statusCode);

    
}