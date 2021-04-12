using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Handler
{
    public interface ITopic
    {
        Task<List<Topic>> GetTopics(int count);
        Task<Topic> GetTopic(string title);
        Task<int> GetTopicId(string title);
    }
}
