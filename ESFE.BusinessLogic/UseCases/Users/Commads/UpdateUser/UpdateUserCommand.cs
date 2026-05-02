using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Users.Commads.UpdateUser;

public record UpdateUserCommand(UpdateUserRequest Request) : IRequest<int>;
