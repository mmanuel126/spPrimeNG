using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbNetworkDiscussionTopics
    {
        public int TopicId { get; set; }
        public int? NetworkId { get; set; }
        public int? MemberId { get; set; }
        public string TopicDesc { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual TbMembers Member { get; set; }
        public virtual TbNetworks Network { get; set; }
    }
}
