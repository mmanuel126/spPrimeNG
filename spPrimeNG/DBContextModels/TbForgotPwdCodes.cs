using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbForgotPwdCodes
    {
        public int CodeId { get; set; }
        public string Email { get; set; }
        public DateTime? CodeDate { get; set; }
        public int? Status { get; set; }
    }
}
