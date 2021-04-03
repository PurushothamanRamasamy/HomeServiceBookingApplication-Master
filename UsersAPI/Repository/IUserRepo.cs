using UsersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersAPI.Repository
{
    public interface IUserRepo
    {
        IEnumerable<UserServiceInfo> GetAllUsers();
        Task<UserServiceInfo> PostUser(UserServiceInfo item);
        UserServiceInfo GetUserById(string id);
        Role GetUserRole(string username);
        Task<UserServiceInfo> RemoveUser(string id);
        Task<UserServiceInfo> EditUser(string id, UserServiceInfo item);

    }
}
