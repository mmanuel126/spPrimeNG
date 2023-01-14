using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbIssues
    {
        public int IssueId { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public int? IssueType { get; set; }
        public string Description { get; set; }
    }
}
