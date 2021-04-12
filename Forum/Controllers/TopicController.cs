using Core.Handler;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class TopicController : Controller
    {
        private readonly IPost _postHandler;
        private readonly ITopic _topicHandler;

        public TopicController(IPost postHandler, ITopic topicHandler)
        {
            _postHandler = postHandler;
            _topicHandler = topicHandler;
        }
        public async Task<IActionResult> IndexAsync(string topic)
        {
            var userId = int.Parse(HttpContext.User.Claims.First().Value);
            var posts = await _postHandler.GetPostsForTopic(await _topicHandler.GetTopicId(topic), userId, 10, 0);
            return View(posts);
        }
    }
}
