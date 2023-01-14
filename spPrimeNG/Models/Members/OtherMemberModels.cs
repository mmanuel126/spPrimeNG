
namespace sportprofiles.Models.Members
{
    public class MemberDatesModel
    {
        public string JoinedDate  {get; set; } = string.Empty;
        public string PicturePath  {get; set; } = string.Empty;
        public string MemberName  {get; set; } = string.Empty;
        public string CurrentCity  {get; set; } = string.Empty;
        public string BirthDate  {get; set; } = string.Empty;
    }

    public class MemberAlbumsModel
    {
        public string AlbumID  {get; set; } = string.Empty;
        public string AlbumName  {get; set; } = string.Empty;
        public string CreatedDate  {get; set; } = string.Empty;
        public string Description  {get; set; } = string.Empty;
        public string PhotoCount  {get; set; } = string.Empty;
        public string FileName  {get; set; } = string.Empty;
    }

    public class AlbumPhotosModel
    {
        public string countID  {get; set; } = string.Empty;
        public string photoID  {get; set; } = string.Empty;
        public string fileName  {get; set; } = string.Empty;
        public string photodesc  {get; set; } = string.Empty;
        public string photoName  {get; set; } = string.Empty;
        public string memberID  {get; set; } = string.Empty;
        public string memberName  {get; set; } = string.Empty;
    }

    public class MemberInfoByEmailModel
    {
        public string friendID  {get; set; } = string.Empty;
        public string picturePath  {get; set; } = string.Empty;
        public string email  {get; set; } = string.Empty;
        public string name  {get; set; } = string.Empty;
    }

    public class RecentActivitiesModel
    {
        public string activityID  {get; set; } = string.Empty;
        public string description  {get; set; } = string.Empty;
        public string activityDate  {get; set; } = string.Empty;
        public string imageFile  {get; set; } = string.Empty;
    }

    public class RecentPostChildModel
    {
        public int? postResponseID  {get; set; }
        public int? postID  {get; set; } 
        public string description  {get; set; } = string.Empty;
        public string dateResponded  {get; set; } = string.Empty;
        public int? memberID  {get; set; }
        public string memberName  {get; set; } = string.Empty;
        public string firstName  {get; set; } = string.Empty;
        public string picturePath  {get; set; } = string.Empty;
    }

    public class RegisteredMembersModelV2
    {
        public string memberID  {get; set; } = string.Empty;
        public string email  {get; set; } = string.Empty;
        public string status  {get; set; } = string.Empty;
        public string firstName  {get; set; } = string.Empty;
        public string lastName  {get; set; } = string.Empty;
        public string sex  {get; set; } = string.Empty;
        public string picturePath  {get; set; } = string.Empty;
        public string joinedDate  {get; set; } = string.Empty;
        public string titleDesc  {get; set; } = string.Empty;
        public string sector  {get; set; } = string.Empty;
        public string industry  {get; set; } = string.Empty;
        public string registeredDate  {get; set; } = string.Empty;
    }

    public class MemberProfileGenInfoModel
    {
        public string MemberID  {get; set; } = string.Empty;
        public string FirstName  {get; set; } = string.Empty;
        public string MiddleName  {get; set; } = string.Empty;
        public string LastName  {get; set; } = string.Empty;
        public string Sex  {get; set; } = string.Empty;
        public bool ShowSexInProfile  {get; set; }
        public string DOBMonth  {get; set; } = string.Empty;
        public string DOBDay  {get; set; } = string.Empty;
        public string DOBYear  {get; set; } = string.Empty;
        public bool ShowDOBType  {get; set; } 
        public string Hometown  {get; set; } = string.Empty;
        public string HomeNeighborhood  {get; set; } = string.Empty;
        public string CurrentStatus  {get; set; } = string.Empty;
        public string InterestedInType  {get; set; } = string.Empty;
        public bool LookingForEmployment  {get; set; }
        public bool LookingForRecruitment  {get; set; }
        public bool LookingForPartnership  {get; set; } 
        public bool LookingForNetworking  {get; set; } 
        public string PicturePath  {get; set; } = string.Empty;
        public string JoinedDate  {get; set; } = string.Empty;
        public string CurrentCity  {get; set; } = string.Empty;
        public string TitleDesc  {get; set; } = string.Empty;
        public string Sport  {get; set; } = string.Empty;
        public string Bio  {get; set; } = string.Empty;
        public string Height  {get; set; } = string.Empty;
        public string Weight  {get; set; } = string.Empty;
        public string LeftRightHandFoot  {get; set; } = string.Empty;
        public string PreferredPosition  {get; set; } = string.Empty;
        public string SecondaryPosition  {get; set; } = string.Empty;
        public string InterestedDesc  {get; set; } = string.Empty;
    }

    public class InterestsModel
    {
        public string InterestID  {get; set; } = string.Empty;
        public string InterestedDesc  {get; set; } = string.Empty;
    }

