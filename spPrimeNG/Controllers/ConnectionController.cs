using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using sportprofiles.Models.Connections;
using sportprofiles.Repository;
using Swashbuckle.AspNetCore.Annotations;

namespace sportprofiles.Controllers
{
    [Route("services/[controller]/[action]")]
    [SwaggerTag("Contains API functionalities to manage and control member connections.")]
    public class ConnectionController : Controller
    {
        readonly IConnectionRepository _connectionRepo;

        public ConnectionController(IConnectionRepository contactRepository)
        {
            _connectionRepo = contactRepository;
        }

        /// <summary>
        /// Gets a list of member contacts by the given member ID and show type.
        /// </summary>
        /// <returns>The member contacts.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="show">Show could be "requests" or "RecentlyAdded".</param>
        [HttpGet]
        [Authorize]
        public List<MemberConnectionModel> GetMemberConnections([FromQuery] int memberID, [FromQuery] string show)
        {
            List<MemberConnectionModel> lst = _connectionRepo.GetMemberConnections(memberID, show).ToList();
            return lst;
        }


        /// <summary>
        /// Gets the list of member contact suggestions for a member ID.
        /// </summary>
        /// <returns>The member contact suggestions.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        public List<MemberConnectionModel> GetMemberConnectionSuggestions([FromQuery] int memberID)
        {
            List<MemberConnectionModel> lst = _connectionRepo.GetMemberConnectionSuggestions(memberID).ToList();
            return lst;
        }

        /// <summary>
        /// Sends the request to contact.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpPut]
        [Authorize]
        public void SendRequestConnection([FromQuery] string memberID, [FromQuery] string contactID)
        {
            _connectionRepo.SendRequestConnection(memberID, contactID);
        }


        /// <summary>
        /// Searchs and return list of contacts for a given member ID and search Text.
        /// </summary>
        /// <returns>The member contacts.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchText">Search text.</param>
        [HttpGet]
        [Authorize]
        public List<MemberConnectionModel> SearchMemberConnections([FromQuery] int memberID, [FromQuery] string searchText)
        {
            List<MemberConnectionModel> lst = _connectionRepo.SearchMemberConnections(memberID, searchText);
            return lst;
        }

        /// <summary>
        /// Gets the member network invite contact list.
        /// </summary>
        /// <returns>The member network invite contact list.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="networkID">Network identifier.</param>
        [HttpGet]
        [Authorize]
        public List<MemberConnectionModel> GetMemberNetworkInviteConnectionList([FromQuery] int memberID, [FromQuery] int networkID)
        {
            List<MemberConnectionModel> lst = _connectionRepo.GetMemberNetworkInviteConnectionList(memberID, networkID).ToList();
            return lst;
        }

        /// <summary>
        /// Gets the requests list.
        /// </summary>
        /// <returns>The requests list.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        public List<MemberConnectionModel> GetRequestsList([FromQuery] int memberID)
        {
            List<MemberConnectionModel> lst = _connectionRepo.GetRequestsList(memberID).ToList();
            return lst;
        }

        /// <summary>
        /// Gets members list by search type.
        /// </summary>
        /// <returns>The members by search type.</returns>        
        /// <param name="userID">User identifier.</param>
        /// <param name="searchType">Search type.</param>
        /// <param name="searchText">Search text.</param>
        [HttpGet]
        [Authorize]
        public List<MemberConnectionModel> GetMembersBySearchType([FromQuery] int userID, [FromQuery] string searchType, [FromQuery] string searchText)
        {
            List<MemberConnectionModel> lst = _connectionRepo.GetMembersBySearchType(userID, searchType, searchText).ToList();
            return lst;
        }

        /// <summary>
        /// Gets the search contacts.
        /// </summary>
        /// <returns>The search contacts.</returns>          
        /// <param name="userID">User identifier.</param>
        /// <param name="searchText">Search text.</param>
        [HttpGet]
        [Authorize]
        public List<MemberConnectionModel> GetSearchConnections([FromQuery] int userID, [FromQuery] string searchText)
        {
            List<MemberConnectionModel> lst = _connectionRepo.GetSearchConnections(userID, searchText).ToList();
            return lst;
        }

        /// <summary>
        /// Gets the member phone book.
        /// </summary>
        /// <returns>The member phone book.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        public List<MemberPhoneBookModel> GetMemberPhoneBook([FromQuery] int memberID)
        {
            List<MemberPhoneBookModel> lst = _connectionRepo.GetMemberPhoneBook(memberID).ToList();
            return lst;
        }

        /// <summary>
        /// Gets the member browsed contacts.
        /// </summary>
        /// <returns>The member browsed contacts.</returns> 
        /// <param name="memberID">Member identifier.</param>
        /// <param name="categoryID">Category identifier.</param>
        /// <param name="subCategory">Sub category.</param>
        [HttpGet]
        [Authorize]
        public List<MemberConnectionModel> GetMemberBrowsedContacts([FromQuery] int memberID, [FromQuery] int categoryID, [FromQuery] string subCategory)
        {
            List<MemberConnectionModel> lst = _connectionRepo.GetMemberBrowsedConnections(memberID, categoryID, subCategory).ToList();
            return lst;
        }

        /// <summary>
        /// Searchs members by a gven type and member ID.
        /// </summary>
        /// <returns>The member by type.</returns>      
        /// <param name="userID">User identifier.</param>
        /// <param name="searchType">Search type.</param>
        /// <param name="searchString">Search string.</param>
        [HttpGet]
        [Authorize]
        public List<MemberByTypeModel> SearchMemberByType([FromQuery] int userID, [FromQuery] int searchType, [FromQuery] string searchString)
        {
            List<MemberByTypeModel> lst = _connectionRepo.SearchMemberByType(userID, searchType, searchString).ToList();
            return lst;
        }

