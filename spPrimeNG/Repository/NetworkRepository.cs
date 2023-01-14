using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using sportprofiles.DBContextModels;
using sportprofiles.Models.Networks;

namespace sportprofiles.Repository
{
    /// <summary>
    /// Describes the functionalities for accessing data for networks
    /// </summary>
    public class NetworkRepository : INetworkRepository
    {
        #region methods...

        readonly dbContexts _context;
        public NetworkRepository(dbContexts context)
        {
            _context = context;
        }

        /// <summary>
        /// creates a network
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="networkName"></param>
        /// <param name="desc"></param>
        /// <param name="category"></param>
        /// <param name="type"></param>
        /// <param name="news"></param>
        /// <param name="office"></param>
        /// <param name="email"></param>
        /// <param name="webSite"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        /// <returns></returns>
        public int CreateNetwork(int memberID, string networkName,
                                        string desc,
                                        int category,
                                        int type,
                                        string news,
                                        string office,
                                        string email,
                                        string webSite,
                                        string street,
                                        string city,
                                        string state,
                                        string zip)
        {
            TbNetworks n = new TbNetworks();

            n.NetworkName = networkName;
            n.Description = desc;
            n.CategoryId = category;
            n.CategoryTypeId = type;
            n.RecentNews = news;
            n.Office = office;
            n.Email = email;
            n.Website = webSite;
            n.Street = street;
            n.City = city;
            n.State = state;
            n.Zip = zip;

            _context.TbNetworks.Add(n);
            _context.SaveChanges();


            if (n.NetworkId != 0)
            {
                return n.NetworkId;
            }
            else
                return 0;
        }


        /// <summary>
        /// update a network
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="networkName"></param>
        /// <param name="desc"></param>
        /// <param name="category"></param>
        /// <param name="type"></param>
        /// <param name="news"></param>
        /// <param name="office"></param>
        /// <param name="email"></param>
        /// <param name="webSite"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        public void UpdateNetwork(int networkID, string networkName,
                                        string desc,
                                        int category,
                                        int type,
                                        string news,
                                        string office,
                                        string email,
                                        string webSite,
                                        string street,
                                        string city,
                                        string state,
                                        string zip)
        {
            //update tb meber profile with new profile picture
            var nbr = (from n in _context.TbNetworks where n.NetworkId == networkID select n).First();
            nbr.NetworkName = networkName;
            nbr.Description = desc;
            nbr.CategoryId = category;
            nbr.CategoryTypeId = type;
            nbr.RecentNews = news;
            nbr.Office = office;
            nbr.Email = email;
            nbr.Website = webSite;
            nbr.Street = street;
            nbr.City = city;
            nbr.State = state;
            nbr.Zip = zip;
            _context.SaveChanges();
        }

        /// <summary>
        /// get a list of member networks
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<NetworkModel> GetMemberNetworks(int memberID)
        {
            var result = _context.Set<NetworkModel>().FromSqlRaw("exec spGetMemberNetworks @MemberID", new SqlParameter("@MemberID", memberID));
            return new List<NetworkModel>(result);
        }

        /// <summary>
        /// get network categories
        /// </summary>
        /// <returns></returns>
        public List<TbNetworkCategory> GetNetworkCategories()
        {
            var lst = (from t in _context.TbNetworkCategory select t).ToList();
            return lst;
        }

        /// <summary>
        /// get network category types
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<TbNetworkCategoryType> GetNetworkCategoryTypes(int categoryID)
        {
            var lst = (from t in _context.TbNetworkCategoryType where t.CategoryId == categoryID select t).ToList();
            return lst;
        }

        /// <summary>
        /// get network basic information
        /// </summary>
        /// <param name="networkID"></param>
        /// <returns></returns>
        public List<NetworkInfoModel> GetNetworkBasicInfo(int networkID)
        {
            var result = _context.Set<NetworkInfoModel>().FromSqlRaw("exec spGetNetworkBasicInfo @NetworkID", new SqlParameter("@NetworkID", networkID));
            return new List<NetworkInfoModel>(result);
        }

        /// <summary>
        /// get networks by category and subcategory
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="category"></param>
        /// <param name="subCategory"></param>
        /// <returns></returns>
        public List<NetworkInfoModel> GetNetworksByCatType(int memberID, int category, int subCategory)
        {
            var lst = (from n in _context.TbNetworks

                       where n.CategoryId == category && n.CategoryTypeId == subCategory
                       select new NetworkInfoModel()
                       {
                           NetworkID = n.NetworkId.ToString(),
                           NetworkName = n.NetworkName,
                           NetworkDesc = n.Description,
                           CategoryID = n.CategoryId.ToString() ?? "",
                           NetworkImage = string.IsNullOrEmpty(n.Image) ? "default.png" : n.Image,
                           MemberCount = (from t in _context.TbNetworkMembers where t.NetworkId == n.NetworkId select t).ToList().Count().ToString(),
                           CreatedDate = n.CreatedDate.ToString() ?? "",
                           IsAlreadyMemberID = ((from t in _context.TbNetworkMembers where t.MemberId == memberID && t.NetworkId == n.NetworkId select new { networkID = t.NetworkId }).ToList())[0].networkID.ToString()
                       }
                       ).ToList();
            return lst;
        }

