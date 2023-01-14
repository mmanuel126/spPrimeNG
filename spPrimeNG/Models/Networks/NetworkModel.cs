using System;
namespace sportprofiles.Models.Networks 
{
	public class MemberNetworkResult
	{
		public string EntityID {get; set; } = string.Empty;
		public string EntityName {get; set; } = string.Empty;
		public string PicturePath {get; set; } = string.Empty;
		public string FirstName {get; set; } = string.Empty;
		public string LastName {get; set; } = string.Empty;
		public string CityState {get; set; } = string.Empty;
		public string Id {get; set; } = string.Empty;
		public string Name {get; set; } = string.Empty;
	}

    public class NetworkInfoModel
    {
        public string NetworkID {get; set; } = string.Empty;
        public string NetworkName {get; set; } = string.Empty;
        public string NetworkDesc {get; set; } = string.Empty;
        public string CategoryID {get; set; } = string.Empty;
        public string NetworkImage {get; set; } = string.Empty;
        public string MemberCount {get; set; } = string.Empty;
        public string CreatedDate {get; set; } = string.Empty;
        public string NetworkOwner {get; set; } = string.Empty;
		public string CategoryTypeID {get; set; } = string.Empty;
		public string RecentNews {get; set; } = string.Empty;
        public string Office {get; set; } = string.Empty;
		public string Email {get; set; } = string.Empty;
		public string WebSite {get; set; } = string.Empty;
		public string Street {get; set; } = string.Empty;
		public string City {get; set; } = string.Empty;
        public string State {get; set; } = string.Empty;
        public string Zip {get; set; } = string.Empty;
        public string InActive {get; set; } = string.Empty;
        public string CategoryDesc {get; set; } = string.Empty;
        public string CategoryTypeDesc {get; set; } = string.Empty;
        public string Access {get; set; } = string.Empty;
        public string IsAlreadyMemberID {get; set; } = string.Empty;
	}

    public class NetworkMemberModel {
        public string MemberID {get; set; } = string.Empty;
        public string MemberName {get; set; } = string.Empty;
        public string PicturePath {get; set; } = string.Empty;
        public string NetworkID {get; set; } = string.Empty;
        public string IsOwner {get; set; } = string.Empty;
        public string IsAdmin {get; set; } = string.Empty;
        public string JoinedDate {get; set; } = string.Empty;
        public string Access {get; set; } = string.Empty;
        public string TitleDesc {get; set; } = string.Empty;
		public string InviteID {get; set; } = string.Empty;
    }

    public class RecentNetworkActivitiesResult {
		public string ActivityID {get; set; } = string.Empty;
		public string Description {get; set; } = string.Empty;
		public string ActivityDate {get; set; } = string.Empty;
		public string ImageFile {get; set; } = string.Empty;
    }

	public class NetworkPostsModel
	{
		public string PostID {get; set; } = string.Empty;
        public string Title {get; set; } = string.Empty;
		public string Description {get; set; } = string.Empty;
		public string PostDate {get; set; } = string.Empty;
        public string AttachFile {get; set; } = string.Empty;
		public string MemberID {get; set; } = string.Empty;
		public string PicturePath {get; set; } = string.Empty;
		public string MemberName {get; set; } = string.Empty;
		public string FirstName {get; set; } = string.Empty;
	}

	public class NetworkChildPostsModel
	{
		public string PostResponseID {get; set; } = string.Empty;
        public string PostID {get; set; } = string.Empty;
		public string Description {get; set; } = string.Empty;
		public string ResponseDate {get; set; } = string.Empty;
		public string MemberID {get; set; } = string.Empty;
        public string PicturePath {get; set; } = string.Empty;
        public string MemberName {get; set; } = string.Empty;
        public string FirstName {get; set; } = string.Empty;
	}

	public class NetworkPhotosModel
	{
		public string CountID {get; set; } = string.Empty;
		public string PhotoID {get; set; } = string.Empty;
		public string MemberID {get; set; } = string.Empty;
		public string MemberName {get; set; } = string.Empty;
		public string PhotoName {get; set; } = string.Empty;
		public string PhotoDesc {get; set; } = string.Empty;
        public string FileName {get; set; } = string.Empty;
		public string Params {get; set; } = string.Empty;
        public string CreatedDate {get; set; } = string.Empty;
	}

	public class NetworkEventModel
	{
		public string EventID {get; set; } = string.Empty;
		public string EventImg {get; set; } = string.Empty;
		public string PlanningWhat {get; set; } = string.Empty;
		public string Location {get; set; } = string.Empty;
		public string EventDate {get; set; } = string.Empty;
		public string RSVP {get; set; } = string.Empty;
		public string EventParams {get; set; } = string.Empty;
		public string ShowCancel {get; set; } = string.Empty;		
	}

	public class NetworkTopicsModel
	{
		public string TopicID {get; set; } = string.Empty;
		public string TopicDesc {get; set; } = string.Empty;
		public string MemberName {get; set; } = string.Empty;
		public string CreatedDate {get; set; } = string.Empty;
		public string MemberID {get; set; } = string.Empty;
		public string TopicPostCnt {get; set; } = string.Empty;
		public string LatestPostMemberID {get; set; } = string.Empty;
		public string LatestPostMemberName {get; set; } = string.Empty;
        public string LatestPostDate {get; set; } = string.Empty;
	}


	public class NetworkModel
    {
        public int NetworkID {get; set; } 
        public string NetworkName {get; set; } = string.Empty;
        public string NetworkDesc {get; set; } = string.Empty;
        public int CategoryID {get; set; } 
        public string NetworkImage {get; set; } = string.Empty;
        public string MemberCount {get; set; } = string.Empty;
        public DateTime  CreatedDate {get; set; } 
        public string NetworkOwner {get; set; } = string.Empty;
	}

}
