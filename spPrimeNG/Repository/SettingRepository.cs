using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using sportprofiles.DBContextModels;
using sportprofiles.Models.Members;
using sportprofiles.Models.Settings;

namespace sportprofiles.Repository
{
    /// <summary>
    /// Describes the functionalities for accessing data for Member settings
    /// </summary>
    public class SettingRepository : ISettingRepository
    {

        readonly dbContexts _context;

        #region methods...

        public SettingRepository(dbContexts context)
        {
            _context = context;
        }

        /// <summary>
        /// Get member id's name information
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<MemberNameInfoModel> GetMemberNameInfo(int memberID)
        {
            var lst = (from m in _context.TbMembers
                       join pp in _context.TbMemberProfile on m.MemberId equals pp.MemberId
                       where m.MemberId == memberID

                       select new MemberNameInfoModel()
                       {
                           FirstName = pp.FirstName,
                           LastName = pp.LastName,
                           MiddleName = pp.MiddleName,
                           Email = m.Email,
                           SecurityQuestion = m.SecurityQuestion.ToString() ?? "", 
                           SecurityAnswer = m.SecurityAnswer,
                           PassWord = m.Password
                       }).ToList();

            return lst;
        }

        /// <summary>
        /// Save member ID's name information
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="fName"></param>
        /// <param name="mName"></param>
        /// <param name="lName"></param>
        public void SaveMemberNameInfo(int memberID, string fName, string mName, string lName)
        {
            var p = (from m in _context.TbMemberProfile where m.MemberId == memberID select m).First();
            p.LastName = lName;
            p.FirstName = fName;
            p.MiddleName = mName;
            _context.SaveChanges();
        }

