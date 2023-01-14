
namespace sportprofiles.Models.Events
{
    public class EventPostModel
    {
        public string PostID  {get; set; } = string.Empty;
        public string Title  {get; set; } = string.Empty;
        public string Description  {get; set; } = string.Empty;
        public string PostDate  {get; set; } = string.Empty;
        public string AttachFile  {get; set; } = string.Empty;
        public string MemberID  {get; set; } = string.Empty;
        public string PicturePath  {get; set; } = string.Empty;
        public string MemberName  {get; set; } = string.Empty;
        public string FirstName  {get; set; } = string.Empty;
        public string EventID  {get; set; } = string.Empty;

    }

    public class EventInfoModel
    {
        public string InviteID  {get; set; } = string.Empty;
        public string email  {get; set; } = string.Empty;
        public string RSVPStatus  {get; set; } = string.Empty;
        public string EventID  {get; set; } = string.Empty;
        public string EventID1  {get; set; } = string.Empty;
        public string NetworkID  {get; set; } = string.Empty;
        public string MemberID  {get; set; } = string.Empty;
        public string StartDate  {get; set; } = string.Empty;
        public string StartTime  {get; set; } = string.Empty;

        public string StartEndTime  {get; set; } = string.Empty;
        public string EndDate  {get; set; } = string.Empty;
        public string EndTime  {get; set; } = string.Empty;
        public string EndEndTime  {get; set; } = string.Empty;
        public string PlanningWhat  {get; set; } = string.Empty;
        public string Location  {get; set; } = string.Empty;
        public string Street  {get; set; } = string.Empty;
        public string City  {get; set; } = string.Empty;
        public string State  {get; set; } = string.Empty;

        public string Zip  {get; set; } = string.Empty;
        public string MoreInfo  {get; set; } = string.Empty;
        public string InviteAllGroupMembers  {get; set; } = string.Empty;
        public string AnyoneCanViewRSVP  {get; set; } = string.Empty;
        public string ShowGuestList  {get; set; } = string.Empty;
        public string EventImg  {get; set; } = string.Empty;
        public string CreatedDate  {get; set; } = string.Empty;
        public string MemberName  {get; set; } = string.Empty;
        public string NetworkName  {get; set; } = string.Empty;
    }


    public class EventGuestsModel
    {
        public string InviteID  {get; set; } = string.Empty;
        public string MemberID  {get; set; } = string.Empty;
        public string MemberName  {get; set; } = string.Empty;
        public string Email  {get; set; } = string.Empty;
        public string RSVPstatus  {get; set; } = string.Empty;
        public string ImageName  {get; set; } = string.Empty;
    }


    public class EventTimesModel
    {
        public string TimeID  {get; set; } = string.Empty;
        public string Description  {get; set; } = string.Empty;
    }

    public class EventGuessListByTypeModel
    {
        public string EventID  {get; set; } = string.Empty;
        public string Type  {get; set; } = string.Empty;
    }

    public class MemberEventsModel
    {
        public string EventID  {get; set; } = string.Empty;
        public string Cnt  {get; set; } = string.Empty;
        public string PlanningWhat  {get; set; } = string.Empty;
        public string Location  {get; set; } = string.Empty;
        public string WEventDate  {get; set; } = string.Empty;
        public string RSVP  {get; set; } = string.Empty;
        public string EventParams  {get; set; } = string.Empty;
        public string StartDate  {get; set; } = string.Empty;
        public string EndDate  {get; set; } = string.Empty;
        public string ShowCancel  {get; set; } = string.Empty;
        public string EventImg  {get; set; } = string.Empty;
    }

    public class ContactsModel
    {
        public string MemberID  {get; set; } = string.Empty;
        public string MemberName  {get; set; } = string.Empty;
        public string ImageName  {get; set; } = string.Empty;
    }

    public class EventInviteModel
    {
        public string NetworkID  {get; set; } = string.Empty;
        public string EventID  {get; set; } = string.Empty;
        public string MemberID  {get; set; } = string.Empty;
        public string Email  {get; set; } = string.Empty;
    }

    public class RsvPstatusModel
    {
        public string EventID  {get; set; } = string.Empty;
        public string MemberID  {get; set; } = string.Empty;
        public string Status  {get; set; } = string.Empty;
    }
}
