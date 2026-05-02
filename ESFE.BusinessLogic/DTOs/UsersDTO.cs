namespace ESFE.BusinessLogic.DTOs
{
    public class UserDTO
    {
        // El ID es vital para poder editar o eliminar al usuario después
        public int Id { get; set; }

        // El Nombre es lo que mostraremos en el saludo "Bienvenido, Juan"
        public string Nombre { get; set; }

        // El Email es el identificador principal en la Cookie
        public string Email { get; set; }

        // Opcional: El Rol nos servirá si después quieres bloquear 
        // secciones solo para administradores
        public string Rol { get; set; }
    }
}