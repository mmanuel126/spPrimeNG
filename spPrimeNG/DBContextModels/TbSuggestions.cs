using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbSuggestions
    {
        public int MemberId { get; set; }
        public string ContactEmail { get; set; }

        public virtual TbMembers Member { get; set; }
    }
}
