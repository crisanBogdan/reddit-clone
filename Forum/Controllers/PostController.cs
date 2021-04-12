using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;
using Core.Handler;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postHandler;
        private readonly IComment _commentHandler;
        private readonly ITopic _topicHandler;

        public PostController(IPost postHandler, IComment commentHandler, ITopic topicHandler)
        {
            _postHandler = postHandler;
            _commentHandler = commentHandler;
            _topicHandler = topicHandler;
        }
        public async Task<IActionResult> Index(string topic, int id, string title)
        {
            var userId = int.Parse(HttpContext.User.Claims.First().Value);
            var post = _postHandler.GetPost(id, userId);
            post.Detailed = true;

            var allComments = await _commentHandler.GetCommentsForPost(id, userId);
            var commentsWithReplies = allComments.Select(c => {
                c.Replies = allComments.FindAll(_c => _c.CommentId == c.Id);
                return c;
            });
            // exclude if already included as a reply
            var comments = commentsWithReplies.Where(c => !commentsWithReplies.Any(cwr => cwr.Replies.Contains(c)));
            return View(new PostDetailsModel
            {
                Post = post,
                Comments = comments.ToList()
            });
        }
        public async Task<IActionResult> New(NewPostModel model)
        {
            model.TopicNames = (await _topicHandler.GetTopics(int.MaxValue)).Select(t => t.Title).ToList();
            return View(model);
        }

        public async Task<IActionResult> OnNewPostSubmit(NewPostModel model)
        {

            if (String.IsNullOrEmpty(model.SelectedTopicName))
            {
                model.ErrorMessage = "You must select a topic.";
                return RedirectToAction("New", model);
            }
            if (String.IsNullOrEmpty(model.Post.Url) && String.IsNullOrEmpty(model.Post.Content))
            {
                model.ErrorMessage = "You must provide a link or content.";
                return RedirectToAction("New", model);
            }
            if (model.Post.Content.Length > 4000)
            {
                model.ErrorMessage = "The content cannot be longer than 4000 characters.";
                return RedirectToAction("New", model);
            }
            await _postHandler.CreatePost(new Post
            {
                Title = model.Post.Title,
                Url = model.Post.Url,
                Content = model.Post.Content,
                TopicId = await _topicHandler.GetTopicId(model.SelectedTopicName),
                UserId = int.Parse(HttpContext.User.Claims.First().Value)
            });
            return Redirect($"/r/{model.SelectedTopicName}");
        }
    }
}