using MediatR;

namespace ProyectoWeb.Controllers
{
    internal class CreateUserCommand : IRequest<object>
    {
        private CreateUserRequest request;

        public CreateUserCommand(CreateUserRequest request)
        {
            this.request = request;
        }
    }
}