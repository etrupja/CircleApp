﻿using CircleApp.Data.Dtos;
using CircleApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Services
{
    public interface IFriendsService
    {
        Task SendRequestAsync(int senderId, int receiverId);
        Task UpdateRequestAsync(int requestId, string status);
        Task RemoveFriendAsync(int frienshipId);

        Task<List<UserWithFriendsCountDto>> GetSuggestedFriendsAsync(int userId);

        Task<List<Friendship>> GetFriendsAsync(int userId);
        Task<List<FriendRequest>> GetSentFriendRequestsAsync(int userId);
        Task<List<FriendRequest>> GetReceivedFriendRequestsAsync(int userId);
    }
}
