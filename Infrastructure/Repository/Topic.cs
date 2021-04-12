using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Core.Handler;
using Core.Entity;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class Topic : ITopic
    {
        private readonly reddit_cloneContext _context;
        public Topic(reddit_cloneContext context)
        {
            _context = context;
        }
        public Task<List<Core.Entity.Topic>> GetTopics(int count)
        {
            var q = from topic in _context.Topic
                    select new Core.Entity.Topic
                    {
                        Id = topic.Id,
                        Title = topic.Title,
                        CreatedAt = topic.CreatedAt,
                    };
            return q.OrderBy(t => t.CreatedAt).Take(count).ToListAsync();
        }

        public Task<Core.Entity.Topic> GetTopic(string title)
        {
            var q = from topic in _context.Topic
                    where (topic.Title == title)
                    join post in _context.Post
                    on topic.Id equals post.TopicId
                    select new Core.Entity.Topic
                    {
                        Id = topic.Id,
                        Title = topic.Title,
                    };

            return q.FirstAsync();
        }

        public Task<int> GetTopicId(string title)
        {
            var q = from topic in _context.Topic
                    where (topic.Title == title)
                    select topic.Id;

            return q.FirstAsync();
        }
    }
}
