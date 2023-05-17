using Microsoft.AspNetCore.Mvc;
using sportprofiles.Models.Members;
using sportprofiles.DBContextModels;
using System.Net.Http.Headers;
using sportprofiles.Repository;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace sportprofiles.Controllers
{
    [Route("services/[controller]")]
    [SwaggerTag("Contains member management API functionalities.")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _mbrRepo;
        private readonly ILoggingRepository _logRepo;
        private readonly ICommonRepository  _commonRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public MemberController(IWebHostEnvironment hostingEnvironment, IMemberRepository memberRepository,
                                   ILoggingRepository loggingRepository, ICommonRepository commonRepository,
                                   IConfiguration configuration )
        {
            _hostingEnvironment = hostingEnvironment;
            _mbrRepo =  memberRepository;
            _logRepo = loggingRepository ;
            _commonRepo = commonRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Validates the new registered user.
        /// </summary>
        /// <returns>The new registered user.</returns>
        /// <param name="email">Email.</param>
        /// <param name="code">Code.</param>
        [HttpGet]
        [Authorize]
        [Route("ValidateNewRegisteredUser")]
        public List<ValidateNewRegisteredUserModel> ValidateNewRegisteredUser([FromQuery] string email, [FromQuery] string code)
        {
            return _logRepo.ValidateNewRegisteredUser(email, Convert.ToInt32(code));
        }

        /// <summary>
        /// Saves the posts.
        /// </summary>
        /// <returns>The posts.</returns>
        /// <param name="body">Body.</param>
        [HttpPost]
        [Authorize]
        [Route("SavePosts/{memberID}/{postID}")]
        public string SavePosts([FromRoute] int memberID, [FromRoute] int postID, [FromQuery] string postMsg)
        {
            if (postID == 0)
               _mbrRepo.CreateMemberPost(memberID, postMsg);
            else
                _mbrRepo.CreatePostComment(memberID,postID,postMsg);
            return "success";
        }

        /// <summary>
        /// LGRs the recent post responses.
        /// </summary>
        /// <returns>The recent post responses.</returns>
        /// <param name="postID">Post identifier.</param>
        [Authorize]
        [HttpGet]
        [Route("GetRecentPostResponses/{postID}")]
        public List<RecentPostChildModel> GetRecentPostResponses([FromRoute] int postID)
        {
            return _mbrRepo.GetMemberPostResponses(postID);
        }

        /// <summary>
        /// Get the recent posts.
        /// </summary>
        /// <returns>The recent posts.</returns>
        /// <param name="memberID">Member identifier.</param>
        [Authorize]
        [HttpGet]
        [Route("getRecentPosts/{memberID}")]
        public List<MemberPostsModel> getRecentPosts([FromRoute] int memberID)
        {
            return _mbrRepo.GetMemberPosts(memberID);
        }

        /// <summary>
        /// Get the recent news.
        /// </summary>
        /// <returns>The recent news.</returns>
        [Authorize]
        [HttpGet]
        [Route("getRecentNews")]
        public List<TbRecentNews> getRecentNews()
        {
            return _mbrRepo.LGRecentNews();
        }

        /// <summary>
        /// Gets the member general info v2.
        /// </summary>
        /// <returns>The member general info v2.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberGeneralInfoV2/{memberID}")]
        public MemberProfileGenInfoModel GetMemberGeneralInfoV2([FromRoute]int memberID)
        {
            return _mbrRepo.GetMemberGeneralInfoV2(memberID);
        }

        /// <summary>
        /// Gets list of member dates.
        /// </summary>
        /// <returns>The member dates.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberDates/{memberID}")]
        public List<MemberDatesModel> GetMemberDates([FromRoute]int memberID)
        {
            return _mbrRepo.GetMemberDates(memberID);
        }

        /// <summary>
        /// Gets member interest description.
        /// </summary>
        /// <returns>The interest description.</returns>
        /// <param name="interestID">Interest identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetInterestDescription/{interestID}")]
        public string GetInterestDescription([FromRoute] int interestID)
        {
            return _mbrRepo.GetInterestDescription(interestID);
        }

        /// <summary>
        /// Gets member personal information.
        /// </summary>
        /// <returns>The member personal info.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberPersonalInfo/{memberID}")]
        public List<TbMemberProfilePersonalInfo> GetMemberPersonalInfo([FromRoute] int memberID)
        {
            return _mbrRepo.GetMemberPersonalInfo(memberID);
        }

        /// <summary>
        /// Gets member contact information.
        /// </summary>
        /// <returns>The member contact info.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberContactInfo/{memberID}")]
        public TbMemberProfileContactInfo GetMemberContactInfo([FromRoute] int memberID)
        {
            List<TbMemberProfileContactInfo> lst = _mbrRepo.GetMemberConnectionInfo(memberID);
            if (lst.Count == 0) { return null; } else {return  lst[0]; };
        }

        /// <summary>
        /// Gets member education information.
        /// </summary>
        /// <returns>The member education info.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberEducationInfo/{memberID}")]
        public List<MemberProfileEducationModel> GetMemberEducationInfo([FromRoute] int memberID)
        {
            return _mbrRepo.GetMemberEducationInfo(memberID);
        }
        /// <summary>
        /// Gets the member employment info.
        /// </summary>
        /// <returns>The member employment info.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberEmploymentInfo/{memberID}")]
        public List<MemberProfileEmploymentModel> GetMemberEmploymentInfo([FromRoute] int memberID)
        {
            List<MemberProfileEmploymentModel> lst = _mbrRepo.GetMemberEmploymentInfo(memberID);
            return lst;
        }
       
        /// <summary>
        /// Saves or update the member general info.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">input data of the MemberProfileGenInfo Model.</param>
        [HttpPost]
        [Authorize]
        [Route("SaveMemberGeneralInfo/{memberID}")]
        public void SaveMemberGeneralInfo([FromRoute] int memberID, [FromBody] MemberProfileGenInfoModel body)
        {
            _mbrRepo.SaveMemberGeneralInfo(memberID, body);
        }

        /// <summary>
        /// Saves or update the member personal info.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="activities">Activities.</param>
        /// <param name="interests">Interests.</param>
        /// <param name="specialSkills">Special skills.</param>
        /// <param name="aboutMe">About me.</param>
        [HttpPost]
        [Authorize]
        [Route("SaveMemberPersonalInfo")]
        public void SaveMemberPersonalInfo(
                     [FromQuery] int memberID,
                     [FromQuery] string activities,
                     [FromQuery] string interests,
                     [FromQuery] string specialSkills,
                     [FromQuery] string aboutMe)
        {
            _mbrRepo.SaveMemberPersonalInfo(memberID, activities, interests, specialSkills, aboutMe);
        }

        /// <summary>
        /// Saves or update the member contact information.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">model input data.</param>
        [HttpPost]
        [Authorize]
        [Route("SaveMemberContactInfo/{memberID}")]
        public void SaveMemberContactInfo([FromRoute] int memberID, [FromBody] MemberProfileContactInfoModel body)
        {
            _mbrRepo.SaveMemberContactInfo(memberID, body);
        }

        /// <summary>
        /// Saves or update the member contact information.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">model input data.</param>
        [HttpPost]
        [Authorize]
        [Route("SaveMemberContactInfoV2/{memberID}")]
        public void SaveMemberContactInfoV2([FromRoute] int memberID,
            [FromQuery] string Email,
            [FromQuery] string OtherEmail,
            [FromQuery] string Facebook,
            [FromQuery] string Instagram,
            [FromQuery] string Twitter,
            [FromQuery] string Website,
            [FromQuery] string HomePhone,
            [FromQuery] string CellPhone,
            [FromQuery] string Address,
            [FromQuery] string City,
            [FromQuery] string Neighborhood,
            [FromQuery] string State,
            [FromQuery] string Zip,
            [FromQuery] string ShowAddress,
            [FromQuery] string ShowEmailToMembers,
            [FromQuery] string ShowCellPhone,
            [FromQuery] string ShowHomePhone
            )
        {
           
            _mbrRepo.SaveMemberContactInfoV2(memberID, Email, OtherEmail, Facebook, Instagram,
                                              Twitter, Website, HomePhone, CellPhone, 
                                              Address, City, Neighborhood, State, Zip, ShowAddress,ShowEmailToMembers,
                                              ShowCellPhone, ShowHomePhone);
        }


        /// <summary>
        /// Sends the friend request.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="email">Email.</param>
        [HttpPost]
        [Authorize]
        [Route("SendFriendRequest/{memberID}")]
        public void SendFriendRequest([FromRoute] int memberID, [FromQuery] string email)
        {
            _mbrRepo.SendFriendRequest(memberID, email);
        }

        /// <summary>
        /// checks if contact request was sent to member id.
        /// </summary>
        /// <returns><c>true</c>, if contact request sent was ised, <c>false</c> otherwise.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="toMemberID">To member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("IsContactRequestSent/{memberID}/{toMemberID}")]
        public bool IsContactRequestSent([FromRoute] int memberID, [FromRoute] int toMemberID)
        {
           return _mbrRepo.isContactRequestSent(memberID, toMemberID);
        }

        /// <summary>
        /// checks if member exists for an email".
        /// </summary>
        /// <returns><c>true</c>, if member was ised, <c>false</c> otherwise.</returns>
        /// <param name="email">Email.</param>
        [HttpGet]
        [Authorize]
        [Route("IsMember")]
        public bool IsMember([FromQuery] string email)
        {
           return _mbrRepo.IsMember(email);
        }

        /// <summary>
        /// checks if member is a contact by contact's email.
        /// </summary>
        /// <returns><c>true</c>, if friend by email was used, <c>false</c> otherwise.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="email">Email.</param>
        [HttpGet]
        [Authorize]
        [Route("IsFriendByEmail/{memberID}")]
        public bool IsFriendByEmail([FromRoute] int memberID, [FromQuery] string email)
        {
           return _mbrRepo.IsFriend(memberID, email);
        }

        /// <summary>
        /// checks if member is a contact by contact id
        /// </summary>
        /// <returns><c>true</c>, if friend by contact identifier was ised, <c>false</c> otherwise.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("IsFriendByContactID/{memberID}/{contactID}")]
        public bool IsFriendByContactID([FromRoute] int memberID, [FromRoute] int contactID)
        {
            bool b = _mbrRepo.IsFriend(memberID, contactID);
            return b;
        }

        /// <summary>
        /// Saves the member basic profile info.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="highSchool">High school.</param>
        /// <param name="highSchoolYear">High school year.</param>
        /// <param name="college">College.</param>
        /// <param name="collegeYear">College year.</param>
        /// <param name="company">Company.</param>
        [HttpPost]
        [Authorize]
        [Route("SaveBasicProfileInfo")]
        public void SaveBasicProfileInfo([FromQuery] string email,
                                    [FromQuery] string highSchool,
                                    [FromQuery] string highSchoolYear,
                                    [FromQuery] string college,
                                    [FromQuery] string collegeYear,
                                    [FromQuery] string company)
        {
            _mbrRepo.SaveBasicProfileInfo(email, highSchool, highSchoolYear, college, collegeYear, company);
        }

        /// <summary>
        /// Gets the member info by email.
        /// </summary>
        /// <returns>The member info by email.</returns>
        /// <param name="email">Email.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberInfoByEmail")]
        public List<MemberInfoByEmailModel> GetMemberInfoByEmail([FromQuery] string email)
        {
            List<MemberInfoByEmailModel> lst = _mbrRepo.GetMemberInfoByEmail(email);
            return lst;
        }

        /// <summary>
        /// Gets the recent activities.
        /// </summary>
        /// <returns>The recent activities.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetRecentActivities/{memberID}")]
        public List<RecentActivitiesModel> GetRecentActivities([FromRoute] int memberID)
        {
            List<RecentActivitiesModel> lst = _mbrRepo.GetRecentActivities(memberID);
            return lst;
        }

        /// <summary>
        /// Gets the member posts.
        /// </summary>
        /// <returns>The member posts.</returns>
        /// <param name="memberID">Member identifier.</param>
        [Authorize]
        [HttpGet]
        [Route("GetMemberPosts/{memberID}")]
        public List<MemberPostsModel> GetMemberPosts([FromRoute] int memberID)
        {
            List<MemberPostsModel> lst = _mbrRepo.GetMemberPosts(memberID);
            return lst;
        }

        /// <summary>
        /// Gets the member post responses.
        /// </summary>
        /// <returns>The member post responses.</returns>\
        /// <param name="postID">Post identifier.</param>
        [Authorize]
        [HttpGet]
        [Route("GetMemberPostResponses/{postID}")]
        public List<RecentPostChildModel> GetMemberPostResponses([FromRoute] int postID)
        {
            List<RecentPostChildModel> lst = _mbrRepo.GetMemberPostResponses(postID);
            return lst;
        }


        /// <summary>
        /// Creates the member post.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="postMsg">Post message.</param>
        [HttpPost]
        [Authorize]
        [Route("CreateMemberPost/{memberID}")]
        public void CreateMemberPost([FromRoute] int memberID, [FromQuery] string postMsg)
        {
            _mbrRepo.CreateMemberPost(memberID, postMsg);
        }


        /// <summary>
        /// Creates the post comment.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="postID">Post identifier.</param>
        /// <param name="postMsg">Post message.</param>
        [HttpGet]
        [Authorize]
        [Route("CreatePostComment/{memberID}/{postID}")]
        public void CreatePostComment([FromRoute] int memberID, [FromRoute] int postID, [FromQuery] string postMsg)
        {
            _mbrRepo.CreatePostComment(memberID, postID, postMsg);
        }


        /// <summary>
        /// Gets the member work experience.
        /// </summary>
        /// <returns>The member work experience.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberWorkExperience/{memberID}")]
        public List<TbMemberProfileEmploymentInfo> GetMemberWorkExperience([FromRoute] int memberID)
        {
            return _mbrRepo.GetMemberWorkExperience(memberID);
        }

        /// <summary>
        /// Gets the member interests.
        /// </summary>
        /// <returns>The member interests.</returns>
        [HttpGet]
        [Authorize]
        [Route("GetMemberInterests")]
        public List<TbInterests> GetMemberInterests()
        {
            return (_mbrRepo.GetMemberInterests());
        }

        /// <summary>
        /// Adds the member school to db.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        [HttpPost]
        [Authorize]
        [Route("AddMemberSchool/{memberID}")]
        public void AddMemberSchool([FromRoute] int memberID, [FromBody] MemberProfileEducationModel body)
        {
            _mbrRepo.AddMemberSchool(memberID, long.Parse(body.SchoolID), int.Parse(body.SchoolType), body.SchoolName, body.YearClass, body.Major, int.Parse(body.Degree), body.Societies, body.SportLevelType);
        }

        /// <summary>
        /// Updates the member school.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        [HttpPut]
        [Authorize]
        [Route("UpdateMemberSchool/{memberID}")]
        public void UpdateMemberSchool([FromRoute] int memberID, [FromBody] MemberProfileEducationModel body)
        {
            _mbrRepo.UpdateMemberSchool(memberID, int.Parse(body.SchoolID), int.Parse(body.SchoolType), body.YearClass, body.Major, int.Parse(body.Degree), body.Societies, body.SportLevelType);
        }

        /// <summary>
        /// Removes the member school.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="instID">Inst identifier.</param>
        /// <param name="instType">Inst type.</param>
        [HttpDelete]
        [Authorize]
        [Route("RemoveMemberSchool")]
        public void RemoveMemberSchool([FromQuery] int memberID, [FromQuery] int instID, [FromQuery] int instType)
        {
            _mbrRepo.RemoveMemberSchool(memberID, instID, instType);
        }

        /// <summary>
        /// Add member work experience.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        [HttpPost]
        [Authorize]
        [Route("AddWorkExperience/{memberID}")]
        public void AddWorkExperience([FromRoute] int memberID, [FromBody] MemberProfileEmploymentModel body)
        {
            _mbrRepo.AddWorkExperience(memberID, int.Parse(body.CompanyID), body.CompanyName, body.Title, body.JobDesc, body.City, body.State, body.StartMonth, body.StartYear, body.EndMonth, body.EndYear);
        }

        /// <summary>
        /// Updates the member work experience.
        /// </summary>
        /// <param name="memberId">Member identifier.</param>
        /// <param name="body">Body.</param>
        [HttpPut]
        [Authorize]
        [Route("UpdateMemberWorkExperience/{memberID}")]
        public void UpdateMemberWorkExperience([FromRoute] int memberId, [FromBody] MemberProfileEmploymentModel body)
        {
            _mbrRepo.UpdateMemberWorkExperience(int.Parse(body.EmpInfoID), memberId, int.Parse(body.CompanyID), body.Title, body.JobDesc, body.City, body.State, body.StartMonth, body.StartYear, body.EndMonth, body.EndYear);
        }

        /// <summary>
        /// Removes the member work experience.
        /// </summary>
        /// <param name="empInfoID">Emp info identifier.</param>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="compID">Comp identifier.</param>
        [HttpDelete]
        [Authorize]
        [Route("RemoveMemberWorkExperience")]
        public void RemoveMemberWorkExperience([FromQuery] int empInfoID, [FromQuery] int memberID, [FromQuery] int compID)
        {
            _mbrRepo.RemoveMemberWorkExperience(empInfoID, memberID, compID);
        }

        /// <summary>
        /// checks to see if the member is active.
        /// </summary>
        /// <returns><c>true</c>, if member active was used, <c>false</c> otherwise.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("IsMemberActive/{memberID}")]
        public bool IsMemberActive([FromRoute] int memberID)
        {
            return _mbrRepo.IsMemberActive(memberID);
        }

        /// <summary>
        /// Gets the member email.
        /// </summary>
        /// <returns>The member email.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberEmail/{memberID}")]
        public List<TbMembers> GetMemberEmail([FromRoute] int memberID)
        {
            return (_mbrRepo.GetMemberEmail(memberID));
        }

        /// <summary>
        /// Adds the member acitivity.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="activity">Activity.</param>
        [HttpPost]
        [Authorize]
        [Route("AddMemberActivity/{memberID}")]
        public void AddMemberActivity([FromRoute] int memberID, [FromQuery] string activity)
        {
            _mbrRepo.AddMemberActivity(memberID, activity);
        }


        /// <summary>
        /// Gets the registered members.
        /// </summary>
        /// <returns>The registered members.</returns>
        /// <param name="status">Status.</param>
        [HttpGet]
        [Authorize]
        [Route("GetRegisteredMembers/{status}")]
        public List<RegisteredMembersModel> GetRegisteredMembers([FromRoute] int status)
        {
            return _mbrRepo.GetRegisteredMembers(status);
        }

        /// <summary>
        /// Sends the advertisement info.
        /// </summary>
        /// <returns>The advertisement info.</returns>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="company">Company.</param>
        /// <param name="email">Email.</param>
        /// <param name="phone">Phone.</param>
        /// <param name="country">Country.</param>
        /// <param name="title">Title.</param>
        [HttpPost]
        [Route("SendAdvertisementInfo")]
        public string SendAdvertisementInfo([FromQuery] string firstName,
                                         [FromQuery] string lastName,
                                         [FromQuery] string company,
                                         [FromQuery] string email,
                                         [FromQuery] string phone,
                                         [FromQuery] string country,
                                         [FromQuery] string title)
        {
            string staffContactEmail = _configuration.GetValue<string>("AppStrings:StaffContactEmail");
            string noReplyEmail = _configuration.GetValue<string>("AppStrings:NoReplyEmail");

            string issue = company + " wants to do business with you as far as advertisement.";
            string txtDesc = firstName + " " + lastName + " from company " + company + " wants has sent you the following information with business interests: <br/>";
            txtDesc += " - Phone: " + phone + "<br/> - Email: " + email + "<br/> - Country: " + country + "<br/> - Title: " + title;
            string strHTML = HTMLContactUsBodyText("System Administrator", email, "Advertisement Interest", issue, txtDesc);

            _commonRepo.SendGenEmailToUser(firstName, noReplyEmail, staffContactEmail, "", "", "Advertisement Interest" + " (" + issue + ")", strHTML);

            return "success";
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <returns>The password.</returns>
        /// <param name="email">Email.</param>
        [HttpGet]
        [Route("ResetPassword")]
        public string ResetPassword([FromQuery] string email)
        {
            List<CodeAndNameForgotPwdModel> lst = _logRepo.GetCodeAndNameForgotPwd(email);
            if (lst.Count != 0)
            {
                CodeAndNameForgotPwdModel ds = lst[0];
                string code = ds.CodeID.ToString();
                string firstName = ds.FirstName.ToString();
                SendEmail(email, code, firstName);
                return "success";
            }
            else
            {
                return "fail";
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetYoutubeChannel/{memberID}")]
        public  string GetYoutubeChannel([FromRoute] int memberID)
        {
            return _mbrRepo.GetYoutubeChannel(memberID);
        }

        [HttpPut]
        [Authorize]
        [Route("SetYoutubeChannel")]
        public void SetYoutubeChannel([FromBody] YoutubeChannelModel body)
        {
            _mbrRepo.SetYoutubeChannel(body.MemberID, body.ChannelID);
        }


        [HttpGet]
        [Authorize]
        [Route("GetInstagramURL/{memberID}")]
        public string GetInstagramURL([FromRoute] int memberID)
        {
            return _mbrRepo.GetInstagramURL(memberID);
        }

        [HttpPut]
        [Authorize]
        [Route("SetInstagramURL")]
        public void SetInstagramURL([FromBody] InstagramURLModel body)
        {
            _mbrRepo.SetInstagramURL(body.MemberID, body.InstagramURL);
        }

        private void SendEmail(string email, string code, string firstName)
        {
            string fromEmail = _configuration.GetValue<string>("AppStrings:AppFromEmail"); 
            string appName = _configuration.GetValue<string>("AppStrings:AppName");
            string subject = "Password Reset Confirmation";
            string webSiteLink = _configuration.GetValue<string>("AppStrings:AppName");
            string body = HTMLBodyText(email, firstName, code, appName,webSiteLink);
            _commonRepo.SendMail(firstName, fromEmail, email, subject, body, true);
        }

        private static string HTMLBodyText(string email, string name, string code, string appName, string webSiteLink )
        {
            string str = "";

            str = str + "<table width='100%' style='text-align: center;'>";
            str = str + "<tr>";
            str = str + "<td style='font-weight: bold; font-size: 12px; height: 25px; text-align: left; background-color: #4a6792;";
            str = str + "vertical-align: middle; color: White;'>";
            str = str + "&nbsp;" + appName;
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "<tr><td>&nbsp;</td></tr>";
            str = str + "<tr>";
            str = str + "<td style='font-size: 12px; text-align: left; width: 100%; font-family: Trebuchet MS,Trebuchet,Verdana,Helvetica,Arial,sans-seri'>";
            str = str + "<p>Hi " + name + ",<p/>";
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "<tr>";
            str = str + "<td style='font-size: 12px; text-align: left; width: 100%; font-family: Trebuchet MS,Trebuchet,Verdana,Helvetica,Arial,sans-seri'>";
            str = str + "<p>You recently requested a new password. <br/>";
            str = str + "<p />";
            str = str + "<p>Here is your reset code, which you can enter on the password reset page:<br/><b>";
            str = str + code + "</b><p />";
            str = str + "<p>You can also reset your password by following the link below:<br/>";
            str = str + "<a href ='" + webSiteLink + "reset?email=" + email + "&c=" + code + "'>" + webSiteLink + "reset?email=" + email + "&c=" + code + "</a><p />";
            str = str + "<p />";
            str = str + "<p>If you did not request to reset your password, please disregard this message.<br>";
            str = str + "<p/>";
            str = str + "Thanks.<br />";
            str = str + "The " + appName  + " staff<br />";
            str = str + "</p>";
            str = str + "<p></p><p>";
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "</table>";

            return str;
        }

        /// <summary>
        /// Is the reset code expired, yes or no.
        /// </summary>
        /// <returns>The reset code expired.</returns>
        /// <param name="code">Code.</param>
        [HttpGet]
        [Route("IsResetCodeExpired")]
        public string IsResetCodeExpired([FromQuery] string code)
        {
            string strCode = code.Trim();
            List<TbForgotPwdCodes> lst = _logRepo.CheckCodeExpired(Convert.ToInt32(strCode));
            if (lst.Count == 0)
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }


        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <returns>The password.</returns>
        /// <param name="pwd">Pwd.</param>
        /// <param name="email">Email.</param>
        /// <param name="code">Code.</param>
        [HttpGet]
        [Route("ChangePassword")]
        public string ChangePassword([FromQuery] string pwd,
                                       [FromQuery] string email,
                                       [FromQuery] string code)
        {
            string mykey = "r0b1nr0y";
            pwd = EncryptStrings.Encrypt(pwd, mykey);
            if (code != "")
            {
                _logRepo.SetCodeToExpire(Convert.ToInt32(code));
            }
            _logRepo.ChangePassword(email, pwd);

            var memberList = _logRepo.ValidateUser(email, pwd);
            if (memberList.Count != 0)
            {
                return memberList[0].MemberID.ToString() + ":" + memberList[0].Email.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Uploads the profile photo.
        /// </summary>
        /// <returns>The profile photo.</returns>
        /// <param name="memberId">Member identifier.</param>
        [HttpPost]
        [Authorize]
        [Route("UploadProfilePhoto/{memberID}")]
        public void UploadProfilePhoto([FromRoute] string memberId)
        {
            var file = Request.Form.Files[0];
            var ext = file.FileName.Split(".")[1];

            string folderName = "images/members/";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            if (file.Length > 0)
            {
                string fileName = memberId + "." + ext;
                string fullPath = Path.Combine(newPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                //update table with file name
                _mbrRepo.UpdateProfilePicture(Convert.ToInt32(memberId), fileName); 
            }
        }

        /// <summary>
        /// set member status
        /// </summary>
        /// <param name="memberId">Member identifier.</param>
        /// <param name="fileName">File name.</param>
        [HttpGet]
        //[Authorize]
        [Route("SetMemberStatus")]
        public void SetMemberStatus([FromQuery] int memberId,[FromQuery] int status)
        {
                _logRepo.SetMemberStatus(memberId, status);
        }

        /// <summary>
        /// Get utube video playlist
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetVideoPlayList/{memberId}")]
        public List<YoutubePlayListModel> GetVideoPlayList([FromRoute] string memberId) {
            return  _mbrRepo.GetVideoPlayList(memberId);
        }

        /// <summary>
        /// Get videos list for a player list.
        /// </summary>
        /// <param name="playerListID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetVideosList/{playerListID}")]
        public List<YoutubeVideosListModel> GetVideosList([FromRoute] string playerListID) {
            return  _mbrRepo.GetVideosList(playerListID);
        }

        private string HTMLContactUsBodyText(string name, string strUserEmail, string subject, string issueType, string desc)
        {
            string appName = _configuration.GetValue<string>("AppStrings:AppName");
            string str = "";
            str = str + "<table width='100%' style='text-align: center;'>";
            str = str + "<tr>";
            str = str + "<td style='font-weight: bold; font-size: 12px; height: 25px; text-align: left; background-color: #4a6792;";
            str = str + "vertical-align: middle; color: White;'>";
            str = str + "&nbsp;" + appName;
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "<tr><td>&nbsp;</td></tr>";
            str = str + "<tr>";
            str = str + "<td style='font-size: 12px; text-align: left; width: 100%; font-family: Trebuchet MS,Trebuchet,Verdana,Helvetica,Arial,sans-seri'>";
            str = str + "<p>Hi " + name + ",<p/>";
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "<tr>";
            str = str + "<td style='font-size: 12px; text-align: left; width: 100%; font-family: Trebuchet MS,Trebuchet,Verdana,Helvetica,Arial,sans-seri'>";
            str = str + "<p>On behalf of " + appName + " user " + strUserEmail + ", the following message from ContactUs page was sent:" + "<br/>";
            str = str + "<p />";
            str = str + "<p><b>Email Address:</b> " + strUserEmail + " <br/>";
            str = str + "<b>Subject:</b> " + subject + " <br/>";
            str = str + "<b>Issue type:</b> " + issueType + " <br/>";
            str = str + "<b>Description:</b> " + desc + " <br/>";
            str = str + "<p/>";
            str = str + "Thanks.<br />";
            str = str + "The " + appName + " Staff<br />";
            str = str + "</p>";
            str = str + "<p></p><p>";
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "</table>";
            return str;
        }
    }
}
