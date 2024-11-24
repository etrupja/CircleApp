using CircleApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Services
{
    public interface IUsersService
    {
        Task<User> GetUser(int loggedInUserId);
        Task UpdateUserProfilePicture(int loggedInUserId, string profilePictureUrl);
    }
}
