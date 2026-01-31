namespace Application.Dtos.Responses;

public record UserDetailsResponse(long Id, string Name, string Role, bool IsActive);
