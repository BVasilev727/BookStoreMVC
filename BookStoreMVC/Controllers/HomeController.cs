using BookStoreMVC.Areas.Identity.Data;
using BookStoreMVC.Models;
using BookStoreMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<BookStoreMVCUser> _userManager;
        private readonly IVideoServices _videoServices;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, UserManager<BookStoreMVCUser> userManager, IVideoServices videoServices, IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _videoServices = videoServices;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var randomVideo = await _videoServices.GetRandomVideo();
            var videoPostedByUser = await _userService.GetUserByIdAsync(randomVideo.PostedByUser);

            VideoViewModel recommendedVideo = new VideoViewModel
            {
                Id = randomVideo.Id,
                Title = randomVideo.Title,
                Description = randomVideo.Description,
                VideoLink = randomVideo.VideoLink,
                ThumbnailURL = randomVideo.ThumbnailURL,
                PostedByUser = videoPostedByUser.Id,
                PostedByUserWithName = videoPostedByUser.UserName,
                Likes = randomVideo.Likes,
            };
            return View(recommendedVideo);
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
