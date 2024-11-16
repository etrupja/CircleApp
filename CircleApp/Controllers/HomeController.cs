using CircleApp.Data;
using CircleApp.Data.Helpers;
using CircleApp.Data.Models;
using CircleApp.Data.Services;
using CircleApp.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CircleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IPostsService _postsService;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IPostsService postsService)
        {
            _logger = logger;
            _context = context;
            _postsService = postsService;
        }

        public async Task<IActionResult> Index()
        {
            int loggedInUserId = 1;
            var allPosts = await _postsService.GetAllPostsAsync(loggedInUserId);

            return View(allPosts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostVM post)
        {
            //Get the logged in user
            int loggedInUser = 1;

            //Create a new post
            var newPost = new Post
            {
                Content = post.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                ImageUrl = "",
                NrOfReports = 0,
                UserId = loggedInUser
            };

            await _postsService.CreatePostAsync(newPost, post.Image);


            //Find and store hashtags
            var postHashtags = HashtagHelper.GetHashtags(post.Content);
            foreach (var hashTag in postHashtags) 
            {
                var hashtagDb = await _context.Hashtags.FirstOrDefaultAsync(n => n.Name == hashTag);
                if(hashtagDb != null)
                {
                    hashtagDb.Count += 1;
                    hashtagDb.DateUpdated = DateTime.UtcNow;

                    _context.Hashtags.Update(hashtagDb);
                    await _context.SaveChangesAsync();
                } else
                {
                    var newHashtag = new Hashtag()
                    {
                        Name = hashTag,
                        Count = 1,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow
                    };
                    await _context.Hashtags.AddAsync(newHashtag);
                    await _context.SaveChangesAsync();
                }
            }

            //Redirect to the index page
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> TogglePostLike(PostLikeVM postLikeVM)
        {
            int loggedInUserId = 1;
            await _postsService.TogglePostLikeAsync(postLikeVM.PostId, loggedInUserId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostFavorite(PostFavoriteVM postFavoriteVM)
        {
            int loggedInUserId = 1;
            await _postsService.TogglePostFavoriteAsync(postFavoriteVM.PostId, loggedInUserId);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> TogglePostVisibility(PostVisibilityVM postVisibilityVM)
        {
            int loggedInUserId = 1;
            await _postsService.TogglePostVisibilityAsync(postVisibilityVM.PostId, loggedInUserId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPostComment(PostCommentVM postCommentVM)
        {
            int loggedInUserId = 1;

            //Creat a post object
            var newComment = new Comment()
            {
                UserId = loggedInUserId,
                PostId = postCommentVM.PostId,
                Content = postCommentVM.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            await _postsService.AddPostCommentAsync(newComment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPostReport(PostReportVM postReportVM)
        {
            int loggedInUserId = 1;
            await _postsService.ReportPostAsync(postReportVM.PostId, loggedInUserId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemovePostComment(RemoveCommentVM removeCommentVM)
        {
            await _postsService.RemovePostCommentAsync(removeCommentVM.CommentId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> PostRemove(PostRemoveVM postRemoveVM)
        {

            await _postsService.RemovePostAsync(postRemoveVM.PostId);
           
                //Update hashtags
                //var postHashtags = HashtagHelper.GetHashtags(postDb.Content);
                //foreach (var hashtag in postHashtags)
                //{
                //    var hashtagDb = await _context.Hashtags.FirstOrDefaultAsync(n => n.Name == hashtag);
                //    if (hashtagDb != null)
                //    {
                //        hashtagDb.Count -= 1;
                //        hashtagDb.DateUpdated = DateTime.UtcNow;

                //        _context.Hashtags.Update(hashtagDb);
                //        await _context.SaveChangesAsync();
                //    }
                //}

            return RedirectToAction("Index");
        }
    }
}
