using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Users.Commads.UpdateUser;

internal sealed class UpdateUserHandler(IEfRepository<User> _repository)
    : IRequestHandler<UpdateUserCommand, int>
{
    public async Task<int> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingUser = await _repository.GetByIdAsync(command.Request.UserId, cancellationToken);
            if (existingUser is null) return 0;

            // Guardar valores que NO deben perderse
            var currentRegistrationDate = existingUser.RegistrationDate;
            var currentPassword = existingUser.UserPassword;

            // Mapear
            command.Request.Adapt(existingUser);

            // Restaurar
            existingUser.RegistrationDate = currentRegistrationDate;
            existingUser.UserPassword = currentPassword;

            await _repository.UpdateAsync(existingUser, cancellationToken);

            return existingUser.UserId;
        }
        catch (Exception ex)
        {
            throw new Exception(
                ex.InnerException?.InnerException?.Message
                ?? ex.InnerException?.Message
                ?? ex.Message,
                ex
            );
        }
    }
}