        /// <summary>
        /// Saves member ID email information 
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="email"></param>
        public void SaveMemberEmailInfo(int memberID, string email)
        {
            List<TbMembers> p = (from m in _context.TbMembers where m.Email == email select m).ToList();
            if (p.Count == 0)
            {
                var q = (from m in _context.TbMembers where m.MemberId == memberID select m).First();
                q.Email = email;
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Save member iDs save password information
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="pwd"></param>
        public void SavePasswordInfo(string memberID, string pwd)
        {
            var q = (from m in _context.TbMembers where m.MemberId == Convert.ToInt32(memberID) select m).First();
            q.Password = pwd;
            _context.SaveChanges();
        }

        /// <summary>
        /// Saves member IDs security question information
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="questionID"></param>
        /// <param name="answer"></param>
        public void SaveSecurityQuestionInfo(int memberID, int questionID, string answer)
        {
            var q = (from m in _context.TbMembers where m.MemberId == memberID select m).First();
            q.SecurityQuestion = questionID;
            q.SecurityAnswer = answer;
            _context.SaveChanges();
        }

        /// <summary>
        /// Deactivate account for member ID.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="reason"></param>
        /// <param name="explanation"></param>
        /// <param name="futureEmail"></param>
        public void DeactivateAccount(int memberID, int reason, string explanation, bool? futureEmail)
        {
            var q = (from m in _context.TbMembers where m.MemberId == memberID select m).First();
            q.Status = 3;
            q.DeactivateReason = reason;
            q.DeactivateExplanation = explanation;
            q.FutureEmails = futureEmail;
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets member IDs networks
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<MemberNetworksModel> GetMemberNetworks(int memberID)
        {
            //not yet implemented
            return new List<MemberNetworksModel>();
        }

        /// <summary>
        /// Validates network ID exist
        /// </summary>
        /// <param name="networkID"></param>
        /// <returns></returns>
        public bool ValidateNetworkID(int networkID)
        {
            var q = (from n in _context.TbNetworks where n.NetworkId == networkID select n).ToList();
            return (q.Count != 0);
        }

        /// <summary>
        /// Validates member ID exist
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public bool ValidateMemberId(int memberID)
        {
            var q = (from m in _context.TbMembers where m.MemberId == memberID select m).ToList();
            return (q.Count != 0);
        }

        /// <summary>
        /// Set member id to join a network
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="networkID"></param>
        public void JoinNetwork(int memberID, int networkID)
        {
            var memID = new SqlParameter("@MemberID", memberID);
            var netID = new SqlParameter("@NetworkID", networkID);
            _context.Database.ExecuteSqlRaw("EXEC spJoinNetwork @MemberID, @NetworkID", memID, netID);
        }

        /// <summary>
        /// Join network by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="networkID"></param>
        public void JoinNetworkByEmail(string email, int networkID)
        {
            var em = new SqlParameter("@Email", email);
            var netID = new SqlParameter("@NetworkID", networkID);
            _context.Database.ExecuteSqlRaw("EXEC spJoinNetworkByEmail @Email, @NetworkID", em, netID);
        }

        /// <summary>
        /// check to see if memberID is a member of network 
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="networkID"></param>
        /// <returns></returns>
        public bool isNetworkMember(int memberID, int networkID)
        {
            List<TbNetworkMembers> q = (from m in _context.TbNetworkMembers where m.MemberId == memberID && m.NetworkId == networkID select m).ToList();
            return (q.Count != 0);
        }

        /// <summary>
        /// Creates network request
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="networkName"></param>
        /// <param name="type"></param>
        /// <param name="purpose"></param>
        public void CreateNetworkRequest(int memberID, string networkName, string type, string purpose)
        {
            var memID = new SqlParameter("@MemberID", memberID);
            var netName = new SqlParameter("@NetworkName", networkName);
            var types = new SqlParameter("@Type", type);
            var purposes = new SqlParameter("@Purpose", purpose);
            _context.Database.ExecuteSqlRaw("EXEC spCreateNetworkRequest @MemberID, @NetworkName, @Type, @Purpose", memID, netName, types, purposes);
        }

        public List<NotificationsSettingModel> GetMemberNotifications(int memberId)
        {
            var q = (from m in _context.TbNotificationSettings
                     where m.MemberId == memberId
                     select new NotificationsSettingModel()
                     {
                         MemberID = m.MemberId,
                         LG_SendMsg = m.LgSendMsg ?? false,
                         LG_AddAsFriend = m.LgAddAsFriend ?? false,
                         LG_ConfirmFriendShipRequest = m.LgConfirmFriendShipRequest ?? false,
                         GP_InviteYouToJoin = m.GpInviteYouToJoin ?? false,
                         GP_MakesYouAGPAdmin = m.GpMakesYouAgpadmin ?? false,
                         GP_RepliesToYourDiscBooardPost = m.GpRepliesToYourDiscBooardPost ?? false,
                         GP_ChangesTheNameOfGroupYouBelong = m.GpChangesTheNameOfGroupYouBelong ?? false,
                         EV_DateChanged = m.EvDateChanged ?? false,
                         EV_InviteToEvent = m.EvInviteToEvent ?? false,
                         HE_RepliesToYourHelpQuest = m.HeRepliesToYourHelpQuest ?? false
                     }).ToList();
            return q;
        }

        public void SaveNotificationSettings(
                  int MemberID,
                  bool LG_SendMsg,
                  bool LG_AddAsFriend,
                  bool LG_ConfirmFriendShipRequest,
                  bool GP_InviteYouToJoin,
                  bool GP_MakesYouAGPAdmin,
                  bool GP_RepliesToYourDiscBooardPost,
                  bool GP_ChangesTheNameOfGroupYouBelong,
                  bool EV_InviteToEvent,
                  bool EV_DateChanged,
                  bool HE_RepliesToYourHelpQuest
            )
        {
            var n = (from m in _context.TbNotificationSettings where m.MemberId == MemberID select m).ToList();

            if (n.Count != 0)
            {
                var q = n.First();
                q.LgSendMsg = LG_SendMsg;
                q.LgAddAsFriend = LG_AddAsFriend;
                q.LgConfirmFriendShipRequest = LG_ConfirmFriendShipRequest;
                q.GpInviteYouToJoin = GP_InviteYouToJoin;
                q.GpMakesYouAgpadmin = GP_MakesYouAGPAdmin;
                q.GpRepliesToYourDiscBooardPost = GP_RepliesToYourDiscBooardPost;
                q.GpChangesTheNameOfGroupYouBelong = GP_ChangesTheNameOfGroupYouBelong;
                q.EvInviteToEvent = EV_InviteToEvent;
                q.EvDateChanged = EV_DateChanged;
                q.HeRepliesToYourHelpQuest = HE_RepliesToYourHelpQuest;
                _context.SaveChanges();
            }
            else
            {
                TbNotificationSettings ps = new TbNotificationSettings();
                ps.MemberId = MemberID;
                ps.LgSendMsg = LG_SendMsg;
                ps.LgAddAsFriend = LG_AddAsFriend;
                ps.LgConfirmFriendShipRequest = LG_ConfirmFriendShipRequest;
                ps.GpInviteYouToJoin = GP_InviteYouToJoin;
                ps.GpMakesYouAgpadmin = GP_MakesYouAGPAdmin;
                ps.GpRepliesToYourDiscBooardPost = GP_RepliesToYourDiscBooardPost;
                ps.GpChangesTheNameOfGroupYouBelong = GP_ChangesTheNameOfGroupYouBelong;
                ps.EvInviteToEvent = EV_InviteToEvent;
                ps.EvDateChanged = EV_DateChanged;
                ps.HeRepliesToYourHelpQuest = HE_RepliesToYourHelpQuest;
                _context.TbNotificationSettings.Add(ps);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Get profile privacy settings
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<PrivacySearchSettingsModel> GetProfileSettings(int memberID)
        {
            List<PrivacySearchSettingsModel> lst = (from p in _context.TbMembersPrivacySettings
                                                    join m in _context.TbMembers on p.MemberId equals m.MemberId
                                                    where p.MemberId == memberID
                                                    select new PrivacySearchSettingsModel()
                                                    {
                                                        ID = p.Id.ToString(),
                                                        MemberID = p.MemberId.ToString(),
                                                        Profile = p.Profile.ToString() ?? "",
                                                        BasicInfo = p.BasicInfo.ToString() ?? "",
                                                        PersonalInfo = p.PersonalInfo.ToString() ?? "",
                                                        PhotosTagOfYou = p.PhotosTagOfYou.ToString() ?? "",
                                                        VideosTagOfYou = p.VideosTagOfYou.ToString() ?? "",
                                                        ContactInfo = p.ContactInfo.ToString() ?? "",
                                                        Education = p.Education.ToString() ?? "",
                                                        WorkInfo = p.WorkInfo.ToString() ?? "",
                                                        IMdisplayName = p.ImdisplayName.ToString() ?? "",
                                                        MobilePhone = p.MobilePhone.ToString() ?? "",
                                                        OtherPhone = p.OtherPhone.ToString() ?? "",
                                                        EmailAddress = p.EmailAddress.ToString() ?? "",
                                                        Visibility = p.Visibility.ToString() ?? "",
                                                        ViewProfilePicture = p.ViewProfilePicture ?? false,
                                                        ViewFriendsList = p.ViewFriendsList ?? false,
                                                        ViewLinksToRequestAddingYouAsFriend = p.ViewLinkToRequestAddingYouAsFriend ?? false,
                                                        ViewLinkTSendYouMsg = p.ViewLinkToSendYouMsg ?? false,
                                                        Email = m.Email.ToString()
                                                    }).ToList();
            return lst;
        }

        /// <summary>
        /// Saves the profile settings.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        public void SaveProfileSettings(
                  int memberID, PrivacySearchSettingsModel body)
        {
            var p = (from m in _context.TbMembersPrivacySettings where m.MemberId == memberID select m).First();
            if (p != null)
            {
                p.MemberId = memberID;
                p.Profile = int.Parse(body.Profile);
                p.BasicInfo = int.Parse(body.BasicInfo);
                p.PersonalInfo = int.Parse(body.PersonalInfo);
                p.PhotosTagOfYou = int.Parse(body.PhotosTagOfYou);
                p.VideosTagOfYou = int.Parse(body.VideosTagOfYou);
                p.ContactInfo = int.Parse(body.ContactInfo);
                p.Education = int.Parse(body.Education);
                p.WorkInfo = int.Parse(body.WorkInfo);
                p.ImdisplayName = int.Parse(body.IMdisplayName);
                p.MobilePhone = int.Parse(body.MobilePhone);
                p.OtherPhone = int.Parse(body.OtherPhone);
                p.EmailAddress = int.Parse(body.EmailAddress);
                _context.SaveChanges();
            }
            else
            {
                TbMembersPrivacySettings ps = new TbMembersPrivacySettings();
                ps.MemberId = memberID;
                ps.Profile = int.Parse(body.Profile);
                ps.BasicInfo = int.Parse(body.BasicInfo);
                ps.PersonalInfo = int.Parse(body.PersonalInfo);
                ps.PhotosTagOfYou = int.Parse(body.PhotosTagOfYou);
                ps.VideosTagOfYou = int.Parse(body.VideosTagOfYou);
                ps.ContactInfo = int.Parse(body.ContactInfo);
                ps.Education = int.Parse(body.Education);
                ps.WorkInfo = int.Parse(body.WorkInfo);
                ps.ImdisplayName = int.Parse(body.IMdisplayName);
                ps.MobilePhone = int.Parse(body.MobilePhone);
                ps.OtherPhone = int.Parse(body.OtherPhone);
                ps.EmailAddress = int.Parse(body.EmailAddress);
                _context.TbMembersPrivacySettings.Add(ps);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Get privacy search settings
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<PrivacySearchSettingsModel> GetPrivacySearchSettings(int memberID)
        {
            return (GetProfileSettings(memberID));
        }

        /// <summary>
        /// saves privacy search settings
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="visibility"></param>
        /// <param name="viewProfilePicture"></param>
        /// <param name="viewFriendsList"></param>
        /// <param name="viewLinkToRequestAddingYouAsFriend"></param>
        /// <param name="viewLinkToSendYouMsg"></param>
        public void SavePrivacySearchSettings(
              int memberID,
              int visibility,
              bool viewProfilePicture,
              bool viewFriendsList,
              bool viewLinkToRequestAddingYouAsFriend,
              bool viewLinkToSendYouMsg)
        {
            var p = (from m in _context.TbMembersPrivacySettings where m.MemberId == memberID select m).First();
            if (p != null)
            {
                p.MemberId = memberID;
                p.Visibility = visibility;
                p.ViewProfilePicture = viewProfilePicture;
                p.ViewFriendsList = viewFriendsList;
                p.ViewLinkToRequestAddingYouAsFriend = viewLinkToRequestAddingYouAsFriend;
                p.ViewLinkToSendYouMsg = viewLinkToSendYouMsg;
                _context.SaveChanges();
            }
            else
            {
                TbMembersPrivacySettings ps = new TbMembersPrivacySettings();
                ps.MemberId = memberID;
                ps.Visibility = visibility;
                ps.ViewFriendsList = viewFriendsList;
                ps.ViewLinkToRequestAddingYouAsFriend = viewLinkToRequestAddingYouAsFriend;
                ps.ViewLinkToSendYouMsg = viewLinkToSendYouMsg;
                _context.TbMembersPrivacySettings.Add(ps);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Get all members for search text
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<TbMemberProfile> GetAllMembers(string searchText)
        {
            var lst = (from m in _context.TbMemberProfile where (m.FirstName.Contains(searchText)) || (m.LastName.Contains(searchText)) select m);
            return lst.ToList();
        }

        #endregion


        /// <summary>
        /// return true if email exist for member id
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmailExist(int memberID, string email)
        {
            var q = (from m in _context.TbMembers where m.Email == email && m.MemberId != memberID select m).ToList();
            if (q.Count != 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// returns a list of members profile pictures.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<TbMemberProfilePictures> GetMemberProfilePictures(int memberID)
        {
            var q = (from m in _context.TbMemberProfilePictures where m.MemberId == memberID && (m.Removed == false || m.Removed == null) select m).ToList();
            return (q.ToList());
        }

        /// <summary>
        /// returns members default profile picture.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public string GetMemberDefaultPicture(int memberID)
        {
            var q = (from m in _context.TbMemberProfilePictures where m.MemberId == memberID && m.IsDefault == true select m).ToList();
            if (q.Count != 0)
            {
                return (q[0].FileName.ToString());
            }
            else
                return "";
        }

        /// <summary>
        /// remove profile picture from list
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="noPhotoFilename"></param>
        public void RemoveProfilePicture(int memberID, string noPhotoFilename)
        {
            //update tb meber profile with new profile picture
            var mbr = (from m in _context.TbMemberProfile where m.MemberId == memberID select m).First();
            mbr.PicturePath = noPhotoFilename;
            _context.SaveChanges();

            //remove the old default picture for the member 
            var mq = (from q in _context.TbMemberProfilePictures where q.MemberId == memberID && q.IsDefault == true select q);
            if (mq.Count() != 0)
            {
                var f = mq.First();
                f.IsDefault = false;
                _context.SaveChanges();
            }
        }


        /// <summary>
        /// Set picture as default
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="profileID"></param>
        /// <param name="fileName"></param>
        public void SetPictureAsDefault(int memberID, int profileID, string fileName)
        {
            //remove the old default picture for the member 
            var mq = (from q in _context.TbMemberProfilePictures where q.MemberId == memberID && q.IsDefault == true select q);
            if (mq.Count() != 0)
            {
                var f = mq.First();
                f.IsDefault = false;
                _context.SaveChanges();
            }
            //set the new default picture 
            var newq = (from q in _context.TbMemberProfilePictures where q.MemberId == memberID && q.ProfileId == profileID select q);
            if (newq.Count() != 0)
            {
                var n = newq.First();
                n.IsDefault = true;
                _context.SaveChanges();
            }

            //update tb meber profile with new profile picture
            var mbr = (from m in _context.TbMemberProfile where m.MemberId == memberID select m).First();
            mbr.PicturePath = fileName;
            _context.SaveChanges();
        }


        /// <summary>
        /// Remove picture as profile
        /// </summary>
        /// <param name="profileID"></param>
        /// <param name="defaultFileName"></param>
        public void RemovePicture(int profileID, string defaultFileName)
        {
            //get profile id to delete and then delete it.
            var newq = (from q in _context.TbMemberProfilePictures where q.ProfileId == profileID select q);
            if (newq.Count() != 0)
            {
                bool isDef = false;
                int memberID = 0;

                var n = newq.First();
                memberID = n.MemberId;
                if (n.IsDefault == true) isDef = true;

                n.Removed = true;
                n.IsDefault = false;
                _context.SaveChanges();

                //update tbmeber profile with new profile picture
                if (isDef)
                {
                    var mbr = (from m in _context.TbMemberProfile where m.MemberId == memberID select m).First();
                    mbr.PicturePath = defaultFileName;
                    _context.SaveChanges();
                }
            }
        }
    }

    public interface ISettingRepository
    {
        List<MemberNameInfoModel> GetMemberNameInfo(int memberID);
        void SaveMemberNameInfo(int memberID, string fName, string mName, string lName);
        void SaveMemberEmailInfo(int memberID, string email);
        void SavePasswordInfo(string memberID, string pwd);
        void SaveSecurityQuestionInfo(int memberID, int questionID, string answer);
        void DeactivateAccount(int memberID, int reason, string explanation, bool? futureEmail);
        List<MemberNetworksModel> GetMemberNetworks(int memberID);
        bool ValidateNetworkID(int networkID);
        bool ValidateMemberId(int memberID);
        void JoinNetwork(int memberID, int networkID);
        void JoinNetworkByEmail(string email, int networkID);
        bool isNetworkMember(int memberID, int networkID);
        void CreateNetworkRequest(int memberID, string networkName, string type, string purpose);
        List<NotificationsSettingModel> GetMemberNotifications(int memberId);
        public void SaveNotificationSettings(
              int MemberID,
              bool LG_SendMsg,
              bool LG_AddAsFriend,
              bool LG_ConfirmFriendShipRequest,
              bool GP_InviteYouToJoin,
              bool GP_MakesYouAGPAdmin,
              bool GP_RepliesToYourDiscBooardPost,
              bool GP_ChangesTheNameOfGroupYouBelong,
              bool EV_InviteToEvent,
              bool EV_DateChanged,
              bool HE_RepliesToYourHelpQuest
        );
        List<PrivacySearchSettingsModel> GetProfileSettings(int memberID);
        void SaveProfileSettings(int memberID, PrivacySearchSettingsModel body);
        List<PrivacySearchSettingsModel> GetPrivacySearchSettings(int memberID);
        void SavePrivacySearchSettings(
          int memberID,
          int visibility,
          bool viewProfilePicture,
          bool viewFriendsList,
          bool viewLinkToRequestAddingYouAsFriend,
          bool viewLinkToSendYouMsg);
        List<TbMemberProfile> GetAllMembers(string searchText);
        bool IsEmailExist(int memberID, string email);
        List<TbMemberProfilePictures> GetMemberProfilePictures(int memberID);
        string GetMemberDefaultPicture(int memberID);
        void RemoveProfilePicture(int memberID, string noPhotoFilename);
        void SetPictureAsDefault(int memberID, int profileID, string fileName);
        void RemovePicture(int profileID, string defaultFileName);
    }
}
