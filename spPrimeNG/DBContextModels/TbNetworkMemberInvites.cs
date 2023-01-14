using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbNetworkMemberInvites
    {
        public int InviteId { get; set; }
        public int? NetworkId { get; set; }
        public int? MemberId { get; set; }
        public string Email { get; set; }
        public int? Status { get; set; }

        public virtual TbMembers Member { get; set; }
        public virtual TbNetworks Network { get; set; }
    }
}
