using Azure.Core;
using CircleApp.Controllers.Base;
using CircleApp.Data;
using CircleApp.Data.Helpers.Constants;
using CircleApp.Data.Models;
using CircleApp.Data.Services;
using CircleApp.ViewModels.Friends;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CircleApp.Controllers
{
    public class FriendsController : BaseController
    {
        public readonly IFriendsService _friendsService;

        public FriendsController(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            if (!userId.HasValue) RedirectToLogin();


            var friends = await _friendsService.GetFriendsAsync(userId.Value);
            var friendRequestsSent = await _friendsService.GetSentFriendRequestsAsync(userId.Value);
            var friendRequestsReceived = await _friendsService.GetReceivedFriendRequestsAsync(userId.Value);

            var friendshipSumary = new FriendshipVM
            {
                Friends = friends,
                FriendRequestsSent = friendRequestsSent,
                FriendRequestsReceived = friendRequestsReceived,
            };

            return View(friendshipSumary);
        }



        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(int receiverId)
        {
            var userId = GetUserId();
            if(!userId.HasValue) RedirectToLogin();

            await _friendsService.SendRequestAsync(userId.Value, receiverId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AcceptRequest(int requestId)
        {
            await _friendsService.UpdateRequestAsync(requestId, FriendshipStatus.Accepted);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RejectRequest(int requestId)
        {
            await _friendsService.UpdateRequestAsync(requestId, FriendshipStatus.Rejected);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> CancelRequest(int requestId)
        {
            await _friendsService.UpdateRequestAsync(requestId, FriendshipStatus.Canceled);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetNotificationsCount()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            //var count = await _friendsService.GetUnreadNotificationsCountAsync(userId.Value);
            return Ok(10);
        }



    }
}
