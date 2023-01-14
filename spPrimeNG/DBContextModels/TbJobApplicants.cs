using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbJobApplicants
    {
        public int AppJobId { get; set; }
        public int MemberId { get; set; }
        public int PostId { get; set; }
        public DateTime? AppliedDate { get; set; }
    }
}
