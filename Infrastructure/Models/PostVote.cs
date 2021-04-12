using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class PostVote
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public bool Vote { get; set; }
    }
}
