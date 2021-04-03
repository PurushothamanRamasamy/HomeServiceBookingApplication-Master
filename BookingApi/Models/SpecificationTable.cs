using System;
using System.Collections.Generic;

#nullable disable

namespace BookingApi.Models
{
    public partial class SpecificationTable
    {
        public SpecificationTable()
        {
            UserServiceInfos = new HashSet<UserServiceInfo>();
        }

        public string Name { get; set; }
        public string SpecializationName { get; set; }

        public virtual SpecializationTable SpecializationNameNavigation { get; set; }
        public virtual ICollection<UserServiceInfo> UserServiceInfos { get; set; }
    }
}
