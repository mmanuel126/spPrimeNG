using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbNetworkPostResponses
    {
        public int PostResponseId { get; set; }
        public int? PostId { get; set; }
        public string Description { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int? MemberId { get; set; }
        public int? NetworkId { get; set; }

        public virtual TbMembers Member { get; set; }
        public virtual TbNetworks Network { get; set; }
    }
}
