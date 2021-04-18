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

        IEnumerable<UserServiceInfo> GetAllProviders();
        IEnumerable<UserServiceInfo> GetUsers();
        Task<UserServiceInfo> PostUser(UserServiceInfo item);
        UserServiceInfo GetUserById(string id);
        UserServiceInfo GetUserServiceInfoByUserName(string Username);
        UserServiceInfo GetUserServiceInfoByMobile(string Mobile);
        UserServiceInfo GetUserServiceInfoByAadhaar(string Aadhaar);
        Task<UserServiceInfo> RemoveUser(string id);
        Task<UserServiceInfo> EditUser(string id, UserServiceInfo item);

    }
}
