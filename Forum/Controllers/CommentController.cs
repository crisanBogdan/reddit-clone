using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;
using Core.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly IComment _commentHandler;
        public CommentController(IComment commentHandler)
        {
            _commentHandler = commentHandler;
        }
        public IActionResult Add([FromQuery]int postId, [FromQuery]string topicName, [FromQuery]string postTitle, [FromQuery]int? commentId)
        {
            return View(new AddCommentModel
            {
                PostId = postId,
                CommentId = commentId,
                TopicName = topicName,
                PostTitle = postTitle
            });
        }

        public async Task<IActionResult> SubmitComment(AddCommentModel model)
        {
            var userId = int.Parse(HttpContext.User.Claims.First().Value);
            await _commentHandler.Create(new Comment
            {
                Content = model.Content,
                PostId = model.PostId,
                UserId = userId,
                CommentId = model.CommentId,
                VoteByUser = new Vote { UserId = userId, VoteType = VoteType.Up },
            });
            return RedirectToAction("Index", "Post", new
            {
                topic = model.TopicName,
                id = model.PostId,
                title = model.PostTitle,
            });
        }
    }
}