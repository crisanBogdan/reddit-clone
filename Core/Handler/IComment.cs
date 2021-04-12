using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Handler
{
    public interface IComment
    {
        Task<int> GetCommentCountForPost(int id);
        Task<List<Comment>> GetCommentsForPost(int postId, int userId);
        Task Vote(int commentId, int userId, VoteType voteType);
        int Rating(int commentId);
        Task Create(Comment c);
    }
}
