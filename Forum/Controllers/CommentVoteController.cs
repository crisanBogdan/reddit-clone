using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Handler;
using Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentVoteController : ControllerBase
    {
        private readonly IComment _commentHandler;
        public CommentVoteController(IComment commentHandler)
        {
            _commentHandler = commentHandler;
        }
        [HttpGet("Upvote/{id}")]
        public async Task<IActionResult> Upvote(int id)
        {
            var userId = int.Parse(HttpContext.User.Claims.First().Value);
            await _commentHandler.Vote(id, userId, VoteType.Up);
            return Ok();
        }
        [HttpGet("Downvote/{id}")]
        public async Task<IActionResult> Downvote(int id)
        {
            var userId = int.Parse(HttpContext.User.Claims.First().Value);
            await _commentHandler.Vote(id, userId, VoteType.Down);
            return Ok();
        }
        [HttpGet("Votes/{id}")]
        public async Task<ActionResult<int>> Votes(int id)
        {
            return _commentHandler.Rating(id);
        }
    }
}
