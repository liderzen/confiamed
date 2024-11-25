using UserService.DAL.Entities;

namespace UserService.DAL.Repositories
{
    public interface IUserRepository
    {
        // Obtener todos los usuarios
        List<User> GetAllUsers();

        // Obtener un usuario por Id
        User GetUserById(int id);

        // Agregar un nuevo usuario
        void AddUser(User user);

        // Actualizar un usuario existente
        void UpdateUser(User user);

        // Eliminar un usuario por Id
        void DeleteUser(int id);
    }
}
