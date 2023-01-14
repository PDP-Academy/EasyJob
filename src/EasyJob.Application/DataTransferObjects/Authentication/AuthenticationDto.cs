namespace EasyJob.Application.DataTransferObjects;

public record AuthenticationDto(
    string userName, string password);