using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PivotIt.Infrastructure.Persistence;
using PivotIt.Web.Models;

namespace PivotIt.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SiteContext _siteContext;

        public HomeController(ILogger<HomeController> logger, SiteContext siteContext)
        {
            _logger = logger;
            _siteContext = siteContext;
        }

        public async Task<IActionResult> Index()
        {
            if (!await _siteContext.UserMessage.AnyAsync().ConfigureAwait(false))
            {
                _logger.LogInformation("Starting user messages table init...");
                for (int i = 0; i < 10; i++)
                {
                    await _siteContext.UserMessage.AddAsync(new Core.Entities.UserMessage
                    {
                        UserID = $"User{i}",
                        CCUsersID = $"4f28f403-6aed-4d10-8ee6-bd9e7919ec92-{i}",
                        Subject = $"Message {i} of 9",
                        MessageBody = $"This is message body of message with count index {i}. Thanks for reaading"
                    }).ConfigureAwait(false);
                }

                await _siteContext.SaveChangesAsync().ConfigureAwait(false);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
