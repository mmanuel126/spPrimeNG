using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbChatMessages
    {
        public int MessageId { get; set; }
        public int? RoomId { get; set; }
        public int? UserId { get; set; }
        public int? ToUserId { get; set; }
        public string Text { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string Color { get; set; }

        public virtual TbChatRoom Room { get; set; }
        public virtual TbMembers ToUser { get; set; }
        public virtual TbMembers User { get; set; }
    }
}
