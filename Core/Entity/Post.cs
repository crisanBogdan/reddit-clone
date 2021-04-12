using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Rating { get; set; }
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CommentCount { get; set; }
        public Vote VoteByUser { get; set; }
        public bool Detailed { get; set; }
    }
}
