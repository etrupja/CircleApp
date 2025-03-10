using CircleApp.Data.Dtos;
using CircleApp.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Services
{
    public interface IPostsService
    {
        Task<List<Post>> GetAllPostsAsync(int loggedInUserId);
        Task<Post> GetPostByIdAsync(int postId);
        Task<List<Post>> GetAllFavoritedPostsAsync(int loggedInUserId);
        Task<Post> CreatePostAsync(Post post);
        Task<Post> RemovePostAsync(int postId);

        Task AddPostCommentAsync(Comment comment);
        Task RemovePostCommentAsync(int commentId);

        Task<GetNotificationDto> TogglePostLikeAsync(int postId, int userId);
        Task<GetNotificationDto> TogglePostFavoriteAsync(int postId, int userId);
        Task TogglePostVisibilityAsync(int postId, int userId);
        Task ReportPostAsync(int postId, int userId);
    }
}
