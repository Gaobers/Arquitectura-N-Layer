using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Users.Commads.CreateUser;


internal sealed class CreateUserHandler(IEfRepository<User> _repository)
    : IRequestHandler<CreateUserCommand, int>
{
    public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newUser = command.Request.Adapt<User>();
            var createdUser = await _repository.AddAsync(newUser, cancellationToken);
            return createdUser.UserId;
        }
        catch (Exception)
        {
            return 0;
            throw;
        }
    }
}