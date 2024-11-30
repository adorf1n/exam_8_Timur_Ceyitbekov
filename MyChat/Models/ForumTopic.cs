namespace MyChat.Models
{
    public class ForumTopic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; } 
        public User? User { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<ForumReply> Replies { get; set; }
    }


}
