using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            Post = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
