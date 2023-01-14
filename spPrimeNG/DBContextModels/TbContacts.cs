using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbContacts
    {
        public int MemberId { get; set; }
        public int ContactId { get; set; }
        public int Status { get; set; }
        public DateTime? DateStamp { get; set; }

        public virtual TbMembers Contact { get; set; }
        public virtual TbMembers Member { get; set; }
    }
}
