using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbMemberProfilePersonalInfo
    {
        public int MemberId { get; set; }
        public string Activities { get; set; }
        public string Interests { get; set; }
        public string FavoriteMusic { get; set; }
        public string FavoriteTvshows { get; set; }
        public string FavoriteMovies { get; set; }
        public string FavoriteBooks { get; set; }
        public string FavoriteQuotations { get; set; }
        public string AboutMe { get; set; }
        public string SpecialSkills { get; set; }

        public virtual TbMembers Member { get; set; }
    }
}
