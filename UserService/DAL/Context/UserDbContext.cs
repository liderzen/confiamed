using System.Data.SqlClient;
using UserService.DAL.Entities;

namespace UserService.DAL.Context
{
    public class UserDbContext
    {
        private readonly string _connectionString;

        // Constructor para inicializar el string de conexión
        public UserDbContext(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // Método para obtener todos los usuarios
        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT Id, Username, Email FROM Users";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Email = reader.GetString(2)
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        // Método para agregar un nuevo usuario
        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "INSERT INTO Users (Username, Email) VALUES (@Username, @Email)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Email", user.Email);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para actualizar un usuario
        public void UpdateUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "UPDATE Users SET Username = @Username, Email = @Email WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Email", user.Email);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para eliminar un usuario
        public void DeleteUser(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "DELETE FROM Users WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
