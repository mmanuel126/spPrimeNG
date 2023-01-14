using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using sportprofiles.Repository;
using sportprofiles.DBContextModels;
using Swashbuckle.AspNetCore.Annotations;
using sportprofiles.Models.Messages;

namespace sportprofiles.Controllers
{
    [Route("services/[controller]")]
    [SwaggerTag("contains API functionalities for messaging or communication between members.")]
    public class MessageController : Controller
    {
        readonly IMessageRepository _msgRepo;

        public MessageController(IMessageRepository msgRepo)
        {
            _msgRepo = msgRepo;
        }

        /// <summary>
        /// Gets the member notifications.
        /// </summary>
        /// <returns>The member notifications.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="showType">Show type.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberNotifications/{memberID}")]
        public List<TbNotifications> GetMemberNotifications([FromRoute] int memberID, [FromQuery] string showType)
        {
            List<TbNotifications> lst = _msgRepo.GetMemberNotifications(memberID, showType);
            return lst;
        }

        /// <summary>
        /// Gets the member messages.
        /// </summary>
        /// <returns>The member messages.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="type">Type.</param>
        /// <param name="showType">Show type.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMemberMessages/{memberID}")]
        public List<SearchMessagesModel> GetMemberMessages([FromRoute] int memberID, [FromQuery] string type, [FromQuery]  string showType)
        {
            var mlist = _msgRepo.GetMemberMessages(memberID, type, showType);
            return mlist;
        }

        /// <summary>
        /// Gets the total unread messages.
        /// </summary>
        /// <returns>The total unread messages.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("GetTotalUnreadMessages/{memberID}")]
        public int GetTotalUnreadMessages([FromRoute] int memberID)
        {
            int res = _msgRepo.GetTotalUnreadMessages(memberID);
            return (res);
        }

        /// <summary>
        /// Creates a message.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="body">Body.</param>
        /// <param name="attachment">Attachment.</param>
        /// <param name="original">Original.</param>
        [HttpPost]
        [Authorize]
        [Route("CreateMessage")]
        public void CreateMessage([FromQuery]  int to, [FromQuery]  int from, [FromQuery] string subject, [FromQuery] string body, [FromQuery] string attachment, [FromQuery]  string original)
        {
            _msgRepo.CreateMessage(to, from, subject, body, attachment, original);
        }

        /// <summary>
        /// Creates the message by source.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="body">Body.</param>
        /// <param name="attachment">Attachment.</param>
        /// <param name="source">Source.</param>
        /// <param name="original">Original.</param>
        [HttpPost]
        [Authorize]
        [Route("CreateMessageBySource")]
        public void CreateMessageBySource([FromQuery] string to, [FromQuery] string from, [FromQuery] string subject, [FromQuery] string body, [FromQuery] string attachment, [FromQuery] string source, [FromQuery]  string original)
        {
            _msgRepo.CreateMessage(to, from, subject, body, attachment, source, original);
        }

        /// <summary>
        /// Toggles the state of the notification.
        /// </summary>
        /// <param name="status">If set to <c>true</c> status.</param>
        /// <param name="notificationID">Notification identifier.</param>
        [HttpPut]
        [Authorize]
        [Route("ToggleNotificationState")]
        public void ToggleNotificationState([FromQuery] bool status, [FromQuery] int notificationID)
        {
            _msgRepo.ToggleNotificationState(status, notificationID);
        }

        /// <summary>
        /// Deletes the notification.
        /// </summary>
        /// <param name="notificationID">Notification identifier.</param>
        /// <param name="memberID">Member identifier.</param>
        [HttpDelete]
        [Authorize]
        [Route("DeleteNotification/{notificationID}/{memberID}")]
        public void DeleteNotification([FromRoute] int notificationID, [FromRoute] int memberID)
        {
            _msgRepo.DeleteNotification(notificationID, memberID);
        }

        /// <summary>
        /// Toggles the state of the message.
        /// </summary>
        /// <param name="status">Status.</param>
        /// <param name="msgID">Message identifier.</param>
        /// <param name="folder">Folder.</param>
        [HttpPut]
        [Route("ToggleMessageState")]
        public void ToggleMessageState([FromQuery] int status, [FromQuery]  int msgID, [FromQuery]  string folder)
        {
            _msgRepo.ToggleMessageState(status, msgID, folder);
        }

        /// <summary>
        /// Gets list of notifications by identifier.
        /// </summary>
        /// <returns>The notification by identifier.</returns>
        /// <param name="nid">Nid.</param>
        [HttpGet]
        [Authorize]
        [Route("GetNotificationByID/{nid}")]
        public IQueryable<TbNotifications> GetNotificationByID([FromRoute] int nid)
        {
            IQueryable<TbNotifications> lst = _msgRepo.GetNotificationByID(nid);
            return lst;
        }

        /// <summary>
        /// Gets the message info by identifier.
        /// </summary>
        /// <returns>The message info by identifier.</returns>
        /// <param name="msgID">Message identifier.</param>
        /// <param name="folder">Folder.</param>
        [HttpGet]
        [Authorize]
        [Route("GetMessageInfoByID/{msgID}")]
        public List<MessageInfoModel> GetMessageInfoByID([FromRoute] int msgID, [FromQuery] string folder)
        {
            List<MessageInfoModel> lst = _msgRepo.GetMessageInfoByID(msgID, folder);
            return lst;
        }

        /// <summary>
        /// Deletes the move message.
        /// </summary>
        /// <param name="msgID">Message identifier.</param>
        /// <param name="mt">Mt.</param>
        /// <param name="folder">Folder.</param>
        [HttpDelete]
        [Authorize]
        [Route("DeleteMoveMessage/{msgID}")]
        public void DeleteMoveMessage([FromRoute] int msgID, [FromQuery]  int moveType, [FromQuery] string folder)
        {
            _msgRepo.DeleteMoveMessage(msgID, moveType, folder);
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="msgID">Message identifier.</param>
        [HttpDelete]
        [Authorize]
        [Route("DeleteMessage/{msgID}")]
        public void DeleteMessage([FromRoute] int msgID)
        {
            _msgRepo.DeleteMessage(msgID);
        }

        /// <summary>
        /// Searchs messages given search key for member id.
        /// </summary>
        /// <returns>The messages.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchKey">Search key.</param>
        /// <param name="type">Type.</param>
        [HttpGet]
        [Authorize]
        [Route("SearchMessages/{memberID}")]
        public List<SearchMessagesModel> SearchMessages([FromRoute]  int memberID, [FromQuery]  string searchKey, [FromQuery]  string type)
        {
            List<SearchMessagesModel> msgList = _msgRepo.SearchMessages(memberID, searchKey, type);
            return msgList;
        }

        /// <summary>
        /// Empties the message folder (inbox, archive, etc).
        /// </summary>
        /// <param name="mID">M identifier.</param>
        /// <param name="folder">Folder.</param>
        [HttpDelete]
        [Authorize]
        [Route("EmptyMessageFolders/{mID}")]
        public void EmptyMessageFolders([FromRoute] int mID, [FromQuery]  string folder)
        {
            _msgRepo.EmptyMessageFolders(mID, folder);
        }

    }
}