        /// <summary>
        /// get network settings
        /// </summary>
        /// <param name="networkID"></param>
        /// <returns></returns>
        public List<TbNetworkSettings> GetNetworkSettings(int networkID)
        {
            var lst = (from n in _context.TbNetworkSettings

                       where n.NetworkId == networkID
                       select n).ToList();

            return (lst);
        }

        /// <summary>
        /// get network admins
        /// </summary>
        /// <param name="networkID"></param>
        /// <returns></returns>
        public List<NetworkMemberModel> GetNetworkAdmins(int networkID)
        {
            var lst = (from m in _context.TbMemberProfile
                       join nm in _context.TbNetworkMembers on m.MemberId equals nm.MemberId

                       where nm.NetworkId == networkID
                       select new NetworkMemberModel()
                       {

                           MemberID = m.MemberId.ToString(),
                           MemberName = m.FirstName + " " + m.LastName,
                           PicturePath = string.IsNullOrEmpty(m.PicturePath) ? "default.png" : m.PicturePath,
                           NetworkID = nm.NetworkId.ToString(),
                           IsOwner = nm.IsOwner.ToString() ?? "",
                           IsAdmin = nm.IsAdmin.ToString() ?? "",
                           JoinedDate = nm.JoinedDate.ToString() ?? "",
                           Access = (nm.IsOwner == true) ? "Owner" : "",
                           TitleDesc = m.TitleDesc
                       }
                      ).ToList();
            return (lst);
        }

        /// <summary>
        /// get network members
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="listType"></param>
        /// <returns></returns>
        public List<NetworkMemberModel> GetNetworkMembers(int networkID, string listType)
        {
            List<NetworkMemberModel> lst = new List<NetworkMemberModel>();
            if (!string.IsNullOrEmpty(listType)) 
            {

                lst = (from m in _context.TbMemberProfile
                       join nm in _context.TbNetworkMembers on m.MemberId equals nm.MemberId

                       where nm.NetworkId == networkID && nm.IsAdmin == false && nm.Status == 1
                       select new NetworkMemberModel()
                       {

                           MemberID = m.MemberId.ToString(),
                           MemberName = m.FirstName + " " + m.LastName,
                           PicturePath = string.IsNullOrEmpty(m.PicturePath) ? "default.png" : m.PicturePath,
                           NetworkID = nm.NetworkId.ToString(),
                           IsOwner = nm.IsOwner.ToString() ?? "",
                           IsAdmin = nm.IsAdmin.ToString() ?? "",
                           JoinedDate = nm.JoinedDate.ToString() ?? "",
                           Access = (nm.IsOwner == true) ? "Owner" : "",
                           TitleDesc = m.TitleDesc
                       }
                         ).ToList();
            }
            else if (listType.ToLower() == "joined")
            {

                lst = (from m in _context.TbMemberProfile
                       join nm in _context.TbNetworkMembers on m.MemberId equals nm.MemberId

                       where nm.NetworkId == networkID && nm.Status == 1
                       select new NetworkMemberModel()
                       {

                           MemberID = m.MemberId.ToString(),
                           MemberName = m.FirstName + " " + m.LastName,
                           PicturePath = string.IsNullOrEmpty(m.PicturePath) ? "default.png" : m.PicturePath,
                           NetworkID = nm.NetworkId.ToString(),
                           IsOwner = nm.IsOwner.ToString() ?? "",
                           IsAdmin = nm.IsAdmin.ToString() ?? "",
                           JoinedDate = nm.JoinedDate.ToString() ?? "",
                           Access = (nm.IsOwner == true) ? "Owner" : "",
                           TitleDesc = m.TitleDesc
                       }
                     ).ToList();
            }
            else if (listType.ToLower() == "invitees")
            {
                lst = (from nm in _context.TbNetworkMemberInvites
                       join mfp in _context.TbMemberProfile on nm.MemberId equals mfp.MemberId
                       where nm.NetworkId == networkID && nm.Status == 0
                       select new NetworkMemberModel()
                       {
                           InviteID = nm.InviteId.ToString(),
                           MemberID = nm.MemberId.ToString() ?? "",
                           MemberName = mfp.FirstName + " " + mfp.LastName,
                           PicturePath = string.IsNullOrEmpty(mfp.PicturePath) ? "default.png" : mfp.PicturePath,
                           NetworkID = nm.NetworkId.ToString() ?? "",
                           IsOwner = "",
                           IsAdmin = "",
                           JoinedDate = DateTime.Now.ToShortDateString(),
                           Access = "",
                           TitleDesc = mfp.TitleDesc
                       }
                     )
                    .Union
                      (

                        from nm in _context.TbNetworkMemberInvites
                        where (nm.Status == 0 || nm.Status == null)
                               && (nm.Email != null || nm.Email != "")

                        select new NetworkMemberModel()
                        {
                            InviteID = nm.InviteId.ToString(),
                            MemberID = nm.MemberId.ToString() ?? "",
                            MemberName = nm.Email,
                            PicturePath = "default.png",
                            NetworkID = nm.NetworkId.ToString() ?? "",
                            IsOwner = "",
                            IsAdmin = "",
                            JoinedDate = DateTime.Now.ToShortDateString(),
                            Access = "",
                            TitleDesc = ""
                        }
                    ).ToList();

            }
            return (lst);
        }

