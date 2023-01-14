using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbNetworkRequests
    {
        public int RequestId { get; set; }
        public int? MemberId { get; set; }
        public int? NetworkId { get; set; }
        public int? NetworkType { get; set; }
        public string Purpose { get; set; }
        public DateTime? RequestedDate { get; set; }
        public int? Status { get; set; }

        public virtual TbMembers Member { get; set; }
        public virtual TbNetworks Network { get; set; }
    }
}
