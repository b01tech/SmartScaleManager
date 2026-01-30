using Core.Enums;

namespace Application.Dtos.Requests;

public record RegisterUserRequest(string Name, string Password, Role Role);
