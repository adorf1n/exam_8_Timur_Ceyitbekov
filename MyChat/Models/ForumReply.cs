namespace MyChat.Models
{
    public class ForumReply
    {
        public int Id { get; set; }
        public string Content { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public int ForumTopicId { get; set; }
        public ForumTopic ForumTopic { get; set; } 
        public int UserId { get; set; } 
        public User User { get; set; } 
    }

}
