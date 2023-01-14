using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbNetworkMembers
    {
        public int NetworkId { get; set; }
        public int MemberId { get; set; }
        public DateTime? JoinedDate { get; set; }
        public bool? IsOwner { get; set; }
        public bool? IsAdmin { get; set; }
        public int? Status { get; set; }

        public virtual TbMembers Member { get; set; }
        public virtual TbNetworks Network { get; set; }
    }
}
