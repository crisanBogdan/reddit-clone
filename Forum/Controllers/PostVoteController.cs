using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;
using Core.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostVoteController : ControllerBase
    {
        private readonly IPost _postHandler;
        public PostVoteController(IPost postHandler)
        {
            _postHandler = postHandler;
        }
        [HttpGet("Upvote/{id}")]
        public async Task<IActionResult> Upvote(int id)
        {
            var userId = int.Parse(HttpContext.User.Claims.First().Value);
            await _postHandler.Vote(id, userId, VoteType.Up);
            return Ok();
        }
        [HttpGet("Downvote/{id}")]
        public async Task<IActionResult> Downvote(int id)
        {
            var userId = int.Parse(HttpContext.User.Claims.First().Value);
            await _postHandler.Vote(id, userId, VoteType.Down);
            return Ok();
        }
        [HttpGet("Votes/{id}")]
        public ActionResult<int> Votes(int id)
        {
            return _postHandler.Rating(id);
        }
    }
}
