using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbMemberPosts
    {
        public TbMemberPosts()
        {
            TbMemberPostResponses = new HashSet<TbMemberPostResponses>();
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PostDate { get; set; }
        public string AttachFile { get; set; }
        public int? MemberId { get; set; }
        public int? FileType { get; set; }

        public virtual TbMembers Member { get; set; }
        public virtual ICollection<TbMemberPostResponses> TbMemberPostResponses { get; set; }
    }
}