        /// <summary>
        /// member accepts request from contact 
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpPut]
        [Authorize]
        public void AcceptRequest([FromQuery] int memberID, [FromQuery] int contactID)
        {
            _connectionRepo.AcceptRequest(memberID, contactID);
        }

        /// <summary>
        /// Member rejects the request from contact.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpPut]
        [Authorize]
        public void RejectRequest([FromQuery] int memberID, [FromQuery] int contactID)
        {
            _connectionRepo.RejectRequest(memberID, contactID);
        }

        /// <summary>
        /// Deletes the contact.
        /// </summary>  
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpDelete]
        [Authorize]
        public void DeleteConnection([FromQuery] int memberID, [FromQuery] int contactID)
        {
            _connectionRepo.DeleteConnection(memberID, contactID);
        }

        /// <summary>
        /// Gets the entity list by search type.
        /// </summary>
        /// <returns>The entity by search type.</returns>   
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchText">Search text.</param>
        /// <param name="searchType">Search type.</param>
        [HttpGet]
        [Authorize]
        public List<EntityModel> GetEntityBySearchType([FromQuery] int memberID, [FromQuery] string searchText, [FromQuery] string searchType)
        {
            List<EntityModel> lst = _connectionRepo.GetEntityBySearchType(memberID, searchText, searchType).ToList();
            return lst;
        }

        /// <summary>
        /// Get the searched contacts by search criteria member id and search text.
        /// </summary>
        /// <returns>The search results.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchText">Search text.</param>
        [HttpGet]
        [Authorize]
        public List<ConnectionSearchModel> ConnectionSearchResults([FromQuery] int memberID, [FromQuery] string searchText)
        {
            List<ConnectionSearchModel> lst = _connectionRepo.ContactSearchResults(memberID, searchText).ToList();
            return lst;
        }

        /// <summary>
        /// /gets lsit of people by member id and searched text.
        /// </summary>
        /// <returns>The search results.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchText">Search text.</param>
        [HttpGet]
        [Authorize]
        public List<ConnectionSearchModel> PeopleSearchResults([FromQuery] int memberID, [FromQuery] string searchText)
        {
            List<ConnectionSearchModel> lst = _connectionRepo.PeopleSearchResults(memberID, searchText).ToList();
            return lst;
        }

        /// <summary>
        /// Search list of Networks by member ID and search text.
        /// </summary>
        /// <returns>The search results.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchText">Search text.</param>
        [HttpGet]
        [Authorize]
        public List<NetworkSearchModel> NetworkSearchResults([FromQuery] int memberID, [FromQuery] string searchText)
        {
            List<NetworkSearchModel> lst = _connectionRepo.NetworkSearchResults(memberID, searchText).ToList();
            return lst;
        }

        /// <summary>
        /// returns list of events by member ID and search text.
        /// </summary>
        /// <returns>The search results.</returns> 
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchText">Search text.</param>
        [HttpGet]
        [Authorize]
        public List<EventSearchModel> EventSearchResults([FromQuery]int memberID, [FromQuery] string searchText)
        {
            List<EventSearchModel> lst = _connectionRepo.EventSearchResults(memberID, searchText).ToList();
            return lst;
        }

        /// <summary>
        /// reuturns the list of Contacts by search text.
        /// </summary>
        /// <returns>The results.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchText">Search text.</param>
        [HttpGet]
        [Authorize]
        public List<SearchModel> SearchResults([FromQuery] int memberID, [FromQuery] string searchText)
        {
            List<SearchModel> lst = _connectionRepo.SearchResults(memberID, searchText).ToList();
            return lst;
        }

        /// <summary>
        /// reuturns the list of connections by search text.
        /// </summary>
        /// <returns>The results.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchText">Search text.</param>
        [HttpGet]
        [Authorize]
        public List<SearchModel> SearchAllResults([FromQuery] int memberID, [FromQuery] string searchText)
        {
            List<SearchModel> lst = _connectionRepo.SearchAllResults(memberID, searchText).ToList();
            return lst;
        }

        /// <summary>
        /// reuturns the list of members I am following
        /// </summary>
        /// <returns>The results.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        public List<MemberConnectionModel> GetPeopleIFollow([FromQuery] int memberID)
        {
            List<MemberConnectionModel> lst = _connectionRepo.GetPeopleIFollow(memberID).ToList();
            return lst;
        }

        /// <summary>
        /// reuturns the list of whose following me.
        /// </summary>
        /// <returns>The results.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        public List<MemberConnectionModel> GetWhosFollowingMe([FromQuery] int memberID)
        {
            List<MemberConnectionModel> lst = _connectionRepo.GetWhosFollowingMe(memberID).ToList();
            return lst;
        }

        /// <summary>
        /// follow member
        /// </summary>  
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpPost]
        [Authorize]
        public void followMember([FromQuery] int memberID, [FromQuery] int contactID)
        {
            _connectionRepo.FollowMember(memberID, contactID);
        }

        /// <summary>
        /// Unfollow member
        /// </summary>  
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpPost]
        [Authorize]
        public void UnfollowMember([FromQuery] int memberID, [FromQuery] int contactID)
        {
            _connectionRepo.UnfollowMember(memberID, contactID);
        }

        /// <summary>
        /// check tos if member is following connection
        /// </summary>
        /// <returns><c>true</c>, if mem is following, <c>false</c> otherwise.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpGet]
        [Authorize]
        public bool IsFollowingConnection([FromQuery] int memberID, [FromQuery] int contactID)
        {
            bool b = _connectionRepo.IsFollowingConnection(memberID, contactID);
            return b;
        }

    }
}
