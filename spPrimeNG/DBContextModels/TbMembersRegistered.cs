using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbMembersRegistered
    {
        public int MemberCodeId { get; set; }
        public int MemberId { get; set; }
        public DateTime? RegisteredDate { get; set; }

        public virtual TbMembers Member { get; set; }
    }
}
