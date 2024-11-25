using System.Data;
using System.Data.SqlClient;
using WorkItemService.DAL.Entities;

namespace WorkItemService.DAL.Repositories
{
    public class WorkItemRepository: IWorkItemRepository
    {
        private readonly string _connectionString;

        public WorkItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<WorkItem> GetAllWorkItems()
        {
            var workItems = new List<WorkItem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM WorkItems", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workItems.Add(MapToWorkItem(reader));
                    }
                }
            }

            return workItems;
        }

        public WorkItem GetWorkItemById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM WorkItems WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapToWorkItem(reader);
                    }
                }
            }

            return null;
        }

        public void AddWorkItem(WorkItem workItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    "INSERT INTO WorkItems (Title, Description, DueDate, Relevance, AssignedUserId, IsCompleted) " +
                    "VALUES (@Title, @Description, @DueDate, @Relevance, @AssignedUserId, @IsCompleted)", connection);

                command.Parameters.AddWithValue("@Title", workItem.Title);
                command.Parameters.AddWithValue("@Description", workItem.Description);
                command.Parameters.AddWithValue("@DueDate", workItem.DueDate);
                command.Parameters.AddWithValue("@Relevance", workItem.Relevance);
                command.Parameters.AddWithValue("@AssignedUserId", workItem.AssignedUserId);
                command.Parameters.AddWithValue("@IsCompleted", workItem.IsCompleted);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateWorkItem(WorkItem workItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    "UPDATE WorkItems SET Title = @Title, Description = @Description, DueDate = @DueDate, " +
                    "Relevance = @Relevance, AssignedUserId = @AssignedUserId, IsCompleted = @IsCompleted " +
                    "WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", workItem.Id);
                command.Parameters.AddWithValue("@Title", workItem.Title);
                command.Parameters.AddWithValue("@Description", workItem.Description);
                command.Parameters.AddWithValue("@DueDate", workItem.DueDate);
                command.Parameters.AddWithValue("@Relevance", workItem.Relevance);
                command.Parameters.AddWithValue("@AssignedUserId", workItem.AssignedUserId);
                command.Parameters.AddWithValue("@IsCompleted", workItem.IsCompleted);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteWorkItem(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("DELETE FROM WorkItems WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }

        public List<WorkItem> GetPendingWorkItemsByUserId(int userId)
        {
            var workItems = new List<WorkItem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM WorkItems WHERE AssignedUserId = @UserId AND IsCompleted = 0", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workItems.Add(MapToWorkItem(reader));
                    }
                }
            }

            return workItems;
        }

        private WorkItem MapToWorkItem(IDataReader reader)
        {
            return new WorkItem
            {
                Id = Convert.ToInt32(reader["Id"]),
                Title = reader["Title"].ToString(),
                Description = reader["Description"].ToString(),
                DueDate = Convert.ToDateTime(reader["DueDate"]),
                Relevance = reader["Relevance"].ToString(),
                AssignedUserId = Convert.ToInt32(reader["AssignedUserId"]),
                IsCompleted = Convert.ToBoolean(reader["IsCompleted"])
            };
        }

    }
}
