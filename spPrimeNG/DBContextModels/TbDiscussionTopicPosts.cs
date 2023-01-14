using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbDiscussionTopicPosts
    {
        public int PostId { get; set; }
        public int? TopicId { get; set; }
        public int? MemberId { get; set; }
        public string PostDesc { get; set; }
        public DateTime? PostDate { get; set; }

        public virtual TbMembers Member { get; set; }
    }
}
