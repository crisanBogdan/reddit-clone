using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Core.Handler;
using Infrastructure.Models;

namespace Infrastructure.Repository
{
    class CommentCount
    {
        public int Count { get; set; }
        public int PostId { get; set; }
    }
    public class Post : IPost
    {
        private readonly reddit_cloneContext _context;
        public Post(reddit_cloneContext context)
        {
            _context = context;
        }

        public async Task<List<Core.Entity.Post>> GetPosts(int size, int userId)
        {
            var q = from post in _context.Post
                    join user in _context.User
                    on post.User.Id equals user.Id
                    join topic in _context.Topic
                    on post.Topic.Id equals topic.Id
                    join comments in (
                        from comment in _context.Comment            
                        group comment by comment.PostId into comments
                        select new CommentCount { Count = comments.Count(), PostId = comments.Key }
                    )
                    on post.Id equals comments.PostId into c
                    from comments_ in c.DefaultIfEmpty()
                    select new Core.Entity.Post
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Content = post.Content,
                        Url = post.Url,
                        UserId = post.UserId,
                        Username = user.Name,
                        TopicId = post.TopicId,
                        TopicName = topic.Title,
                        CreatedAt = post.CreatedAt,
                        CommentCount = comments_.Count,
                    };
            var list = await q.Take(size).ToListAsync();
            foreach (var p in list)
            {
                p.Rating = Rating(p.Id);
                p.VoteByUser = UserVotes(p.Id).Find(v => v.UserId == userId);
            }
            list.Sort((p1, p2) => p1.CreatedAt > p2.CreatedAt ? 1 : -1);

            return list;
        }

        public async Task<List<Core.Entity.Post>> GetPostsForTopic(int topicId, int userId, int take, int skip)
        {
            var q = from post in _context.Post
                    where (post.TopicId == topicId)
                    join topic in _context.Topic
                    on topicId equals topic.Id
                    join user in _context.User
                    on post.UserId equals user.Id
                    join comments in (
                        from comment in _context.Comment
                        group comment by comment.PostId into comments
                        select new CommentCount { Count = comments.Count(), PostId = comments.Key }
                    )
                    on post.Id equals comments.PostId into c
                    from comments_ in c.DefaultIfEmpty()
                    select new Core.Entity.Post
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Content = post.Content,
                        Url = post.Url,
                        UserId = user.Id,
                        Username = user.Name,
                        TopicId = topic.Id,
                        TopicName = topic.Title,
                        CreatedAt = post.CreatedAt,
                        CommentCount = comments_.Count,
                    };
            var list = await q.Skip(skip).Take(take).ToListAsync();
            foreach (var p in list)
            {
                p.Rating = Rating(p.Id);
                p.VoteByUser = UserVotes(p.Id).Find(v => v.UserId == userId);
            }
            list.Sort((p1, p2) => p1.CreatedAt > p2.CreatedAt ? 1 : -1);

            return list.ToList();
        }

        public Core.Entity.Post GetPost(int postId, int userId)
        {
            var q = from post in _context.Post
                    where (post.Id == postId)
                    join user in _context.User
                    on post.UserId equals user.Id
                    join topic in _context.Topic
                    on post.TopicId equals topic.Id
                    select new Core.Entity.Post
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Content = post.Content,
                        UserId = user.Id,
                        Username = user.Name,
                        CreatedAt = post.CreatedAt,
                        Url = post.Url,
                        TopicName = topic.Title
                    };
            var p = q.First();
            p.Rating = Rating(p.Id);
            p.VoteByUser = UserVotes(p.Id).Find(v => v.UserId == userId);

            return p;
        }

        public async Task CreatePost(Core.Entity.Post post)
        {
            var createdAt = DateTime.Now;
            _context.Post.Add(new Models.Post
            {
                Title = post.Title,
                Url = post.Url,
                Content = post.Content,
                UserId = post.UserId,
                TopicId = post.TopicId,
                CreatedAt = createdAt
            });
            _context.SaveChanges();

            var q = from _post in _context.Post
                    where (_post.CreatedAt == createdAt)
                    select _post.Id;
            _context.PostVote.Add(new PostVote
            {
                PostId = q.First(),
                UserId = post.UserId,
                Vote = true
            });
            _context.SaveChanges();
            return;
        }

        public int Rating(int id)
        {
            return UserVotes(id).Select(v => v.VoteType == VoteType.Up ? 1 : -1).Sum();
        }

        public List<Vote> UserVotes(int postId)
        {
            var q = from postUpvote in _context.PostVote
                    where (postUpvote.PostId == postId)
                    select new Vote
                    {
                        UserId = postUpvote.UserId,
                        VoteType = postUpvote.Vote ? VoteType.Up : VoteType.Down
                    };
            return q.ToList();
        }

        public async Task Vote(int postId, int userId, VoteType voteType)
        {
            var q = from postVote in _context.PostVote
                       where (postVote.PostId == postId && postVote.UserId == userId)
                       select postVote;
            var vote = await q.FirstOrDefaultAsync();
            if (vote != null)
            {
                vote.Vote = voteType == VoteType.Up;
                _context.SaveChanges();
                return;
            }
            _context.PostVote.Add(new PostVote
            {
                UserId = userId,
                PostId = postId,
                Vote = voteType == VoteType.Up
            });
            _context.SaveChanges();

            return;
        }
    }
}
