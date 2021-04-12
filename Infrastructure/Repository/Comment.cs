using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Core.Entity;
using Core.Handler;
using Infrastructure.Models;
using System;

namespace Infrastructure.Repository
{
    public class Comment : IComment
    {
        private readonly reddit_cloneContext _context;
        public Comment(reddit_cloneContext context)
        {
            _context = context;
        }

        public Task Create(Core.Entity.Comment c)
        {
            var createdAt = DateTime.Now;
            _context.Comment.Add(new Models.Comment
            {
                UserId = c.UserId,
                PostId = c.PostId,
                Content = c.Content,
                CommentId = c.CommentId,
                CreatedAt = createdAt,
            });
            _context.SaveChanges();

            if (c.VoteByUser == null)
            {
                return Task.CompletedTask;
            }

            var q = from comment in _context.Comment
                    where (comment.CreatedAt == createdAt)
                    select comment.Id;
            _context.CommentVote.Add(new CommentVote
            {
                UserId = c.VoteByUser.UserId,
                CommentId = q.First(),
                Vote = c.VoteByUser.VoteType == VoteType.Up,
            });
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task<int> GetCommentCountForPost(int id)
        {
            return _context.Comment.CountAsync(c => c.PostId == id);
        }

        public async Task<List<Core.Entity.Comment>> GetCommentsForPost(int postId, int userId)
        {
            var q = from comment in _context.Comment
                    where (comment.PostId == postId)
                    join user in _context.User
                    on comment.UserId equals user.Id
                    select new Core.Entity.Comment
                    {
                        Id = comment.Id,
                        Content = comment.Content,
                        CreatedAt = comment.CreatedAt,
                        UserId = comment.UserId,
                        Username = user.Name,
                        PostId = comment.PostId,
                        CommentId = comment.CommentId,
                    };
            var withRating = (await q.ToListAsync()).Select(c =>
            {
                c.Rating = Rating(c.Id);
                c.VoteByUser = UserVote(c.Id, userId);
                return c;
            });
            return withRating.ToList();
        }

        public int Rating(int id)
        {
            var q = from upvote in _context.CommentVote
                    where (upvote.CommentId == id)
                    select upvote.Vote;
            var votes = q.ToList().Select(v => v ? 1 : -1).Sum();
            return votes;
        }

        private CommentVote? FindVote(int commentId, int userId)
        {
            var q = from vote in _context.CommentVote
                    where (vote.CommentId == commentId && vote.UserId == userId)
                    select vote;
            return q.FirstOrDefault();
        }
        public Vote? UserVote(int commentId, int userId)
        {
            var found = FindVote(commentId, userId);

            if (found == null)
            {
                return null;
            }
            return new Vote
            {
                UserId = found.UserId,
                VoteType = found.Vote ? VoteType.Up : VoteType.Down
            };
        }

        private async Task AddVote(int commentId, int userId, bool vote)
        {
            _context.CommentVote.Add(new CommentVote
            {
                UserId = userId,
                CommentId = commentId,
                Vote = vote
            });
            await _context.SaveChangesAsync();
            return;
        }
        public async Task Vote(int commentId, int userId, VoteType voteType)
        {
            var vote = FindVote(commentId, userId);
            if (vote != null)
            {
                vote.Vote = voteType == VoteType.Up;
                _context.SaveChanges();
                return;
            }
            await AddVote(commentId, userId, voteType == VoteType.Up);
            return;
        }
    }
}