        /// <summary>
        /// Gets the contacts not in network.
        /// </summary>
        /// <returns>The contacts not in network.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="networkID">Network identifier.</param>
        public List<NetworkMemberModel> GetContactsNotInNetwork(int memberID, int networkID)
        {
            var lst = (from c in _context.TbContacts
                       join
                        m in _context.TbNetworkMembers on c.MemberId equals m.MemberId
                       join mp in _context.TbMemberProfile on c.MemberId equals mp.MemberId

                       where m.MemberId == memberID
                       select new NetworkMemberModel()
                       {
                           MemberID = m.MemberId.ToString(),
                           MemberName = mp.FirstName + " " + mp.LastName,
                           PicturePath = string.IsNullOrEmpty(mp.PicturePath) ? "default.png" : mp.PicturePath,
                           NetworkID = m.NetworkId.ToString(),
                           IsOwner = m.IsOwner.ToString() ?? "",
                           IsAdmin = m.IsAdmin.ToString() ?? "",
                           JoinedDate = m.JoinedDate.ToString() ?? "",
                           Access = "",
                           TitleDesc = mp.TitleDesc
                       }
                      ).ToList();
            return lst;
        }

        /// <summary>
        /// add a network member 
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="networkID"></param>
        public void AddNetworkMembers(int memberID, int networkID)
        {
            var pID = new SqlParameter("@MemberID", memberID);
            var pNetworkID = new SqlParameter("@NetworkID", networkID);
            _context.Database.ExecuteSqlRaw("EXEC spAddNetworkMembers @MemberID, @NetworkID", pID, pNetworkID);
        }

        /// <summary>
        /// Ingore a membe who wants to join network
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="networkID"></param>
        public void IgnoreJoinNetwork(int memberID, int networkID)
        {
            var pID = new SqlParameter("@MemberID", memberID);
            var pNetworkID = new SqlParameter("@NetworkID", networkID);
            _context.Database.ExecuteSqlRaw("EXEC spIgnoreJoinNetwork @MemberID, @NetworkID", pID, pNetworkID);
        }

        /// <summary>
        /// Update network settings
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="access"></param>
        /// <param name="nonAdminCanWrite"></param>
        /// <param name="showNetworkEvents"></param>
        /// <param name="enableDiscussionBoard"></param>
        /// <param name="enablePhotos"></param>
        /// <param name="enableVideos"></param>
        /// <param name="enableLinks"></param>
        /// <param name="onlyAllowAdminsToUploadPhotos"></param>
        /// <param name="onlyAllowAdminsToUploadVideos"></param>
        /// <param name="onlyAllowAdminsToPostLinks"></param>
        public void UpdateNetworkSettings(int networkID, int access, bool nonAdminCanWrite,
                                                bool showNetworkEvents,
                                                bool enableDiscussionBoard,
                                                bool enablePhotos,
                                                bool enableVideos,
                                                bool enableLinks,
                                                bool onlyAllowAdminsToUploadPhotos,
                                                bool onlyAllowAdminsToUploadVideos,
                                                bool onlyAllowAdminsToPostLinks)
        {
            
            var q = "EXEC spUpdateNetworkSettings @NetworkID, @Access, @NonAdminCanWrite, @ShowGroupEvents,";
            q = q + "@EnableDiscussionBoard, @EnablePhotos, @EnableVideos, @EnableLinks,@OnlyAllowAdminsToUploadPhotos, ";
            q = q + "@OnlyAllowAdminsToUploadVideos, @OnlyAllowAdminsToPostLinks";

            var pID = new SqlParameter("@NetworkID", networkID);
            var pAccess = new SqlParameter("@Access", access);
            var pNACW = new SqlParameter("@NonAdminCanWrite", nonAdminCanWrite);
            var pSGE = new SqlParameter("@ShowGroupEvents", showNetworkEvents);
            var pEDB = new SqlParameter("@EnableDiscussionBoard", enableDiscussionBoard);
            var pEP = new SqlParameter("@EnablePhotos", enablePhotos);
            var pEV = new SqlParameter("@EnableVideos", enableVideos);
            var pEL = new SqlParameter("@EnableLinks", enableLinks);
            var pOAP = new SqlParameter("@OnlyAllowAdminsToUploadPhotos", onlyAllowAdminsToUploadPhotos);
            var pOAPV = new SqlParameter("@OnlyAllowAdminsToUploadVideos", onlyAllowAdminsToUploadVideos);
            var pOAPL = new SqlParameter("@OnlyAllowAdminsToPostLinks", onlyAllowAdminsToPostLinks);
            _context.Database.ExecuteSqlRaw(q, pID, pAccess,pNACW,pSGE,pEDB,pEP,pEV,pEL,pOAP,pOAPV,pOAPL);
        }


        /// <summary>
        /// Invite member to join network
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="memberID"></param>
        public void InviteMemberToJoinNetwork(int networkID, int memberID)
        {
            var pID = new SqlParameter("@MemberID", memberID);
            var pNetworkID = new SqlParameter("@NetworkID", networkID);
            _context.Database.ExecuteSqlRaw("EXEC spInviteMemberToJoinNetwork @NetworkID, @MemberID", pNetworkID, pID);
        }

        /// <summary>
        /// Invite email to join network
        /// </summary>
        /// <param name="NetworkID"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public int InviteEmailToJoinNetwork(int NetworkID, string email)
        {
            TbNetworkMemberInvites n = new TbNetworkMemberInvites();
            n.NetworkId = NetworkID;
            n.Email = email;
            _context.TbNetworkMemberInvites.Add(n);
            _context.SaveChanges();
            if (n.InviteId != 0)
            {
                return n.InviteId;
            }
            else
                return (0);
        }

