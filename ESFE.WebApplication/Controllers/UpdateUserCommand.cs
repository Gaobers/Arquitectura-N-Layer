using ESFE.Entities;
using MediatR;

namespace ProyectoWeb.Controllers
{
    internal class UpdateUserCommand : IRequest<object>
    {
        public UpdateUserCommand(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}