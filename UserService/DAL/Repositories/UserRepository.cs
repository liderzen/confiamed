using System.Data.SqlClient;
using UserService.DAL.Entities;

namespace UserService.DAL.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

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
                                Id = reader.GetInt32(0),         // Lee la columna "Id"
                                Username = reader.GetString(1), // Lee la columna "Username"
                                Email = reader.GetString(2)     // Lee la columna "Email"
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

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

        public User GetUserById(int id)
        {
            User user = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT Id, Username, Email FROM Users WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Email = reader.GetString(2)
                            };
                        }
                    }
                }
            }

            return user;
        }

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

        public void DeleteUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Users WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
