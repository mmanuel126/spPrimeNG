using Microsoft.EntityFrameworkCore;
using sportprofiles.Models.Connections;
using Microsoft.Data.SqlClient;
using System.Data;
using sportprofiles.DBContextModels;
using System.Dynamic;

namespace sportprofiles.Repository
{
    /// <summary>
    /// Describes the functionalities for accessing data for contacts
    /// </summary>
    public class ConnectionRepository : IConnectionRepository
    {
        readonly dbContexts _context;
        readonly IMemberRepository _mbrRepo;
        readonly IMessageRepository _msgRepo;

        public ConnectionRepository(dbContexts context, IMemberRepository memberRepository, IMessageRepository messageRepository)
        {
            _context = context;
            _mbrRepo = memberRepository;
            _msgRepo = messageRepository;
        }

        /// <summary>
        /// Get list of member contacts.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="show"></param>
        /// <returns></returns>
        public List<MemberConnectionModel> GetMemberConnections(int memberID, string show)
        {
            
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spGetMemberContacts";

                // Add the memberID parameter and set its properties.
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@MemberID";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = memberID;
                // Add the parameter to the Parameters collection. 
                command.Parameters.Add(parameter);

                // Add the memberID parameter and set its properties.
                parameter = new SqlParameter();
                parameter.ParameterName = "@ShowType";
                parameter.SqlDbType = SqlDbType.VarChar ;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = show;
                // Add the parameter to the Parameters collection. 
                command.Parameters.Add(parameter);

                List<MemberConnectionModel> cList = new List<MemberConnectionModel>();

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            MemberConnectionModel pc = new MemberConnectionModel();
                            pc.ConnectionID = reader.GetValue(reader.GetOrdinal("contactid")).ToString();
                            pc.Email = reader.GetValue(reader.GetOrdinal("email")).ToString();
                            pc.FirstName  = reader.GetValue(reader.GetOrdinal("firstname")).ToString();
                            pc.FriendName  = reader.GetValue(reader.GetOrdinal("friendname")).ToString();
                            pc.LabelText  = reader.GetValue(reader.GetOrdinal("labeltext")).ToString();
                            pc.Location  = reader.GetValue(reader.GetOrdinal("location")).ToString();
                            pc.NameAndID  = reader.GetValue(reader.GetOrdinal("nameandid")).ToString();
                            pc.Params   = reader.GetValue(reader.GetOrdinal("params")).ToString();;
                            pc.ParamsAV  = reader.GetValue(reader.GetOrdinal("paramsav")).ToString();
                            pc.PicturePath  = reader.GetValue(reader.GetOrdinal("picturepath")).ToString();
                            pc.ShowType  = reader.GetValue(reader.GetOrdinal("showtype")).ToString();
                            pc.Status  = reader.GetValue(reader.GetOrdinal("status")).ToString();
                            pc.TitleDesc   = reader.GetValue(reader.GetOrdinal("titledesc")).ToString();
                            cList.Add(pc);
                        }
                    }
                    reader.Close();
                }
                return cList;
            }
        }

        public List<MemberConnectionModel> GetMemberConnectionSuggestions(int memberID)
        {
            List<MemberConnectionModel> lst = new List<MemberConnectionModel>();
            return lst;
        }

        /// <summary>
        /// get search member contacts by search text wildcard
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<MemberConnectionModel> SearchMemberConnections(int memberID, string searchText)
        {
            var lst = (from mpf in _context.TbMemberProfile
                       join ct in _context.TbContacts on mpf.MemberId equals ct.ContactId
                       join mcti in _context.TbMemberProfileContactInfo on ct.ContactId equals mcti.MemberId
                       into t
                       from nt in t.DefaultIfEmpty()
                       where
                       ct.MemberId == memberID && (ct.Status == 0 || ct.Status == 1) &&
                       (mpf.FirstName.Contains(searchText) || mpf.LastName.Contains(searchText))
                       select new MemberConnectionModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName,
                           Location = nt.City + ", " + nt.State,
                           PicturePath = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,
                           ConnectionID = ct.ContactId.ToString(),
                           ShowType = "",
                           Status = ct.Status.ToString(),
                           TitleDesc = mpf.TitleDesc
                       }).ToList();
            return lst;
        }

        /// <summary>
        /// Get member network invite contacts list
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="networkID"></param>
        /// <returns></returns>
        public List<MemberConnectionModel> GetMemberNetworkInviteConnectionList(int memberID, int networkID)
        {
            var lst = (from mpf in _context.TbMemberProfile
                       join c in _context.TbContacts on mpf.MemberId equals c.ContactId
                       join mpci in _context.TbMemberProfileContactInfo on c.ContactId equals mpci.MemberId
                       where c.MemberId == memberID && (c.Status == 0 || c.Status == 1)
                       select new MemberConnectionModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName,
                           Location = mpci.City + ", " + mpci.State,
                           PicturePath = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,
                           ConnectionID = c.ContactId.ToString(),
                           ShowType = "",
                           Status = c.Status.ToString(),
                           TitleDesc = mpf.TitleDesc

                       }).ToList();
            return lst;
        }

        /// <summary>
        /// Get the list of contact requests.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<MemberConnectionModel> GetRequestsList(int memberID)
        {
            var lst = (from mpf in _context.TbMemberProfile
                       join c in _context.TbContactRequests on mpf.MemberId equals c.FromMemberId
                       where c.ToMemberId == memberID && (c.Status == 0)
                       select new MemberConnectionModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName,
                           PicturePath = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,
                           ConnectionID = c.FromMemberId.ToString(),

                       }).ToList();
            return lst;
        }

        /// <summary>
        /// /Get the list of members by search type
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<MemberConnectionModel> GetMembersBySearchType(int userID, string searchType, string searchText)
        {
            string[] s = searchText.Split(' ');
            string name = s[0];
            string name2 = "";
            if (s.Length > 1)
                name2 = s[1];

            if (searchType.ToLower() != "name" && searchType.ToLower() != "people")
                name = searchText;

            if (searchText.IndexOf("@", StringComparison.CurrentCulture) != -1) //email search
                searchType = "email";

            List<MemberConnectionModel> lst = null;

            var lstReq = (from t in _context.TbContactRequests where t.FromMemberId == userID select t).ToList();
            var lstCont = (from t in _context.TbContacts where t.MemberId == userID select new { t.MemberId }).ToList();

            if (searchType.ToLower() == "email")
            {
                lst = (from mpf in _context.TbMemberProfile
                       join m in _context.TbMembers on mpf.MemberId equals m.MemberId
                       where m.Email == name
                       select new MemberConnectionModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName,
                           PicturePath = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,
                           ConnectionID = m.MemberId.ToString(),
                           ShowType = "",
                           TitleDesc = mpf.TitleDesc,
                           Email = m.Email,
                           LabelText = ((m.MemberId == userID) || (lstReq.Any(mb => mb.FromMemberId == m.MemberId)) || ((lstCont.Any(c => c.MemberId == m.MemberId)) && m.MemberId != userID)) ? "View Profile" : "Add as Contact",
                           NameAndID = m.MemberId.ToString() + "," + mpf.FirstName + "," + mpf.LastName,
                           Params = m.MemberId.ToString() + ",'" + mpf.FirstName + "','" + mpf.LastName + "'",
                       }).ToList();
            }
            else if (searchType.ToLower() == "employer" || searchType.ToLower() == "company")
            {
                lst = (from mpf in _context.TbMemberProfile
                       join m in _context.TbMembers on mpf.MemberId equals m.MemberId
                       join e in _context.TbMemberProfileEmploymentInfoV2 on m.MemberId equals e.MemberId
                       where e.CompanyName.Contains(name)
                       select new MemberConnectionModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName,
                           PicturePath = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,
                           ConnectionID = m.MemberId.ToString(),
                           ShowType = "",
                           TitleDesc = mpf.TitleDesc,
                           Email = m.Email,
                           LabelText = ((m.MemberId == userID) || (lstReq.Any(mb => mb.FromMemberId == m.MemberId)) || ((lstCont.Any(c => c.MemberId == m.MemberId)) && m.MemberId != userID)) ? "View Profile" : "Add as Contact",
                           NameAndID = m.MemberId.ToString() + "," + mpf.FirstName + "," + mpf.LastName,
                           Params = m.MemberId.ToString() + ",'" + mpf.FirstName + "','" + mpf.LastName + "'",
                       }).ToList();
            }
            else if (searchType.ToLower() == "public")
            {
                lst = (from mpf in _context.TbMemberProfile
                       join m in _context.TbMembers on mpf.MemberId equals m.MemberId
                       join e in _context.TbMemberProfileEducationV2 on m.MemberId equals e.MemberId
                       where e.SchoolName.Contains(name) && e.SchoolType == 1
                       select new MemberConnectionModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName,
                           PicturePath = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,
                           ConnectionID  = m.MemberId.ToString(),
                           ShowType = "",
                           TitleDesc = mpf.TitleDesc,
                           Email = m.Email,
                           LabelText = ((m.MemberId == userID) || (lstReq.Any(mb => mb.FromMemberId == m.MemberId)) || ((lstCont.Any(c => c.MemberId == m.MemberId)) && m.MemberId != userID)) ? "View Profile" : "Add as Contact",
                           NameAndID = m.MemberId.ToString() + "," + mpf.FirstName + "," + mpf.LastName,
                           Params = m.MemberId.ToString() + ",'" + mpf.FirstName + "','" + mpf.LastName + "'",
                       }).ToList();
            }
            else if (searchType.ToLower() == "private")
            {
                lst = (from mpf in _context.TbMemberProfile
                       join m in _context.TbMembers on mpf.MemberId equals m.MemberId
                       join e in _context.TbMemberProfileEducationV2 on m.MemberId equals e.MemberId
                       where e.SchoolName.Contains(name) && e.SchoolType == 2
                       select new MemberConnectionModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName,
                           PicturePath = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,
                           ConnectionID = m.MemberId.ToString(),
                           ShowType = "",
                           TitleDesc = mpf.TitleDesc,
                           Email = m.Email,
                           LabelText = ((m.MemberId == userID) || (lstReq.Any(mb => mb.FromMemberId == m.MemberId)) || ((lstCont.Any(c => c.MemberId == m.MemberId)) && m.MemberId != userID)) ? "View Profile" : "Add as Contact",
                           NameAndID = m.MemberId.ToString() + "," + mpf.FirstName + "," + mpf.LastName,
                           Params = m.MemberId.ToString() + ",'" + mpf.FirstName + "','" + mpf.LastName + "'",
                       }).ToList();
            }
            else if (searchType.ToLower() == "college")
            {
                lst = (from mpf in _context.TbMemberProfile
                       join m in _context.TbMembers on mpf.MemberId equals m.MemberId
                       join e in _context.TbMemberProfileEducationV2 on m.MemberId equals e.MemberId
                       where e.SchoolName.Contains(searchText) && e.SchoolType == 3
                       select new MemberConnectionModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName,
                           PicturePath = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,
                           ConnectionID = m.MemberId.ToString(),
                           ShowType = "",
                           TitleDesc = mpf.TitleDesc,
                           Email = m.Email,
                           LabelText = ((m.MemberId == userID) || (lstReq.Any(mb => mb.FromMemberId == m.MemberId)) || ((lstCont.Any(c => c.MemberId == m.MemberId)) && m.MemberId != userID)) ? "View Profile" : "Add as Contact",
                           NameAndID = m.MemberId.ToString() + "," + mpf.FirstName + "," + mpf.LastName,
                           Params = m.MemberId.ToString() + ",'" + mpf.FirstName + "','" + mpf.LastName + "'",
                       }).ToList();
            }
            else if (searchType.ToLower() == "name" || searchType.ToLower() == "people")
            {
                lst = (from mpf in _context.TbMemberProfile
                       join m in _context.TbMembers on mpf.MemberId equals m.MemberId
                       //join e in context.TbMemberProfileEducationV2 on m.MemberId equals e.MemberId
                       where mpf.FirstName.Contains(name) && mpf.LastName.Contains(name2)
                       select new MemberConnectionModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName,
                           PicturePath = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,
                           ConnectionID = m.MemberId.ToString(),
                           ShowType = "",
                           TitleDesc = mpf.TitleDesc,
                           Email = m.Email,
                           LabelText = ((m.MemberId == userID) || (lstReq.Any(mb => mb.FromMemberId == m.MemberId)) || ((lstCont.Any(c => c.MemberId == m.MemberId)) && m.MemberId != userID)) ? "View Profile" : "Add as Contact",
                           NameAndID = m.MemberId.ToString() + "," + mpf.FirstName + "," + mpf.LastName,
                           Params = m.MemberId.ToString() + ",'" + mpf.FirstName + "','" + mpf.LastName + "'",
                       }).ToList();
            }
            return lst;
        }

        /// <summary>
        /// Gets the search contacts.
        /// </summary>
        /// <returns>The search contacts.</returns>
        /// <param name="userID">User identifier.</param>
        /// <param name="searchText">Search text.</param>
        public List<MemberConnectionModel> GetSearchConnections(int userID, string searchText)
        {
            var result = _context.Set<MemberConnectionModel>().FromSqlRaw("exec spGetSearchContacts @UserID, @SearchText, @SearchText2 ", new SqlParameter("@UserID", userID), new SqlParameter("@SearchText", searchText), new SqlParameter("@SearchText2", ""));
            return new List<MemberConnectionModel>(result);
        }

        /// <summary>
        /// Get the list of members phone book.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<MemberPhoneBookModel> GetMemberPhoneBook(int memberID)
        {
            var lst = (from mpf in _context.TbMemberProfile
                       join c in _context.TbContacts on mpf.MemberId equals c.MemberId
                       join ps in _context.TbMemberProfileContactInfo on c.MemberId equals ps.MemberId
                       where (mpf.MemberId == memberID && ps.HomePhone == null && ps.CellPhone != null)

                       select new MemberPhoneBookModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           HomePhone = ps.HomePhone,
                           CellPhone = ps.CellPhone,
                           PicturePath = (string.IsNullOrEmpty(mpf.PicturePath)) ? "default.png" : mpf.PicturePath,
                           ConnectionID = c.ContactId.ToString()
                       }
                      )
                    .ToList();
            return lst;
        }

        /// <summary>
        /// Get the list of members browsed contacts.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="categoryID"></param>
        /// <param name="subCategory"></param>
        /// <returns></returns>
        public List<MemberConnectionModel> GetMemberBrowsedConnections(int memberID, int categoryID, string subCategory)
        {
            List<MemberConnectionModel> lst = null;
            if (categoryID == 1)
            {
                lst = (from mpf in _context.TbMemberProfile
                       join c in _context.TbContacts on mpf.MemberId equals c.MemberId
                       join ps in _context.TbMemberProfileContactInfo on c.MemberId equals ps.MemberId
                       where (mpf.MemberId == memberID && ps.City == subCategory)

                       select new MemberConnectionModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           Location = ps.City + " " + ps.State,
                           TitleDesc = mpf.TitleDesc,
                           PicturePath = (string.IsNullOrEmpty(mpf.PicturePath)) ? "default.png" : mpf.PicturePath,
                           ConnectionID = c.ContactId.ToString()
                       }
                      ).ToList();
            }
            else if (categoryID == 2)
            {


                lst = (from mpf in _context.TbMemberProfile
                       join c in _context.TbContacts on mpf.MemberId equals c.MemberId
                       join ps in _context.TbMemberProfileContactInfo on c.MemberId equals ps.MemberId
                       join ed in _context.TbMemberProfileEducationV2 on c.MemberId equals ed.MemberId
                       where (mpf.MemberId == memberID && ed.SchoolName == subCategory && ed.SchoolType == 3)

                       select new MemberConnectionModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           Location = ps.City + " " + ps.State,
                           TitleDesc = mpf.TitleDesc,
                           PicturePath = (string.IsNullOrEmpty(mpf.PicturePath)) ? "default.png" : mpf.PicturePath,
                           ConnectionID = c.ContactId.ToString()

                       }
                      ).ToList();
            }
            else
            {
                lst = (from mpf in _context.TbMemberProfile
                       join c in _context.TbContacts on mpf.MemberId equals c.MemberId
                       join ps in _context.TbMemberProfileContactInfo on c.MemberId equals ps.MemberId
                       join ed in _context.TbMemberProfileEducationV2 on c.MemberId equals ed.MemberId
                       where (mpf.MemberId == memberID && ed.SchoolName == subCategory && (ed.SchoolType == 1 || ed.SchoolType == 2))

                       select new MemberConnectionModel()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           Location = ps.City + " " + ps.State,
                           TitleDesc = mpf.TitleDesc,
                           PicturePath = (string.IsNullOrEmpty(mpf.PicturePath)) ? "default.png" : mpf.PicturePath,
                           ConnectionID = c.ContactId.ToString()
                       }
                      ).ToList();
            }
            return lst;
        }

        /// <summary>
        /// Search for member by type
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="searchType"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<MemberByTypeModel> SearchMemberByType(int userID, int searchType, string searchString)
        {
            var lstCont = (from t in _context.TbContacts where t.MemberId == userID select new { t.MemberId }).ToList();

            List<MemberByTypeModel> lst = null;

            if (searchType == 1)
            { //email
                lst = (from mpf in _context.TbMemberProfile
                       join m in _context.TbMembers on mpf.MemberId equals m.MemberId
                       where m.Email.Contains(searchString)

                       select new MemberByTypeModel()
                       {
                           MemberID = m.MemberId.ToString(),
                           Name = mpf.FirstName + " " + mpf.LastName,
                           TypeVal = m.Email,
                           PicturePath = (string.IsNullOrEmpty(mpf.PicturePath)) ? "default.png" : mpf.PicturePath,
                           IsFriend = ((lstCont.Any(c => c.MemberId == m.MemberId)) && m.MemberId != userID) ? "yes" : "no",
                           IsSamePerson = (m.MemberId == userID) ? "yes" : "no",
                       }
                      ).ToList();
            }
            else if (searchType == 2)
            { //high schools
                lst = (from mpf in _context.TbMemberProfile
                       join m in _context.TbMembers on mpf.MemberId equals m.MemberId
                       join p in _context.TbMemberProfileEducationV2 on m.MemberId equals p.MemberId
                       where p.SchoolName.Contains(searchString) && (p.SchoolType == 1 || p.SchoolType == 0)

                       select new MemberByTypeModel()
                       {
                           MemberID = m.MemberId.ToString(),
                           Name = mpf.FirstName + " " + mpf.LastName,
                           TypeVal = p.SchoolName,
                           PicturePath = (string.IsNullOrEmpty(mpf.PicturePath)) ? "default.png" : mpf.PicturePath,
                           IsFriend = ((lstCont.Any(c => c.MemberId == m.MemberId)) && m.MemberId != userID) ? "yes" : "no",
                           IsSamePerson = (m.MemberId == userID) ? "yes" : "no",
                       }
                      ).ToList();
            }
            else if (searchType == 4)
            { //college
                lst = (from mpf in _context.TbMemberProfile
                       join m in _context.TbMembers on mpf.MemberId equals m.MemberId
                       join p in _context.TbMemberProfileEducationV2 on m.MemberId equals p.MemberId
                       where p.SchoolName.Contains(searchString) && (p.SchoolType == 3)

                       select new MemberByTypeModel()
                       {
                           MemberID = m.MemberId.ToString(),
                           Name = mpf.FirstName + " " + mpf.LastName,
                           TypeVal = p.SchoolName,
                           PicturePath = (string.IsNullOrEmpty(mpf.PicturePath)) ? "default.png" : mpf.PicturePath,
                           IsFriend = ((lstCont.Any(c => c.MemberId == m.MemberId)) && m.MemberId != userID) ? "yes" : "no",
                           IsSamePerson = (m.MemberId == userID) ? "yes" : "no",
                       }
                      ).ToList();
            }
            return lst;
        }

        /// <summary>
        /// Accept contact's request 
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="contactID"></param>
        public void AcceptRequest(int memberID, int contactID)
        {
            var pMemberID = new SqlParameter("@MemberID", memberID);
            var pContactID = new SqlParameter("@ContactID", contactID);
            _context.Database.ExecuteSqlRaw("EXEC spAcceptRequest @MemberID, @ContactID", pMemberID, pContactID);
        }

        /// <summary>
        /// Reject contact's request
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="contactID"></param>
        public void RejectRequest(int memberID, int contactID)
        {
            var pMemberID = new SqlParameter("@MemberID", memberID);
            var pContactID = new SqlParameter("@ContactID", contactID);
            _context.Database.ExecuteSqlRaw("EXEC spRejectRequest @MemberID, @ContactID", pMemberID, pContactID);

        }

        /// <summary>
        /// Delete contact
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="contactID"></param>
        public void DeleteConnection(int memberID, int contactID)
        {
            var pMemberID = new SqlParameter("@MemberID", memberID);
            var pContactID = new SqlParameter("@ContactID", contactID);
            _context.Database.ExecuteSqlRaw("EXEC spDeleteContact @MemberID, @ContactID", pMemberID, pContactID);
        }

        /// <summary>
        /// Get whose following me
        /// </summary>
        /// <returns>The list of members I am following.</returns>
        /// <param name="MemberID">User identifier.</param>
        public List<MemberConnectionModel> GetWhosFollowingMe(int memberID)
        {
            var result = _context.Set<MemberConnectionModel>().FromSqlRaw("exec spGetWhosFollowingMe @MemberID", new SqlParameter("@MemberID", memberID));
            return new List<MemberConnectionModel>(result);
        }

        /// <summary>
        /// get people I follow
        /// </summary>
        /// <param name="memberID">User identifier.</param>
        public List<MemberConnectionModel> GetPeopleIFollow(int memberID)
        {
            var result = _context.Set<MemberConnectionModel>().FromSqlRaw("exec spGetFollowedMembers @MemberID", new SqlParameter("@MemberID", memberID));
            return new List<MemberConnectionModel>(result);
        }
        
        /// <summary>
        /// follow member
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="contactID"></param>
        public void FollowMember(int memberID, int contactID)
        {
            var pMemberID = new SqlParameter("@MemberID", memberID);
            var pContactID = new SqlParameter("@FollowingMemberID", contactID);
            _context.Database.ExecuteSqlRaw("EXEC spAddFollowingMember @MemberID, @FollowingMemberID", pMemberID, pContactID);
        }

        /// <summary>
        /// un follow member
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="contactID"></param>
        public void UnfollowMember(int memberID, int contactID)
        {
            var pMemberID = new SqlParameter("@MemberID", memberID);
            var pContactID = new SqlParameter("@FollowingMemberID", contactID);
            _context.Database.ExecuteSqlRaw("EXEC spUnFollowMember @MemberID, @FollowingMemberID", pMemberID, pContactID);
        }


        /// <summary>
        /// Get entity by search type
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="searchText"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public List<EntityModel> GetEntityBySearchType(int memberID, string searchText, string searchType)
        {
            List<EntityModel> lst = null;
            if (searchType.ToLower() == "contact")
            {
                lst = (from mpf in _context.TbMemberProfile
                       join c in _context.TbContacts on mpf.MemberId equals c.MemberId
                       join p in _context.TbMemberProfileContactInfo on c.MemberId equals p.MemberId
                       where c.MemberId == memberID && (mpf.LastName.Contains(searchText) || mpf.FirstName.Contains(searchText))

                       select new EntityModel()
                       {
                           EntityID = c.ContactId.ToString(),
                           EntityName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName,
                           LastName = mpf.LastName,
                           PicturePath = (string.IsNullOrEmpty(mpf.PicturePath)) ? "default.png" : mpf.PicturePath,
                           CityState = p.City + ", " + p.State,
                           Id = c.ContactId.ToString(),
                           Name = mpf.FirstName + " " + mpf.LastName
                       }
                      ).ToList();
            }
            else if (searchType == "people")
            {
                lst = (from mpf in _context.TbMemberProfile
                       join c in _context.TbMembers on mpf.MemberId equals c.MemberId
                       join p in _context.TbMemberProfileContactInfo on c.MemberId equals p.MemberId
                       where (mpf.LastName.Contains(searchText)) || (mpf.FirstName.Contains(searchText)) || ((mpf.FirstName + " " + mpf.LastName).Contains(searchText))

                       select new EntityModel()
                       {
                           EntityID = c.MemberId.ToString(),
                           EntityName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName,
                           LastName = mpf.LastName,
                           PicturePath = (string.IsNullOrEmpty(mpf.PicturePath)) ? "default.png" : mpf.PicturePath,
                           CityState = p.City + ", " + p.State,
                           Id = c.MemberId.ToString(),
                           Name = mpf.FirstName + " " + mpf.LastName
                       }
                      ).ToList();
            }
            else if (searchType == "network")
            {
                lst = (from net in _context.TbNetworks
                       where net.NetworkName.Contains(searchText)

                       select new EntityModel()
                       {
                           EntityID = net.NetworkId.ToString(),
                           EntityName = net.NetworkName,
                           FirstName = "",
                           LastName = "",
                           PicturePath = (string.IsNullOrEmpty(net.Image)) ? "default.png" : net.Image,
                           CityState = net.City + ", " + net.State,
                           Id = net.NetworkId.ToString(),
                           Name = net.NetworkName
                       }
                      ).ToList();
            }
            else if (searchType == "event")
            {
                lst = (from evt in _context.TbEvents
                       where evt.PlanningWhat.Contains(searchText)

                       select new EntityModel()
                       {
                           EntityID = evt.EventId.ToString(),
                           EntityName = evt.PlanningWhat,
                           FirstName = "",
                           LastName = "",
                           PicturePath = (string.IsNullOrEmpty(evt.EventImg)) ? "default.png" : evt.EventImg,
                           CityState = evt.StartDate.Value.ToShortDateString() + " at " + evt.StartTime + " - " + evt.Location,
                           Id = evt.EventId.ToString(),
                           Name = evt.PlanningWhat
                       }
                      ).ToList();
            }
            return lst;
        }

        /// <summary>
        /// Get contact search results 
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<ConnectionSearchModel> ContactSearchResults(int memberID, string searchText)
        {
            List<ConnectionSearchModel> lst = null;
            lst = (from mpf in _context.TbMemberProfile
                   join c in _context.TbContacts on mpf.MemberId equals c.MemberId
                   join p in _context.TbMemberProfileContactInfo on c.MemberId equals p.MemberId
                   where c.MemberId == memberID && (mpf.LastName.Contains(searchText) || mpf.FirstName.Contains(searchText))

                   select new ConnectionSearchModel()
                   {
                       EntityID = c.ContactId.ToString(),
                       EntityName = mpf.FirstName + " " + mpf.LastName,
                       FirstName = mpf.FirstName,
                       LastName = mpf.LastName,
                       PicturePath = (string.IsNullOrEmpty(mpf.PicturePath)) ? "default.png" : mpf.PicturePath,
                       CityState = p.City + ", " + p.State,
                       LabelText = "",
                       Email = "",
                       NameAndID = "",
                       Params = "",
                       ParamsAV = "",
                       Description = "",
                       MemberCount = "",
                       CreatedDate = "01/01/1900",
                       Location = "",
                       EventDate = "",
                       Rsvp = "",
                       StartDate = "",
                       EndDate = ""
                   }
                      ).ToList();
            return lst;
        }

        /// <summary>
        /// return people search result
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<ConnectionSearchModel> PeopleSearchResults(int memberID, string searchText)
        {
            var lstReq = (from t in _context.TbContactRequests where t.FromMemberId == memberID select t).ToList();
            var lstCont = (from t in _context.TbContacts where t.MemberId == memberID select new { t.MemberId }).ToList();

            List<ConnectionSearchModel> lst = null;
            lst = (from mpf in _context.TbMemberProfile
                   join m in _context.TbMembers on mpf.MemberId equals m.MemberId
                   where (mpf.LastName.Contains(searchText) || mpf.FirstName.Contains(searchText))

                   select new ConnectionSearchModel()
                   {
                       EntityID = m.MemberId.ToString(),
                       EntityName = mpf.FirstName + " " + mpf.LastName,
                       FirstName = mpf.FirstName,
                       LastName = mpf.LastName,
                       PicturePath = (string.IsNullOrEmpty(mpf.PicturePath)) ? "default.png" : mpf.PicturePath,
                       CityState = "",
                       LabelText = ((m.MemberId == memberID) || (lstReq.Any(mb => mb.FromMemberId == m.MemberId)) || ((lstCont.Any(c => c.MemberId == m.MemberId)) && m.MemberId != memberID)) ? "View Profile" : "Add as Contact",
                       Email = m.Email,
                       NameAndID = m.MemberId.ToString() + "," + mpf.FirstName + "," + mpf.LastName,
                       Params = m.MemberId.ToString() + ",'" + mpf.FirstName + "','" + mpf.LastName + "'",
                       ParamsAV = "",
                       Description = "",
                       MemberCount = "",
                       CreatedDate = "01/01/1900",
                       Location = "",
                       EventDate = "",
                       Rsvp = "",
                       StartDate = "",
                       EndDate = ""
                   }
                      ).ToList();
            return lst;
        }

        /// <summary>
        /// return network search result
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<NetworkSearchModel> NetworkSearchResults(int memberID, string searchText)
        {
            var lstNetReq = (from t in _context.TbNetworkRequests where t.MemberId == memberID select t).ToList();
            var lstNetMemb = (from t in _context.TbNetworkMembers where t.MemberId == memberID select new { t.NetworkId }).ToList();

            List<NetworkSearchModel> lst = null;
            lst = (from n in _context.TbNetworks
                   where (n.NetworkName.Contains(searchText))

                   select new NetworkSearchModel()
                   {
                       EntityID = n.NetworkId.ToString(),
                       EntityName = n.NetworkName,
                       PicturePath = (string.IsNullOrEmpty(n.Image)) ? "default.png" : n.Image,
                       CityState = "",
                       LabelText = (lstNetReq.Any(mb => mb.NetworkId == n.NetworkId)) || (lstNetMemb.Any(c => c.NetworkId == n.NetworkId)) ? "View Network" : "Join Network",
                       Email = string.IsNullOrEmpty(n.Email) ? "" : n.Email,
                       NameAndID = n.NetworkId.ToString() + "," + n.NetworkName,
                       Params = n.NetworkId.ToString() + ",'" + n.NetworkName,
                       ParamsAV = "",
                       Description = n.Description,
                       MemberCount = "0 Members",
                       CreatedDate = string.IsNullOrEmpty(n.CreatedDate.ToString()) ? "" : n.CreatedDate.ToString(),
                       Location = "",
                       EventDate = "",
                       Rsvp = "",
                       StartDate = "",
                       EndDate = ""
                   }
                      ).ToList();
            return lst;
        }

        /// <summary>
        /// return event search result
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<EventSearchModel> EventSearchResults(int memberID, string searchText)
        {
            List<EventSearchModel> lst = null;
            lst = (from e in _context.TbEvents
                   join ei in _context.TbEventInvites on e.EventId equals ei.EventId
                   where e.PlanningWhat.Contains(searchText)

                   select new EventSearchModel()
                   {
                       EntityID = e.EventId.ToString(),
                       EntityName = e.PlanningWhat,
                       PicturePath = (string.IsNullOrEmpty(e.EventImg)) ? "default.png" : e.EventImg,
                       CityState = "",
                       LabelText = "",
                       Email = "",
                       NameAndID = "",
                       EventParams = "",
                       Params = e.EventId.ToString() + ";" + e.PlanningWhat + ";" + ei.Rsvpstatus.ToString() + (string.IsNullOrEmpty(e.EventImg) ? "default.png" : e.EventImg).ToString() + ";" + e.StartDate.Value.ToShortDateString() + " at " + e.StartTime,
                       ParamsAV = "",
                       Description = "",
                       MemberCount = "",
                       CreatedDate = string.IsNullOrEmpty(e.CreatedDate.ToString()) ? "" : e.CreatedDate.ToString(),
                       Location = e.Location,
                       EventDate = e.StartDate.Value.ToShortDateString() + " at " + e.StartTime,
                       Rsvp = (ei.MemberId == memberID) ? ei.Rsvpstatus : "",
                       StartDate = e.StartDate.Value.ToShortDateString(),
                       EndDate = e.EndDate.Value.ToShortDateString(),
                       ShowCancel = (e.MemberId == memberID) ? "true" : "false"
                   }
                      ).ToList();
            return lst;
        }

        public List<SearchModel> SearchResults(int memberID, string searchText)
        {
            var result = _context.Set<SearchModel>().FromSqlRaw("exec spSearchResults @MemberID, @SearchText ", new SqlParameter("@MemberID", memberID), new SqlParameter("@SearchText", searchText));
            return new List<SearchModel>(result);
        }

        public List<SearchModel> SearchAllResults(int memberID, string searchText)
        {
            var result = _context.Set<SearchModel>().FromSqlRaw("exec spAllSearchResults @MemberID, @SearchText ", new SqlParameter("@MemberID", memberID), new SqlParameter("@SearchText", searchText));
            return new List<SearchModel>(result);
        }

        public void SendRequestConnection(string memberID, string contactID)
        {
            _mbrRepo.SendFriendRequest(Convert.ToInt32(memberID), contactID);
            string msg = "I would like to add you to my contact list so we can start networking. Please accept the request from your request connections list.";
            _msgRepo.CreateMessage(Convert.ToInt32(contactID), Convert.ToInt32(memberID), "Requesting Contact", msg, "", "");

        }

        public IEnumerable<dynamic> GetDynamicResult(string commandText, params SqlParameter[] parameters)
        {
            // Get the connection from DbContext
            var connection = _context.Database.GetDbConnection();

            // Open the connection if isn't open
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = commandText;
                command.Connection = connection;

                if (parameters?.Length > 0)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                using (var dataReader = command.ExecuteReader())
                {
                    // List for column names
                    var names = new List<string>();

                    if (dataReader.HasRows)
                    {
                        // Add column names to list
                        for (var i = 0; i < dataReader.VisibleFieldCount; i++)
                        {
                            names.Add(dataReader.GetName(i));
                        }
                        while (dataReader.Read())
                        {
                            // Create the dynamic result for each row
                            var result = new ExpandoObject() as IDictionary<string, object>;
                            foreach (var name in names)
                            {
                                // Add key-value pair
                                // key = column name
                                // value = column value
                                result.Add(name, dataReader[name]);
                            }
                            yield return result;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check to see if contact id is followin memberID.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="connectionID"></param>
        /// <returns></returns>
        public bool IsFollowingConnection(int memberID, int connectionID)
        {
            var result = _context.Set<ResultModel>().FromSqlRaw("exec IsFollowingConnection @MemberID, @FollowingMemberId ", new SqlParameter("@MemberID", memberID), new SqlParameter("@FollowingMemberId", connectionID));
            var  res= new List<ResultModel>(result);

           if (res.Count == 0)
                return false;
            else
                return true;
        }

    }

    public interface IConnectionRepository {
        List<MemberConnectionModel> GetMemberConnections(int memberID, string show);
        List<MemberConnectionModel> GetMemberConnectionSuggestions(int memberID);
        List<MemberConnectionModel> SearchMemberConnections(int memberID, string searchText);
        List<MemberConnectionModel> GetMemberNetworkInviteConnectionList(int memberID, int networkID);
        List<MemberConnectionModel> GetRequestsList(int memberID);
        List<MemberConnectionModel> GetMembersBySearchType(int userID, string searchType, string searchText);
        List<MemberConnectionModel> GetSearchConnections(int userID, string searchText);
        List<MemberPhoneBookModel> GetMemberPhoneBook(int memberID);
        List<MemberConnectionModel> GetMemberBrowsedConnections(int memberID, int categoryID, string subCategory);
       List<MemberByTypeModel>  SearchMemberByType(int userID, int searchType, string searchString);
        void AcceptRequest(int memberID, int contactID);
        void RejectRequest(int memberID, int contactID);
        void DeleteConnection(int memberID, int contactID);
        List<EntityModel> GetEntityBySearchType(int memberID, string searchText, string searchType);
        List<ConnectionSearchModel> ContactSearchResults(int memberID, string searchText);
        List<ConnectionSearchModel> PeopleSearchResults(int memberID, string searchText);
        List<NetworkSearchModel> NetworkSearchResults(int memberID, string searchText);
        List<EventSearchModel> EventSearchResults(int memberID, string searchText);
        List<SearchModel> SearchResults(int memberID, string searchText);
        List<SearchModel> SearchAllResults(int memberID, string searchText);
        void SendRequestConnection(string memberID, string contactID);
        List<MemberConnectionModel> GetWhosFollowingMe(int memberID);
        void UnfollowMember(int memberID, int contactID);
        void FollowMember(int memberID, int contactID);
        List<MemberConnectionModel> GetPeopleIFollow(int memberID);
        bool IsFollowingConnection(int memberID, int connectionID);
    }  
}
