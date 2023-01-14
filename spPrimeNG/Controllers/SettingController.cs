using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sportprofiles.Models.Members;
using sportprofiles.Models.Settings;
using sportprofiles.Repository;
using Swashbuckle.AspNetCore.Annotations;

namespace sportprofiles.Controllers
{
    [Route("services/[controller]")]
    [SwaggerTag("This is a list of interfaces to manage application settings and user preferences.")]
    public class SettingController : Controller
    {
        private readonly ISettingRepository _setRepo;

        public SettingController(ISettingRepository setRepo)
        {
            _setRepo = setRepo;
        }

        /// <summary>
        /// Gets the member name information.
        /// </summary>
        /// <returns>The member name info.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberNameInfo/{memberID}")]
        public List<MemberNameInfoModel> GetMemberNameInfo([FromRoute] int memberID)
        {
            return _setRepo.GetMemberNameInfo(memberID);
        }

        /// <summary>
        /// Saves the member name info.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="fName">First name.</param>
        /// <param name="mName">Middle name.</param>
        /// <param name="lName">Last name.</param>
        [HttpPut]
        [Authorize]
        [Route("SaveMemberNameInfo/{memberID}")]
        public void SaveMemberNameInfo([FromRoute] int memberID, [FromQuery] string fName, [FromQuery] string mName, [FromQuery] string lName)
        {
            _setRepo.SaveMemberNameInfo(memberID, fName, mName, lName);
        }

        /// <summary>
        /// Saves the member email information.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="email">Email.</param>
        [HttpPut]
        [Authorize]
        [Route("SaveMemberEmailInfo/{memberID}")]
        public void SaveMemberEmailInfo([FromRoute] int memberID, [FromQuery] string email)
        {
            _setRepo.SaveMemberEmailInfo(memberID, email);
        }

        /// <summary>
        /// Saves the password information.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="pwd">Password.</param>
        [HttpPut]
        [Authorize]
        [Route("SavePasswordInfo")]
        public void SavePasswordInfo([FromBody] PasswordData body)
        {
            string mykey = "r0b1nr0y";
            string pwd = EncryptStrings.Encrypt(body.Pwd, mykey);
            _setRepo.SavePasswordInfo( body.MemberID, pwd);
        }

        /// <summary>
        /// Saves the security question info.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="questionID">Question identifier.</param>
        /// <param name="answer">Answer.</param>
        [HttpPut]
        [Authorize]
        [Route("SaveSecurityQuestionInfo/{memberID}")]
        public void SaveSecurityQuestionInfo([FromRoute] int memberID, [FromQuery] int questionID, [FromQuery] string answer)
        {
            _setRepo.SaveSecurityQuestionInfo(memberID, questionID, answer);
        }

        /// <summary>
        /// Deactivates the account.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="reason">Reason.</param>
        /// <param name="explanation">Explanation.</param>
        /// <param name="futureEmail">Future email.</param>
        [HttpPut]
        [Authorize]
        [Route("DeactivateAccount/{memberID}")]
        public void DeactivateAccount([FromRoute] int memberID, [FromQuery] int reason, [FromQuery] string explanation, [FromQuery] bool? futureEmail)
        {
            _setRepo.DeactivateAccount(memberID, reason, explanation, futureEmail);
        }

        /// <summary>
        /// Gets the member notifications.
        /// </summary>
        /// <returns>The member notifications.</returns>
        /// <param name="memberId">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberNotifications/{memberID}")]
        public List<NotificationsSettingModel> GetMemberNotifications([FromRoute] int memberId)
        {
            return _setRepo.GetMemberNotifications(memberId);
        }

        /// <summary>
        /// Saves the notification settings.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        [HttpPut]
        [Authorize]
        [Route("SaveNotificationSettings/{memberID}")]
        public void SaveNotificationSettings([FromRoute] int memberID, [FromBody] NotificationsSettingModel body)
        {

            _setRepo.SaveNotificationSettings(memberID, body.LG_SendMsg, body.LG_AddAsFriend, body.LG_ConfirmFriendShipRequest,
                                           body.GP_InviteYouToJoin, body.GP_MakesYouAGPAdmin, body.GP_RepliesToYourDiscBooardPost,
                                           body.GP_ChangesTheNameOfGroupYouBelong, body.EV_InviteToEvent, body.EV_DateChanged,
                                           body.HE_RepliesToYourHelpQuest);

        }

        /// <summary>
        /// Gets the profile settings.
        /// </summary>
        /// <returns>The profile settings.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetProfileSettings/{memberID}")]
        public List<PrivacySearchSettingsModel> GetProfileSettings([FromRoute] int memberID)
        {
            return _setRepo.GetProfileSettings(memberID);
        }

        /// <summary>
        /// Saves the profile settings.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        [HttpPut]
        [Authorize]
        [Route("SaveProfileSettings/{memberID}")]
        public void SaveProfileSettings([FromRoute] int memberID, [FromBody] PrivacySearchSettingsModel body)
        {
            _setRepo.SaveProfileSettings(memberID, body);
        }

        /// <summary>
        /// Gets the privacy search settings.
        /// </summary>
        /// <returns>The privacy search settings.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetPrivacySearchSettings/{memberID}")]
        public List<PrivacySearchSettingsModel> GetPrivacySearchSettings([FromRoute]int memberID)
        {
            return _setRepo.GetPrivacySearchSettings(memberID);
        }

        /// <summary>
        /// Saves the privacy search settings.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="visibility">Visibility.</param>
        /// <param name="viewProfilePicture">If set to <c>true</c> view profile picture.</param>
        /// <param name="viewFriendsList">If set to <c>true</c> view friends list.</param>
        /// <param name="viewLinkToRequestAddingYouAsFriend">If set to <c>true</c> view link to request adding you as friend.</param>
        /// <param name="viewLinkToSendYouMsg">If set to <c>true</c> view link to send you message.</param>
        [HttpPut]
        [Authorize]
        [Route("SavePrivacySearchSettings/{memberID}")]
        public void SavePrivacySearchSettings([FromHeader] string authorization,
              [FromRoute] int memberID,
              [FromQuery] int visibility,
              [FromQuery] bool viewProfilePicture,
              [FromQuery] bool viewFriendsList,
              [FromQuery] bool viewLinkToRequestAddingYouAsFriend,
              [FromQuery] bool viewLinkToSendYouMsg)
        {
            _setRepo.SavePrivacySearchSettings(memberID, visibility, viewProfilePicture, viewFriendsList, viewLinkToRequestAddingYouAsFriend,
                                             viewLinkToSendYouMsg);
        }
    }

}
