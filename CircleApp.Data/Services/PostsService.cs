using CircleApp.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Services
{
    public class PostsService:IPostsService
    {
        private readonly AppDbContext _context;
        public PostsService(AppDbContext context) 
        {
            _context = context;
        }

        public Task AddPostCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<Post> CreatePostAsync(Post post, IFormFile Image)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetAllPostsAsync(int loggedInUserId)
        {
            throw new NotImplementedException();
        }

        public Task RemovePostAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task RemovePostCommentAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        public Task ReportPostAsync(int postId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task TogglePostFavoriteAsync(int postId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task TogglePostLikeAsync(int postId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
