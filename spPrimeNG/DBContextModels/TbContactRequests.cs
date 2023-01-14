using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbContactRequests
    {
        public int RequestId { get; set; }
        public int? FromMemberId { get; set; }
        public int? ToMemberId { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? Status { get; set; }

        public virtual TbMembers FromMember { get; set; }
        public virtual TbMembers ToMember { get; set; }
    }
}
