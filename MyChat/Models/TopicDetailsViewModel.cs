namespace MyChat.Models
{
    public class TopicDetailsViewModel
    {
        public ForumTopic Topic { get; set; }
        public List<ForumReply> Replies { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

}
