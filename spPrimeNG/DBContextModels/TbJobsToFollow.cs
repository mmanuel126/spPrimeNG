using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbJobsToFollow
    {
        public int JobFollowId { get; set; }
        public int PostId { get; set; }
        public int MemberId { get; set; }
    }
}
