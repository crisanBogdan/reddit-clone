using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;

namespace Web.Models
{
    public class PostDetailsModel
    {
        public Post Post { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
