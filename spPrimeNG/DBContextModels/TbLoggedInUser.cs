using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbLoggedInUser
    {
        public int LoggedInUserId { get; set; }
        public int? UserId { get; set; }
        public int? RoomId { get; set; }

        public virtual TbChatRoom Room { get; set; }
        public virtual TbMembers User { get; set; }
    }
}
