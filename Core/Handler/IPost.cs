using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Handler
{
    public interface IPost
    {
        Task<List<Post>> GetPosts(int size, int userId);
        Task<List<Post>> GetPostsForTopic(int topicId, int userId, int take, int skip);
        Post GetPost(int id, int userId);
        Task CreatePost(Post post);
        List<Vote> UserVotes(int postId);
        Task Vote(int postId, int userId, VoteType voteType);
        int Rating(int id);
    }
}
