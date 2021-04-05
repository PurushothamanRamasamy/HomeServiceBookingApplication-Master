using UsersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersAPI.Repository
{
    public class UserRepo: IUserRepo
    {
        private readonly UsersContext _context;

        public UserRepo()
        {

        }
        public UserRepo(UsersContext context)
        {
            _context = context;
        }
        public UserServiceInfo GetUserById(string id)
        {
            UserServiceInfo usr = _context.UserServiceInfos.Find(id);
            return usr;
        }
        
        public UserServiceInfo GetUserServiceInfoByUserName(string Username)
        {
            UserServiceInfo item = _context.UserServiceInfos.FirstOrDefault(usr => usr.Username == Username);

            return item;
        }

        public UserServiceInfo GetUserServiceInfoByAadhaar(string Aadhaar)
        {
            UserServiceInfo item = _context.UserServiceInfos.FirstOrDefault(usr => usr.Aadhaarno == Aadhaar && usr.Role!="admin");

            return item;
        }
        /*public async Task<Role> GetUserRole(string uName, string pass)
        {
            Role Sp = new Role();
            var role = _context.UserServiceInfos.FirstOrDefault(u => u.Username == uName && u.Password == pass);
            Sp.role = role.Role;
            return Sp;
        }*/
        public IEnumerable<UserServiceInfo> GetAllUsers()
        {
            return _context.UserServiceInfos.ToList();
        }
        public IEnumerable<UserServiceInfo> GetAllProviders()
        {
            return _context.UserServiceInfos.Where(u=>u.Role== "provider" && u.IsNewProvider==false).ToList();
        }
        public IEnumerable<UserServiceInfo> GetUsers()
        {
            return _context.UserServiceInfos.Where(u => u.Role == "user").ToList();
        }
        public async Task<UserServiceInfo> PostUser(UserServiceInfo item)
        {
            UserServiceInfo Sp = null;
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                Sp = new UserServiceInfo() {
                    Usid = item.Username + "-" + item.Phoneno,
                    Username = item.Username,
                    Phoneno = item.Phoneno,
                    EmailId = item.EmailId,
                    Password = item.Password,
                    Specialization = item.Specialization,
                    Specification = item.Specification,
                    ServiceCity = item.ServiceCity,
                    Address = item.Address,
                    Aadhaarno = item.Aadhaarno,
                    Role = item.Role,
                    Experience = item.Experience,
                    Costperhour = item.Costperhour,
                    Rating = item.Rating,
                    IsNewProvider = true,
                    IsProvicedBooked=false
                };
                await _context.UserServiceInfos.AddAsync(Sp);
                await _context.SaveChangesAsync();
            }
            return Sp;
        }
        public async Task<UserServiceInfo> RemoveUser(string id)
        {
            UserServiceInfo sp = await _context.UserServiceInfos.FindAsync(id);
            if (sp == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                _context.UserServiceInfos.Remove(sp);
                await _context.SaveChangesAsync();
            }
            return sp;
        }
        public async Task<UserServiceInfo> EditUser(string id,UserServiceInfo item)
        {
            UserServiceInfo sp = await _context.UserServiceInfos.FindAsync(id);
            sp.Password = item.Password;
            sp.Rating = item.Rating;
            sp.IsNewProvider = item.IsNewProvider;
            sp.IsProvicedBooked = item.IsProvicedBooked;
            _context.SaveChanges();
            return sp;
        }
    }
}
