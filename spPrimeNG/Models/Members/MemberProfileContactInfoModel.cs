
namespace sportprofiles.Models.Members
{
    public class MemberProfileContactInfoModel
    {
        public string Email  {get; set; } = string.Empty;
        public string OtherEmail  {get; set; } = string.Empty;
        public string Facebook  {get; set; } = string.Empty;        
        public string Instagram  {get; set; } = string.Empty;
        public string Twitter  {get; set; } = string.Empty;
        public string Website {get; set;} = string.Empty;
        public string HomePhone  {get; set; } = string.Empty;
        public string CellPhone  {get; set; } = string.Empty;
        public string Address  {get; set; } = string.Empty;
        public string City  {get; set; } = string.Empty;
        public string Neighborhood  {get; set; } = string.Empty;
        public string State  {get; set; } = string.Empty;
        public string Zip  {get; set; } = string.Empty;
        public bool ShowAddress  {get; set; }
        public bool ShowEmailToMembers  {get; set; } 
        public bool ShowCellPhone  {get; set; }
        public bool ShowHomePhone  {get; set; }        
    }
}