        /// <summary>
        /// Check to see if email is a network member 
        /// </summary>
        /// <param name="NetworkID"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsNetworkMember(int NetworkID, string email)
        {
            var lst = (from m in _context.TbMembers where m.Email == email select new { m.MemberId }).ToList();
            if (lst.Count == 0)
                return false;
            else
            {
                int memberID = Convert.ToInt32(lst[0].MemberId.ToString());
                var nlst = (from n in _context.TbNetworkMembers where n.MemberId == memberID && n.NetworkId == NetworkID select new { n.NetworkId }).ToList();

                if (nlst.Count != 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// checks to see if the member has made a request to join the network
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public bool IsMemberNetWorkRequestor(int networkID, int memberID)
        {
             var lst = (from n in _context.TbNetworkMemberRequests where (n.NetworkId == networkID) && (n.RequestorId == memberID) select new { n.RequestorId }).ToList();
            return lst.Count != 0;
        }

        /// <summary>
        /// get the recent netwrok activity list
        /// </summary>
        /// <param name="networkID"></param>
        /// <returns></returns>
        public List<RecentNetworkActivitiesResult> GetRecentNetworkActivities(int networkID)
        {
            var lst = (from r in _context.TbNetworkRecentActivities
                       join a in _context.TbActivityType on r.ActivityTypeId equals a.ActivityTypeId
                       where r.NetworkId == networkID
                       orderby r.ActivityDate descending
                       select new RecentNetworkActivitiesResult()
                       {
                           ActivityID = r.ActivityId.ToString(),
                           Description = r.Description,
                           ActivityDate = r.ActivityDate.ToString() ?? "",
                           ImageFile = string.IsNullOrEmpty(a.ImageFile) ? "default.png" : a.ImageFile,
                       }).ToList();

            return lst;
        }

        //get the network post list.
        public List<NetworkPostsModel> GetNetworkPosts(int networkID)
        {
            List<NetworkPostsModel> lst;
            lst = (from mn in _context.TbNetworkPosts
                   join mnn in _context.TbMemberProfile on mn.MemberId equals mnn.MemberId
                   where mn.NetworkId == networkID //l.Contains((int)mn.MemberId)
                   orderby mn.PostDate descending
                   select new NetworkPostsModel()
                   {
                       PostID = mn.PostId.ToString(),
                       Title = mn.Title,
                       Description = mn.Description,
                       PostDate = mn.PostDate.ToString() ?? "",
                       AttachFile = mn.AttachFile,
                       MemberID = mn.MemberId.ToString() ?? "",
                       PicturePath = mnn.PicturePath,
                       MemberName = mnn.FirstName + " " + mnn.LastName,
                       FirstName = mnn.FirstName,
                   }).Take(18).ToList();
            return lst;
        }

        /// <summary>
        /// Get the network post responses per post id
        /// </summary>
        /// <param name="postID"></param>
        /// <returns></returns>
        public List<NetworkChildPostsModel> GetNetworkPostResponses(int postID)
        {
            var lst = (from r in _context.TbNetworkPostResponses
                       join a in _context.TbMemberProfile on r.MemberId equals a.MemberId
                       where r.PostId == postID
                       orderby r.ResponseDate descending

                       select new NetworkChildPostsModel()
                       {
                           PostResponseID = r.PostResponseId.ToString(),
                           PostID = r.PostId.ToString() ?? "",
                           Description = r.Description,
                           ResponseDate = r.ResponseDate.ToString() ?? "",
                           MemberID = r.MemberId.ToString() ?? "",
                           PicturePath = string.IsNullOrEmpty(a.PicturePath) ? "default.png" : a.PicturePath,
                           MemberName = a.FirstName + " " + a.LastName,
                           FirstName = a.FirstName
                       }).ToList();
            return lst;
        }

        /// <summary>
        /// Update profile picture
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="fileName"></param>
        public void UpdateProfilePicture(int networkID, string fileName)
        {
            var pic = (from n in _context.TbNetworks where n.NetworkId == networkID select n).First();
            pic.Image = fileName;
            _context.SaveChanges();
        }

        /// <summary>
        /// create a network post
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="memberID"></param>
        /// <param name="postMsg"></param>
        public void CreateNetworkPost(int networkID, int memberID, string postMsg)
        {
            TbNetworkPosts np = new TbNetworkPosts();
            np.NetworkId = networkID;
            np.MemberId = memberID;
            np.PostDate = (DateTime?)DateTime.Now;
            np.Description = postMsg;
            _context.TbNetworkPosts.Add(np);
            _context.SaveChanges();
        }

        /// <summary>
        /// create a post comment
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="memberID"></param>
        /// <param name="postID"></param>
        /// <param name="postMsg"></param>
        public void CreatePostComment(int networkID, int memberID, int postID, string postMsg)
        {
            TbNetworkPostResponses np = new TbNetworkPostResponses();
            np.NetworkId = networkID;
            np.MemberId = memberID;
            np.ResponseDate = (DateTime?)DateTime.Now;
            np.PostId = postID;
            np.Description = postMsg;
            _context.TbNetworkPostResponses.Add(np);
            _context.SaveChanges();
        }

        /// <summary>
        /// Creata a comment for topic
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="topicID"></param>
        /// <param name="postMsg"></param>
        public void CreatePostComment(int memberID, int topicID, string postMsg)
        {
            var pMemID = new SqlParameter("@MemberID", memberID);
            var pTopicID = new SqlParameter("@TopicID", topicID);
            var pPostMsg = new SqlParameter("@PostMsg", postMsg);
            _context.Database.ExecuteSqlRaw(" EXEC spCreateTopicPostComment @MemberID, @TopicID, @PostMsg", pMemID, pTopicID, pPostMsg);
        }


        /// <summary>
        /// get a network basic info
        /// </summary>
        /// <param name="networkID"></param>
        /// <returns></returns>
        public List<NetworkInfoModel> GetNetworkInfo(int networkID)
        {
            var lst = (from n in _context.TbNetworks
                       join s in _context.TbNetworkSettings
                        on n.NetworkId equals s.NetworkId
                       join nc in _context.TbNetworkCategory
                        on n.CategoryId equals nc.CategoryId
                       join nct in _context.TbNetworkCategoryType
                        on n.CategoryTypeId equals nct.CategoryTypeId
                       where n.NetworkId == networkID
                       select new NetworkInfoModel()
                       {

                           NetworkID = n.NetworkId.ToString(),
                           NetworkName = n.NetworkName,
                           NetworkDesc = n.Description,
                           CategoryID = n.CategoryId.ToString() ?? "",
                           CategoryTypeID = n.CategoryTypeId.ToString() ?? "",
                           RecentNews = n.RecentNews,
                           Office = n.Office,
                           Email = n.Email,
                           WebSite = n.Website,
                           Street = n.Street,
                           City = n.City,
                           State = n.State,
                           Zip = n.Zip,
                           InActive = n.InActive.ToString() ?? "",
                           CreatedDate = n.CreatedDate.ToString() ?? "",
                           NetworkImage = string.IsNullOrEmpty(n.Image) ? "default.png" : n.Image,
                           CategoryDesc = nc.Description,
                           CategoryTypeDesc = nct.Description,
                           Access = s.Access.ToString() ?? ""

                       }).ToList();
            return (lst);
        }

        /// <summary>
        /// creates a photo for network by a member
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="memberID"></param>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public int CreatePhoto(int networkID, int memberID, string title, string desc, string ext)
        {
            TbNetworkPhotos np = new TbNetworkPhotos();
            np.NetworkId = networkID;
            np.MemberId = memberID;
            np.PhotoName = title;
            np.PhotoDesc = desc;
            _context.TbNetworkPhotos.Add(np);
            _context.SaveChanges();
            int id = np.PhotoId;
            return id;
        }

        /// <summary>
        /// get network photos.
        /// </summary>
        /// <param name="networkID"></param>
        /// <returns></returns>
        public List<NetworkPhotosModel> GetNetworkPhotos(int networkID)
        {
            var lst = (from n in _context.TbNetworkPhotos
                       join m in _context.TbMemberProfile
                        on n.MemberId equals m.MemberId

                       where n.NetworkId == networkID && n.Removed != true
                       orderby n.PhotoId ascending

                       select new NetworkPhotosModel()
                       {
                           PhotoID = n.PhotoId.ToString(),
                           MemberID = n.MemberId.ToString() ?? "",
                           MemberName = m.FirstName + " " + m.LastName,
                           PhotoName = n.PhotoName,
                           PhotoDesc = n.PhotoDesc,
                           FileName = n.FileName.ToString(),
                           Params = n.PhotoId + ":" + n.PhotoName + ":" + n.PhotoDesc,
                           CreatedDate = n.CreatedDate.ToString(),

                       }).ToList();
            return (lst);
        }

        /// <summary>
        /// remove photo by photo id
        /// </summary>
        /// <param name="photoID"></param>
        public void RemovePhoto(int photoID)
        {
            var pPhotoID = new SqlParameter("@PhotoID", photoID);
            _context.Database.ExecuteSqlRaw("EXEC spRemoveNetworkPhoto @PhotoID", pPhotoID);
        }

        /// <summary>
        /// Update network photo info
        /// </summary>
        /// <param name="photoID"></param>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        public void UpdateNetworkPhoto(int photoID, string title, string desc)
        {
            var pPhotoID = new SqlParameter("@PhotoID", photoID);
            var pTitle = new SqlParameter("@Title", title);
            var pDesc = new SqlParameter("@Desc", desc);
            _context.Database.ExecuteSqlRaw("EXEC spUpdateNetworkPhoto @PhotoID,@Title, @Desc", pPhotoID, pTitle, pDesc);
        }

        /// <summary>
        /// get network events
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<NetworkEventModel> GetNetworkEvents(int networkID, int memberID)
        {
            var lst = (from n in _context.TbEvents
                       join m in _context.TbEventInvites
                       on n.EventId equals m.EventId

                       where n.NetworkId == networkID && n.MemberId == memberID
                       orderby n.EventId descending

                       select new NetworkEventModel()
                       {
                           EventID = n.EventId.ToString(),
                           EventImg = string.IsNullOrEmpty(n.EventImg) ? "default.png" : n.EventImg,
                           PlanningWhat = n.PlanningWhat,
                           Location = n.Location,
                           EventDate = (n.StartDate != null) ? n.StartDate.Value.ToShortDateString() + " at " + n.StartTime : "",
                           RSVP = m.Rsvpstatus,
                           EventParams = n.EventId + ";" + n.PlanningWhat + ";" + m.Rsvpstatus + ";" + (string.IsNullOrEmpty(n.EventImg) ? "default.png" : n.EventImg) + ";" + n.StartDate.Value.ToShortDateString() + ";" + n.StartTime,
                           ShowCancel = (n.MemberId == memberID) ? "true" : "false"

                       }).ToList();
            return (lst);
        }

        /// <summary>
        /// Create a topic for network discussions
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="memberID"></param>
        /// <param name="topicName"></param>
        /// <param name="post"></param>
        public void CreateTopic(int networkID, int memberID, string topicName, string post)
        {
            if (post == null) { post = ""; }
            var pNetID = new SqlParameter("@NetworkID", networkID);
            var pMemID = new SqlParameter("@MemberID", memberID);
            var pTopicName = new SqlParameter("@TopicName", topicName);
            var pPost = new SqlParameter("@Post", post);
            _context.Database.ExecuteSqlRaw(" EXEC spCreateTopic @NetworkID, @MemberID, @TopicName, @Post", pNetID, pMemID, pTopicName, pPost);
        }

        /// <summary>
        /// Get network discussions topics for network
        /// </summary>
        /// <param name="networkID"></param>
        /// <returns></returns>
        public List<NetworkTopicsModel> GetNetworkDiscussionTopics(int networkID)
        {
            var lst = (from t in _context.TbNetworkDiscussionTopics
                       join m in _context.TbMemberProfile
                       on t.MemberId equals m.MemberId

                       where t.NetworkId == networkID
                       orderby t.TopicId descending

                       select new NetworkTopicsModel()
                       {
                           TopicID = t.TopicId.ToString(),
                           TopicDesc = t.TopicDesc,
                           MemberName = m.FirstName + " " + m.LastName,
                           CreatedDate = (t.CreatedDate != null) ? t.CreatedDate.Value.ToLongDateString() : "",
                           MemberID = t.MemberId.ToString()?? "",
                           TopicPostCnt = (from d in _context.TbDiscussionTopicPosts where d.TopicId == t.TopicId && ((int)t.NetworkId == networkID) select d).Count().ToString(),
                           LatestPostMemberID = (from d in _context.TbDiscussionTopicPosts where d.TopicId == t.TopicId orderby d.PostDate descending select new { d.MemberId }).First().MemberId.ToString(),
                           LatestPostMemberName = (from d in _context.TbDiscussionTopicPosts join e in _context.TbMemberProfile on d.MemberId equals e.MemberId where d.TopicId == t.TopicId orderby d.PostDate descending select new { memberName = e.FirstName + " " + e.LastName }).First().memberName.ToString(),
                           LatestPostDate = (from d in _context.TbDiscussionTopicPosts where d.TopicId == t.TopicId orderby d.PostDate descending select new { d.PostDate }).First().PostDate.ToString()

                       }).ToList();
            return (lst);
        }


        /// <summary>
        /// Get topic posts.
        /// </summary>
        /// <param name="topicID"></param>
        /// <returns></returns>
        public List<NetworkPostsModel> GetTopicPosts(int topicID)
        {
            var lst = (from p in _context.TbDiscussionTopicPosts
                       join mpf in _context.TbMemberProfile on p.MemberId equals mpf.MemberId
                       where p.TopicId == topicID
                       orderby p.PostDate descending
                       select new NetworkPostsModel()
                       {
                           PostID = p.PostId.ToString(),
                           Description = p.PostDesc,
                           PostDate = p.PostDate.ToString(),
                           MemberID = p.MemberId.ToString(),
                           PicturePath = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,
                           MemberName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName
                       }).ToList();
            return (lst);
        }

        /// <summary>
        /// Update network Image
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="filename"></param>
        public void UpdateNetworkImage(int networkID, string filename)
        {
            var pNetID = new SqlParameter("@NetworkID", networkID);
            var pName = new SqlParameter("@FileName", filename);
            _context.Database.ExecuteSqlRaw("EXEC spUpdateNetworkImage @NetworkID, @FileName", pNetID, pName);
        }


        /// <summary>
        /// Get total network invites
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public int? GetTotalNetworkInvites(int memberID)
        {
            var lst = (from n in _context.TbNetworkMemberInvites where n.MemberId == memberID && n.Status == 0 select n).ToList();
            return (lst.Count);
        }

        /// <summary>
        /// Get list of member network invites
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<NetworkInfoModel> GetMemberNetworkInvites(int memberID)
        {
            var lst = (from n in _context.TbNetworks
                       join nm in _context.TbNetworkMembers on n.NetworkId equals nm.NetworkId
                       where nm.MemberId == memberID && nm.Status == 0
                       select new NetworkInfoModel()
                       {
                           NetworkID = n.NetworkId.ToString(),
                           NetworkName = n.NetworkName,
                           NetworkDesc = n.Description,
                           CategoryID = n.CategoryId.ToString(),
                           NetworkImage = string.IsNullOrEmpty(n.Image) ? "default.png" : n.Image,
                           MemberCount = (from t in _context.TbNetworkMembers where t.NetworkId == n.NetworkId select t).ToList().Count() + " Member(s)",
                           CreatedDate = n.CreatedDate.ToString(),
                       }
                      ).ToList();
            return lst;
        }

        /// <summary>
        /// Delete network topics
        /// </summary>
        /// <param name="topicID"></param>
        public void DeleteNetworkTopic(int topicID)
        {
            var pDelID = new SqlParameter("@TopicID", topicID);
            _context.Database.ExecuteSqlRaw("EXEC DeleteNetworkTopic @topicID", pDelID);
        }

        /// <summary>
        /// make a network member an admin
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="memberID"></param>
        public void AddNetworkAdmin(int networkID, int memberID)
        {
            var pNetID = new SqlParameter("@NetworkID", networkID);
            var pMembID = new SqlParameter("@MemberID", memberID);
            _context.Database.ExecuteSqlRaw("EXEC spAddNetworkAdmin @NetworkID, @MemberID", pNetID, pMembID);
        }

        /// <summary>
        /// Permanentlies the remove rejected request.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="networkID">Network identifier.</param>
        public void PermanentlyRemoveRejectedRequest(int memberID, int networkID)
        {
            var pNetID = new SqlParameter("@NetworkID", networkID);
            var pMembID = new SqlParameter("@MemberID", memberID);
            _context.Database.ExecuteSqlRaw("EXEC spRejectNetworkRequest @MemberID, @NetworkID", pMembID, pNetID);
        }


        public void DeactivateMemberFromNetwork(int memberID, int networkID)
        {
            var pNetID = new SqlParameter("@NetworkID", networkID);
            var pMembID = new SqlParameter("@MemberID", memberID);
            _context.Database.ExecuteSqlRaw("EXEC spDeactivateMemberFromNetwork @MemberID, @NetworkID", pMembID, pNetID);
        }

        #endregion

        /// <summary>
        /// Search top 8 of network search result
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public List<NetworkInfoModel> SearchTop8NetworkResults(string Value)
        {
            var lst = (from n in _context.TbNetworks
                       join c in _context.TbNetworkCategory on n.CategoryId equals c.CategoryId
                       join t in _context.TbNetworkCategoryType on n.CategoryTypeId equals t.CategoryTypeId
                       where n.NetworkName.Contains(Value)
                       orderby n.NetworkName descending
                       select new NetworkInfoModel()
                       {
                           NetworkID = n.NetworkId.ToString(),
                           NetworkName = n.NetworkName,
                           NetworkDesc = n.Description,
                           CategoryDesc = c.Description,
                           NetworkImage = string.IsNullOrEmpty(n.Image) ? "default.png" : n.Image,
                           CategoryTypeDesc = t.Description,

                       }).ToList();
            return (lst);
        }

        /// <summary>
        /// Gets the name of the networks by key.
        /// </summary>
        /// <returns>The networks by key name.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="Value">Value.</param>
        public List<NetworkInfoModel> GetNetworksByKeyName(int memberID, string Value)
        {
            var lst = (from n in _context.TbNetworks
                       where n.NetworkName.Contains(Value)

                       select new NetworkInfoModel()
                       {
                           NetworkID = n.NetworkId.ToString(),
                           NetworkName = n.NetworkName,
                           NetworkDesc = n.Description,
                           CategoryID = n.CategoryId.ToString(),
                           NetworkImage = string.IsNullOrEmpty(n.Image) ? "default.png" : n.Image,
                           MemberCount = (from t in _context.TbNetworkMembers where t.NetworkId == n.NetworkId select t).ToList().Count() + " Member(s)",
                           CreatedDate = n.CreatedDate.ToString(),
                       }).ToList();
            return (lst);
        }

        /// <summary>
        /// add a member who requested to join network
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="memberID"></param>
        /// <param name="status"></param>
        public void AddMemberToNetworkRequest(int networkID, int memberID, int status)
        {
            var pMemID = new SqlParameter("@MemberID", memberID);
            var pNetID = new SqlParameter("@NetworkID", networkID);
            var pStatus = new SqlParameter("@Email", status);
            _context.Database.ExecuteSqlRaw("EXEC spAddMemberToNetworkRequest @NetworkID, @MemberID, @Status", pNetID, pMemID, pStatus);
        }

        /// <summary>
        /// Deletes the invite.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="networkID">Network identifier.</param>
        /// <param name="email">Email.</param>
        public void DeleteInvite(int memberID, int networkID, string email)
        {
            var pMemID = new SqlParameter("@MemberID", memberID);
            var pNetID = new SqlParameter("@NetworkID", networkID);
            var pEmail = new SqlParameter("@Email", email);
            _context.Database.ExecuteSqlRaw("EXEC spDeleteInvite @MemberID, @NetworkID, @Email", pMemID, pNetID, pEmail);
        }

        /// <summary>
        /// Demotes the admin.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="networkID">Network identifier.</param>
        public void DemoteAdmin(int memberID, int networkID)
        {
            var member = (from n in _context.TbNetworkMembers where n.MemberId == memberID && n.NetworkId == networkID select n).First();
            if (member != null)
            {
                member.IsAdmin = false;
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the network invite.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="networkID">Network identifier.</param>
        public void DeleteNetworkInvite(string memberID, int networkID)
        {
            int res;
            if (int.TryParse(memberID.Trim(), out res))
            {
                var member = (from n in _context.TbNetworkMemberInvites where (int)n.MemberId == Convert.ToInt32(memberID) && (int)n.NetworkId == networkID select n).First();
                if (member != null)
                {
                    _context.Remove(member);
                    _context.SaveChanges();
                }
            }
            else
            {
                var member = (from n in _context.TbNetworkMemberInvites where n.Email == memberID && (int)n.NetworkId == networkID select n).First();
                if (member != null)
                {
                    _context.Remove(member);
                    _context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// get network topic member creator
        /// </summary>
        /// <param name="topicID"></param>
        /// <returns></returns>
        public int GetNetworkTopicMemberCreator(int topicID)
        {
            List<TbDiscussionTopicPosts> mbr = (from m in _context.TbDiscussionTopicPosts where m.TopicId == topicID orderby m.PostId ascending select m).ToList();
            if (mbr.Count == 0)
                return 0;
            else
                return (int)mbr[0].MemberId;
        }

        public void UpdateNetworkInfo(NetworkInfoModel body)
        {
            var net = (from n in _context.TbNetworks where n.NetworkId == Convert.ToInt32(body.NetworkID) select n).First();
            if (net != null)
            {
                net.NetworkName = body.NetworkName;
                net.Description = body.NetworkDesc;
                net.RecentNews = body.RecentNews;
                _context.SaveChanges();
            }

        }
    }

    public interface INetworkRepository
    {
        int CreateNetwork(int memberID, string networkName,
                                        string desc,
                                        int category,
                                        int type,
                                        string news,
                                        string office,
                                        string email,
                                        string webSite,
                                        string street,
                                        string city,
                                        string state,
                                        string zip);
        void UpdateNetwork(int networkID, string networkName,
                                        string desc,
                                        int category,
                                        int type,
                                        string news,
                                        string office,
                                        string email,
                                        string webSite,
                                        string street,
                                        string city,
                                        string state,
                                        string zip);
        List<NetworkModel> GetMemberNetworks(int memberID);
        List<TbNetworkCategory> GetNetworkCategories();
        List<TbNetworkCategoryType> GetNetworkCategoryTypes(int categoryID);
        List<NetworkInfoModel> GetNetworkBasicInfo(int networkID);
        List<NetworkInfoModel> GetNetworksByCatType(int memberID, int category, int subCategory);
        List<TbNetworkSettings> GetNetworkSettings(int networkID);
        List<NetworkMemberModel> GetNetworkAdmins(int networkID);
        List<NetworkMemberModel> GetNetworkMembers(int networkID, string listType);
        List<NetworkMemberModel> GetContactsNotInNetwork(int memberID, int networkID);
        void AddNetworkMembers(int memberID, int networkID);
        void IgnoreJoinNetwork(int memberID, int networkID);
        void UpdateNetworkSettings(int networkID, int access, bool nonAdminCanWrite,
                                                bool showNetworkEvents,
                                                bool enableDiscussionBoard,
                                                bool enablePhotos,
                                                bool enableVideos,
                                                bool enableLinks,
                                                bool onlyAllowAdminsToUploadPhotos,
                                                bool onlyAllowAdminsToUploadVideos,
                                                bool onlyAllowAdminsToPostLinks);
        void InviteMemberToJoinNetwork(int networkID, int memberID);
        int InviteEmailToJoinNetwork(int NetworkID, string email);
        bool IsNetworkMember(int NetworkID, string email);
        bool IsMemberNetWorkRequestor(int networkID, int memberID);
        List<RecentNetworkActivitiesResult> GetRecentNetworkActivities(int networkID);
        List<NetworkPostsModel> GetNetworkPosts(int networkID);
        List<NetworkChildPostsModel> GetNetworkPostResponses(int postID);
        void UpdateProfilePicture(int networkID, string fileName);
        List<NetworkPostsModel> GetTopicPosts(int topicID);
        void CreatePostComment(int memberID, int topicID, string postMsg);
        void CreatePostComment(int networkID, int memberID, int postID, string postMsg);
        List<NetworkInfoModel> GetNetworkInfo(int networkID);
        int CreatePhoto(int networkID, int memberID, string title, string desc, string ext);
        List<NetworkPhotosModel> GetNetworkPhotos(int networkID);
        void RemovePhoto(int photoID);
        void UpdateNetworkPhoto(int photoID, string title, string desc);
        List<NetworkEventModel> GetNetworkEvents(int networkID, int memberID);
        void CreateTopic(int networkID, int memberID, string topicName, string post);
        List<NetworkTopicsModel> GetNetworkDiscussionTopics(int networkID);
        void UpdateNetworkImage(int networkID, string filename);
        int? GetTotalNetworkInvites(int memberID);
        List<NetworkInfoModel> GetMemberNetworkInvites(int memberID);
        void DeleteNetworkTopic(int topicID);
        void AddNetworkAdmin(int networkID, int memberID);
        void PermanentlyRemoveRejectedRequest(int memberID, int networkID);
        void DeactivateMemberFromNetwork(int memberID, int networkID);
        List<NetworkInfoModel> SearchTop8NetworkResults(string Value);
        List<NetworkInfoModel> GetNetworksByKeyName(int memberID, string Value);
        void AddMemberToNetworkRequest(int networkID, int memberID, int status);
        void DeleteInvite(int memberID, int networkID, string email);
        void DemoteAdmin(int memberID, int networkID);
        void DeleteNetworkInvite(string memberID, int networkID);
        int GetNetworkTopicMemberCreator(int topicID);
        void CreateNetworkPost(int networkID, int memberID, string postMsg);
        void UpdateNetworkInfo(NetworkInfoModel body);
    }
}
