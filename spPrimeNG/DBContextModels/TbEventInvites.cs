using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbEventInvites
    {
        public int InviteId { get; set; }
        public int? EventId { get; set; }
        public int? MemberId { get; set; }
        public string Email { get; set; }
        public string Rsvpstatus { get; set; }

        public virtual TbEvents Event { get; set; }
        public virtual TbMembers Member { get; set; }
    }
}
