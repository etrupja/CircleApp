using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Services
{
    public interface IFriendsService
    {
        Task<bool> SendRequest(int senderId, int receiverId);
    }
}
