using System.ComponentModel.DataAnnotations;
using WorkItemService.DAL.Entities;

namespace UserService.DAL.Entities
{
    public class User
    {
        // Representa el identificador único del usuario (Primary Key)
        public int Id { get; set; }

        // Representa el nombre de usuario
        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        // Representa el correo electrónico del usuario
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
