using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbMembersRecentActivities
    {
        public int ActivityId { get; set; }
        public string Description { get; set; }
        public DateTime? ActivityDate { get; set; }
        public int? MemberId { get; set; }
        public int? ActivityTypeId { get; set; }

        public virtual TbActivityType ActivityType { get; set; }
        public virtual TbMembers Member { get; set; }
    }
}
