using UserService.BLL.Interfaces;
using UserService.DAL.Entities;
using UserService.DAL.Repositories;

namespace UserService.BLL.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        // Constructor
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            // Validación adicional
            if (id <= 0)
            {
                throw new ArgumentException("El ID del usuario debe ser mayor a 0.");
            }

            return _userRepository.GetUserById(id);
        }

        public void AddUser(User user)
        {
            // Validaciones básicas
            if (string.IsNullOrEmpty(user.Username))
            {
                throw new ArgumentException("El nombre de usuario no puede estar vacío.");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentException("El correo electrónico no puede estar vacío.");
            }

            _userRepository.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            if (user.Id <= 0)
            {
                throw new ArgumentException("El ID del usuario debe ser válido.");
            }

            if (string.IsNullOrEmpty(user.Username))
            {
                throw new ArgumentException("El nombre de usuario no puede estar vacío.");
            }

            _userRepository.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID del usuario debe ser mayor a 0.");
            }

            _userRepository.DeleteUser(id);
        }


    }
}
