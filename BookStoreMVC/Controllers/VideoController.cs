using BookStoreMVC.Models;
using BookStoreMVC.Repositories.Implementation;
using BookStoreMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace BookStoreMVC.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoServices _videoServices;
        private readonly IUserService _userService;
        private readonly IFavoriteService _favoriteService;

        public VideoController(IVideoServices videoServices, IUserService userService, IFavoriteService favoriteService)
        {
            _videoServices = videoServices;
            _userService = userService;
            _favoriteService = favoriteService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var videos = await _videoServices.GetAllVideosAsync();
         
            return View(videos);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(string searchTerm)
        {
            IEnumerable<Video> videos;
            if(!string.IsNullOrEmpty(searchTerm))
            {
                videos = await _videoServices.SearchForVideos(searchTerm);
            }
            else
            {
                videos = await _videoServices.GetAllVideosAsync();
            }
            return View(videos);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(string videoId)
        {
            var video = await _videoServices.GetVideoAsync(videoId);
            var videoPostedByUser = await _userService.GetUserByIdAsync(video.PostedByUser);
            if(video == null)
            {
                return NotFound();
            }
            var videoViewModel = new VideoViewModel
            {
                Id = video.Id,
                Title = video.Title,
                ThumbnailURL = video.ThumbnailURL,
                Likes = video.Likes,
                VideoLink = video.VideoLink,
                PostedByUser = video.PostedByUser,
                PostedByUserWithName = videoPostedByUser.UserName,
                Description = video.Description
            };
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.IsFavorited = await _favoriteService.IsFavoriteAsync(userId, videoId);
            return View(videoViewModel);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Video video)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if(ModelState.IsValid)
            {
                var newVideo = new Video
                {
                    Id = video.Id,
                    Title = video.Title,
                    PostedByUser = video.PostedByUser,
                    Description = video.Description,
                    Likes = 0,
                    VideoLink = video.VideoLink,
                    ThumbnailURL = video.ThumbnailURL,
                };
                await _videoServices.AddVideoAsync(newVideo);
                return RedirectToAction("Index");
            }
            return View(video);
        }
        [Authorize]
        public async Task<IActionResult> Edit(string videoId)
        {
            var video = await _videoServices.GetVideoAsync(videoId);
            if(video == null)
            {
                return NotFound();
            }
            return View(video);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(Video video)
        {
            var user = await _userService.GetUserByIdAsync(video.PostedByUser);
            if(video.PostedByUser != user.Id)
            {
                return NotFound();
            }
            var editedVideo = new Video
            {
                Id = video.Id,
                Title = video.Title,
                Description = video.Description,
                PostedByUser = video.PostedByUser,
                Likes = video.Likes,
                VideoLink = video.VideoLink,
                ThumbnailURL = video.ThumbnailURL
            };
            if(ModelState.IsValid)
            {
                await _videoServices.UpdateVideoAsync(editedVideo);
                return RedirectToAction("Index");
            }
            return View(video);
        }
        [Authorize]
        public async Task<IActionResult> Delete(string videoId)
        {
            var video = await _videoServices.GetVideoAsync(videoId);
            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string videoId)
        {
            var video = await _videoServices.GetVideoAsync(videoId);
            if(video.Id == videoId)
            {
                if(User.IsInRole("Admin") || User.FindFirstValue(ClaimTypes.NameIdentifier) == video.PostedByUser)
                {
                    await _videoServices.DeleteVideoAsync(videoId);
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Like(string videoId)
        {
            var video = await _videoServices.GetVideoAsync(videoId);
            if(video==null)
            {
                return NotFound();
            }
            video.Likes+=1;
            await _videoServices.UpdateVideoAsync(video);
            return RedirectToAction("Details",video.Id);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Favorite(string videoId)
        {
            if (string.IsNullOrEmpty(videoId)) return BadRequest();


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                return Unauthorized();
            }
            await _favoriteService.AddToFavoriteAsync(userId, videoId);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Unfavorite(string videoId)
        {
            if (string.IsNullOrEmpty(videoId)) return BadRequest();


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }
            await _favoriteService.RemoveFromFavorites(userId, videoId);
            return Ok();
        }
        [Authorize]
        public async Task<IActionResult> MyFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var videoPostedByUser = await _userService.GetUserByIdAsync(userId);
            if (userId == null) { return BadRequest(); }

            var favorites = await _favoriteService.GetUserFavorites(userId);
            List<VideoViewModel> favoriteViews = new List<VideoViewModel>();
            foreach (var favorite in favorites)
            {
                VideoViewModel videoViewModel = new VideoViewModel
                {
                    Id = favorite.Id,
                    Title = favorite.Title,
                    ThumbnailURL = favorite.ThumbnailURL,
                    Likes = favorite.Likes,
                    VideoLink = favorite.VideoLink,
                    PostedByUser = favorite.PostedByUser,
                    PostedByUserWithName = videoPostedByUser.UserName,
                    Description = favorite.Description
                };
                favoriteViews.Add(videoViewModel);
            }

            return View(favoriteViews);
        }
        [Authorize]
        public async Task<IActionResult> MyVideos()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound();
            }

            var myVideos = await _videoServices.GetVideosByUserAsync(userId);
            return View(myVideos);
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MyVideos(string searchTerm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId==null)
            {
                return NotFound();
            }
            IEnumerable<Video> videos;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                videos = await _videoServices.SearchForVideosFromUser(searchTerm, userId);
            }

            else {
                videos = await _videoServices.GetVideosByUserAsync(userId);

            } return View(videos);

        }
    }
}
