namespace EasyJob.Application.DataTransferObjects;

public record AuthenticationDto(
    string email, string password);