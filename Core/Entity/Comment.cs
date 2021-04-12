using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int PostId { get; set; }
        public int? CommentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Rating { get; set; }
        public Vote VoteByUser { get; set; }
        public IEnumerable<Comment> Replies { get; set; }
    }
}