    public class MemberPostsModel
    {
        public string PostID  {get; set; } = string.Empty;
        public string Title  {get; set; } = string.Empty;
        public string Description  {get; set; } = string.Empty;
        public string DatePosted  {get; set; } = string.Empty;
        public string AttachFile  {get; set; } = string.Empty;
        public string MemberID  {get; set; } = string.Empty;
        public string PicturePath  {get; set; } = string.Empty;
        public string MemberName  {get; set; } = string.Empty;
        public string FirstName  {get; set; } = string.Empty;
        public string ChildPostCnt  {get; set; } = string.Empty;
    }

    public class ValidateNewRegisteredUserModel
    {
        public string MemberId  {get; set; } = string.Empty;
        public string Email  {get; set; } = string.Empty;
        public string FirstName  {get; set; } = string.Empty;
        public string LastName  {get; set; } = string.Empty;
        public string PassWord  {get; set; } = string.Empty;
        public string UserImage  {get; set; } = string.Empty;
        public string Title  {get; set; } = string.Empty; 
    }

    public class CodeAndNameForgotPwdModel
    {
        public string CodeID  {get; set; } = string.Empty;
        public string FirstName  {get; set; } = string.Empty;
    }

    public class MemberNameInfoModel
    {
        public string FirstName  {get; set; } = string.Empty;
        public string LastName  {get; set; } = string.Empty;
        public string MiddleName  {get; set; } = string.Empty;
        public string Email  {get; set; } = string.Empty;
        public string SecurityQuestion  {get; set; } = string.Empty;
        public string SecurityAnswer  {get; set; } = string.Empty;
        public string PassWord  {get; set; } = string.Empty;
    }

    public class MemberNetworksModel
    {
        public string NetworkID  {get; set; } = string.Empty;
        public string NetworkName  {get; set; } = string.Empty;
        public string NetworkDesc  {get; set; } = string.Empty;
        public string CategoryID  {get; set; } = string.Empty;
        public string NetworkImage  {get; set; } = string.Empty;
        public string MemberCount  {get; set; } = string.Empty;
        public string CreatedDate  {get; set; } = string.Empty;
        public string NetworkOwner  {get; set; } = string.Empty;
    }

    public class PrivacySearchSettingsModel
    {
        public string ID  {get; set; } = string.Empty;
        public string MemberID  {get; set; } = string.Empty;
        public string Profile  {get; set; } = string.Empty;
        public string BasicInfo  {get; set; } = string.Empty;
        public string PersonalInfo  {get; set; } = string.Empty;
        public string PhotosTagOfYou  {get; set; } = string.Empty;
        public string VideosTagOfYou  {get; set; } = string.Empty;
        public string ContactInfo  {get; set; } = string.Empty;
        public string Education  {get; set; } = string.Empty;
        public string WorkInfo  {get; set; } = string.Empty;
        public string IMdisplayName  {get; set; } = string.Empty;
        public string MobilePhone  {get; set; } = string.Empty;
        public string OtherPhone  {get; set; } = string.Empty;
        public string EmailAddress  {get; set; } = string.Empty;
        public string Visibility  {get; set; } = string.Empty;
        public bool ViewProfilePicture  {get; set; }
        public bool ViewFriendsList  {get; set; }  
        public bool ViewLinksToRequestAddingYouAsFriend  {get; set; } 
        public bool ViewLinkTSendYouMsg  {get; set; } 
        public string Email  {get; set; } = string.Empty;
    }

    public class UserModel
    {
        public string MemberID  {get; set; } = string.Empty;
        public string Name  {get; set; } = string.Empty;
        public string Email  {get; set; } = string.Empty;
        public string Picturepath  {get; set; } = string.Empty;
        public string Title  {get; set; } = string.Empty;
        public string CurrentStatus  {get; set; } = string.Empty;

    }

    public class SportsListModel {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description  {get; set; } = string.Empty;
    }

    public class AdsModel {
        public int ID  {get; set; } 
        public string Name  {get; set; } = string.Empty;
        public string HeaderText  {get; set; } = string.Empty;
        public DateTime PostingDate  {get; set; }
        public string TextField  {get; set; } = string.Empty;
        public string NavigateUrl  {get; set; } = string.Empty;
        public string ImageUrl  {get; set; } = string.Empty;
        public string Type  {get; set; } = string.Empty;
    }

    public class PostsModel
    {
        public int MemberID  {get; set; }  
        public int PostID  {get; set; }  
        public string PostMsg  {get; set; } = string.Empty;
    }

    public class YoutubeChannelModel
    {
        public string MemberID  {get; set; } = string.Empty;
        public string ChannelID  {get; set; } = string.Empty;
    }

    public class InstagramURLModel
    {
        public string MemberID  {get; set; } = string.Empty;
        public string InstagramURL  {get; set; } = string.Empty;
    }

}

