using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Server.Data
{
    [Table("ba_messages")]
    public class Message
    {
        [Key]
        [Column("msg_id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("msg_text")]
        public string Text { get; set; }

        [Column("msg_date")]
        public DateTime Date { get; set; }
    }
}