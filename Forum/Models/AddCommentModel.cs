using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class AddCommentModel
    {
        public int PostId { get; set; }
        public int? CommentId { get; set; }
        public string Content { get; set; }
        public string TopicName { get; set; }
        public string PostTitle { get; set; }
    }
}
