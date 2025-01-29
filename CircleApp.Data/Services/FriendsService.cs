using CircleApp.Data.Helpers.Constants;
using CircleApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly AppDbContext _context;
        public FriendsService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<bool> SendRequest(int senderId, int receiverId)
        {
            var request = new FriendRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Status = FriendshipStatus.Pending,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };
            _context.FriendRequests.Add(request);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
