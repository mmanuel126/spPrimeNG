using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbSchoolsIlike
    {
        public int SchoolLikeId { get; set; }
        public int SchoolId { get; set; }
        public int MemberId { get; set; }
        public string SchoolType { get; set; }
    }
}
