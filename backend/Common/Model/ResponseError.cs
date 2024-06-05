namespace EduAdmin.Common.Model;

public record ResponseError(
    string Title,
    int StatusCode,
    List<string> Errors
) {}