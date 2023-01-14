using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbMembersBlocked
    {
        public int? MemberId { get; set; }
        public int? BlockMemberId { get; set; }
        public string BlockMemberName { get; set; }

        public virtual TbMembers BlockMember { get; set; }
        public virtual TbMembers Member { get; set; }
    }
}
