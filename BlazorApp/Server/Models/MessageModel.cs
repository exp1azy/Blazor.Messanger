namespace BlazorApp.Server.Models
{
    public class MessageModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
