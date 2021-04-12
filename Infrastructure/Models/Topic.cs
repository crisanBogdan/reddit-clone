using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class Topic
    {
        public Topic()
        {
            Post = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
