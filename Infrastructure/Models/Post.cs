using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
