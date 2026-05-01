using MediatR;

namespace ProyectoWeb.Controllers
{
    internal class DeleteUserCommand : IRequest<object>
    {
        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}