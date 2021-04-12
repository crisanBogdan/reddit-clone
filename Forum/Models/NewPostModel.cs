using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;

namespace Web.Models
{
    public class NewPostModel
    {
        public Post Post { get; set; } = new Post { Title = "An interesting title" };
        public string ErrorMessage { get; set; }
        public string SelectedTopicName { get; set; }
        public List<string> TopicNames { get; set; }
    }
}
