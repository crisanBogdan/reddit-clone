using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class CommentVote
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public bool Vote { get; set; }
    }
}
