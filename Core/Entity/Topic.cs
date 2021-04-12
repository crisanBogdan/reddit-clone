using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Post> Post { get; set; }

    }
}
