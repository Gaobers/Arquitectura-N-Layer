using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Users.Commads.CreateUser;

public record CreateUserCommand(CreateUserRequest Request) : IRequest<int>;
