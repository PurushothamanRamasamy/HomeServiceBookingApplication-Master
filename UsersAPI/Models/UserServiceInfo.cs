using System;
using System.Collections.Generic;

#nullable disable

namespace UsersAPI.Models
{
    public partial class UserServiceInfo
    {
        public UserServiceInfo()
        {
            
        }

        public string Usid { get; set; }
        public string Username { get; set; }
        public string Phoneno { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Specialization { get; set; }
        public string Specification { get; set; }
        public string ServiceCity { get; set; }
        public string Address { get; set; }
        public string Aadhaarno { get; set; }
        public string Role { get; set; }
        public int? Experience { get; set; }
        public int? Costperhour { get; set; }
        public int? Rating { get; set; }
        public bool? IsNewProvider { get; set; }
        public bool? IsProvicedBooked { get; set; }  


    }
}
