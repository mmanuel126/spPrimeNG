using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbActivityType
    {
        public TbActivityType()
        {
            TbMembersRecentActivities = new HashSet<TbMembersRecentActivities>();
        }

        public int ActivityTypeId { get; set; }
        public string ActivityTypeDesc { get; set; }
        public string ImageFile { get; set; }

        public virtual ICollection<TbMembersRecentActivities> TbMembersRecentActivities { get; set; }
    }
}
