
namespace sportprofiles.Models.Connections
{
    public class MemberConnectionModel
    {
        public string FriendName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string ConnectionID { get; set; } = string.Empty;
        public string ShowType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string TitleDesc { get; set; } = string.Empty;
        public string Params { get; set; } = string.Empty;
        public string ParamsAV { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string LabelText { get; set; } = string.Empty;
        public string NameAndID { get; set; } = string.Empty;
    }

    public class MemberPhoneBookModel {
        public string FriendName { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public string CellPhone { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string ConnectionID { get; set; } = string.Empty;
    }

	public class MemberByTypeModel
	{
		public string MemberID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string TypeVal { get; set; } = string.Empty;
        public string IsFriend { get; set; } = string.Empty;
        public string IsSamePerson { get; set; } = string.Empty;
    }

    public class EntityModel {
        public string EntityID { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CityState { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

	public class ConnectionSearchModel
	{
		public string EntityID { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CityState { get; set; } = string.Empty;
        public string LabelText { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NameAndID { get; set; } = string.Empty;
        public string Params { get; set; } = string.Empty;
        public string ParamsAV { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MemberCount { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string EventDate { get; set; } = string.Empty;
        public string Rsvp { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;

    }

	public class NetworkSearchModel
	{
		public string EntityID { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string LabelText { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NameAndID { get; set; } = string.Empty;
        public string Params { get; set; } = string.Empty;
        public string ParamsAV { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MemberCount { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string CityState { get; set; } = string.Empty;
        public string EventDate { get; set; } = string.Empty;
        public string Rsvp { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }

	public class EventSearchModel
	{
		public string EntityID { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string EventDate { get; set; } = string.Empty;
        public string Rsvp { get; set; } = string.Empty;
        public string Params { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MemberCount { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string CityState { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NameAndID { get; set; } = string.Empty;
        public string EventParams { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string LabelText { get; set; } = string.Empty;
        public string ShowCancel { get; set; } = string.Empty;
        public string ParamsAV { get; set; } = string.Empty;
	}

	public class SearchModel
	{
        public int EntityID { get; set; }
		public string EntityName { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string EventDate { get; set; } = string.Empty;
        public string Rsvp { get; set; } = string.Empty;
        public string Params { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MemberCount { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } 
        public string CityState { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NameAndID { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string LabelText { get; set; } = string.Empty;
        public string ShowCancel { get; set; } = string.Empty;
        public string ParamsAV { get; set; } = string.Empty;
        public string Stype { get; set; } = string.Empty;
    }

	public class ResultModel
	{
		public int MemberID { get; set; } = 0;
    }
}
