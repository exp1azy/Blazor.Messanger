using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Server.Data
{
    [Table("ba_users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int Id { get; set; }

        [Column("user_name")]
        public string Name { get; set; }

        [Column("user_password")]
        public string Password { get; set; }

        [Column("user_nick")]
        public string Username { get; set; }

        [Column("user_email")]
        public string Email { get; set; }
    }
}