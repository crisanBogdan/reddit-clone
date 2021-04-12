using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity
{
    public class Vote
    {
        public int UserId { get; set; }
        public VoteType VoteType { get; set; }
    }
    public enum VoteType
    {
        None,
        Up,
        Down
    }
}
