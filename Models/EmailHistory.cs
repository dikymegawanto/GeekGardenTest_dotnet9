using System;

namespace GeekGarden.API.Models
{
    public class EmailHistory
    {
        public int Id { get; set; }
        public string? RecipientEmail { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public DateTime SentAt { get; set; }
    }
}
