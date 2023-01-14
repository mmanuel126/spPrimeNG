using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbRecentNews
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HeaderText { get; set; }
        public DateTime? PostingDate { get; set; }
        public string TextField { get; set; }
        public string NavigateUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}
