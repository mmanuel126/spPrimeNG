using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbMemberProfile
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public bool? ShowSexInProfile { get; set; }
        public string Dobmonth { get; set; }
        public string Dobday { get; set; }
        public string Dobyear { get; set; }
        public bool? ShowDobtype { get; set; }
        public string Hometown { get; set; }
        public string HomeNeighborhood { get; set; }
        public string CurrentStatus { get; set; }
        public int? InterestedInType { get; set; }
        public bool? LookingForEmployment { get; set; }
        public bool? LookingForRecruitment { get; set; }
        public bool? LookingForPartnership { get; set; }
        public bool? LookingForNetworking { get; set; }
        public string PicturePath { get; set; }
        public DateTime? JoinedDate { get; set; }
        public string CurrentCity { get; set; }
        public string TitleDesc { get; set; }
        public string Sport { get; set; }
        public string PreferredPosition { get; set; }
        public string SecondaryPosition { get; set; }
        public string LeftRightHandFoot { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Bio { get; set; }

        public virtual TbMembers Member { get; set; }
    }
}
