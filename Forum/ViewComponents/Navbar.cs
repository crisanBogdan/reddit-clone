using Core.Handler;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewComponents
{
    public class Navbar : ViewComponent
    {
        private readonly ITopic _topicHandler;

        public Navbar(ITopic topicHandler)
        {
            _topicHandler = topicHandler;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var topics = await _topicHandler.GetTopics(5);
            return View(topics);
        }
    }
}
