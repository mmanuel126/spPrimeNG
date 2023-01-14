using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbMembers
    {
        public TbMembers()
        {
            TbAlbums = new HashSet<TbAlbums>();
            TbChatMessagesToUser = new HashSet<TbChatMessages>();
            TbChatMessagesUser = new HashSet<TbChatMessages>();
            TbContactRequestsFromMember = new HashSet<TbContactRequests>();
            TbContactRequestsToMember = new HashSet<TbContactRequests>();
            TbContactsContact = new HashSet<TbContacts>();
            TbContactsMember = new HashSet<TbContacts>();
            TbDiscussionTopicPosts = new HashSet<TbDiscussionTopicPosts>();
            TbEventInvites = new HashSet<TbEventInvites>();
            TbEvents = new HashSet<TbEvents>();
            TbLinks = new HashSet<TbLinks>();
            TbLoggedInUser = new HashSet<TbLoggedInUser>();
            TbMemberPostResponses = new HashSet<TbMemberPostResponses>();
            TbMemberPosts = new HashSet<TbMemberPosts>();
            TbMembersPrivacySettings = new HashSet<TbMembersPrivacySettings>();
            TbMembersRecentActivities = new HashSet<TbMembersRecentActivities>();
            TbMembersRegistered = new HashSet<TbMembersRegistered>();
            TbMessagesContact = new HashSet<TbMessages>();
            TbMessagesDeletedContact = new HashSet<TbMessagesDeleted>();
            TbMessagesDeletedSender = new HashSet<TbMessagesDeleted>();
            TbMessagesJunkContact = new HashSet<TbMessagesJunk>();
            TbMessagesJunkSender = new HashSet<TbMessagesJunk>();
            TbMessagesRepliesContact = new HashSet<TbMessagesReplies>();
            TbMessagesRepliesSender = new HashSet<TbMessagesReplies>();
            TbMessagesSender = new HashSet<TbMessages>();
            TbMessagesSentContact = new HashSet<TbMessagesSent>();
            TbMessagesSentSender = new HashSet<TbMessagesSent>();
            TbNetworkDiscussionTopics = new HashSet<TbNetworkDiscussionTopics>();
            TbNetworkMemberInvites = new HashSet<TbNetworkMemberInvites>();
            TbNetworkMembers = new HashSet<TbNetworkMembers>();
            TbNetworkPhotos = new HashSet<TbNetworkPhotos>();
            TbNetworkPostResponses = new HashSet<TbNetworkPostResponses>();
            TbNetworkPosts = new HashSet<TbNetworkPosts>();
            TbNetworkRecentActivities = new HashSet<TbNetworkRecentActivities>();
            TbNetworkRequests = new HashSet<TbNetworkRequests>();
            TbNetworkVideos = new HashSet<TbNetworkVideos>();
            TbNotificationSettings = new HashSet<TbNotificationSettings>();
            TbSuggestions = new HashSet<TbSuggestions>();
            TbVideos = new HashSet<TbVideos>();
        }

        public int MemberId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Status { get; set; }
        public int? SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public int? DeactivateReason { get; set; }
        public string DeactivateExplanation { get; set; }
        public bool? FutureEmails { get; set; }
        public int? ChatStatus { get; set; }
        public bool? LogOn { get; set; }
        public string YoutubeChannel { get; set; }

        public virtual TbMemberProfile TbMemberProfile { get; set; }
        public virtual TbMemberProfileContactInfo TbMemberProfileContactInfo { get; set; }
        public virtual TbMemberProfileEducation TbMemberProfileEducation { get; set; }
        public virtual TbMemberProfileEmploymentInfo TbMemberProfileEmploymentInfo { get; set; }
        public virtual TbMemberProfilePersonalInfo TbMemberProfilePersonalInfo { get; set; }
        public virtual ICollection<TbAlbums> TbAlbums { get; set; }
        public virtual ICollection<TbChatMessages> TbChatMessagesToUser { get; set; }
        public virtual ICollection<TbChatMessages> TbChatMessagesUser { get; set; }
        public virtual ICollection<TbContactRequests> TbContactRequestsFromMember { get; set; }
        public virtual ICollection<TbContactRequests> TbContactRequestsToMember { get; set; }
        public virtual ICollection<TbContacts> TbContactsContact { get; set; }
        public virtual ICollection<TbContacts> TbContactsMember { get; set; }
        public virtual ICollection<TbDiscussionTopicPosts> TbDiscussionTopicPosts { get; set; }
        public virtual ICollection<TbEventInvites> TbEventInvites { get; set; }
        public virtual ICollection<TbEvents> TbEvents { get; set; }
        public virtual ICollection<TbLinks> TbLinks { get; set; }
        public virtual ICollection<TbLoggedInUser> TbLoggedInUser { get; set; }
        public virtual ICollection<TbMemberPostResponses> TbMemberPostResponses { get; set; }
        public virtual ICollection<TbMemberPosts> TbMemberPosts { get; set; }
        public virtual ICollection<TbMembersPrivacySettings> TbMembersPrivacySettings { get; set; }
        public virtual ICollection<TbMembersRecentActivities> TbMembersRecentActivities { get; set; }
        public virtual ICollection<TbMembersRegistered> TbMembersRegistered { get; set; }
        public virtual ICollection<TbMessages> TbMessagesContact { get; set; }
        public virtual ICollection<TbMessagesDeleted> TbMessagesDeletedContact { get; set; }
        public virtual ICollection<TbMessagesDeleted> TbMessagesDeletedSender { get; set; }
        public virtual ICollection<TbMessagesJunk> TbMessagesJunkContact { get; set; }
        public virtual ICollection<TbMessagesJunk> TbMessagesJunkSender { get; set; }
        public virtual ICollection<TbMessagesReplies> TbMessagesRepliesContact { get; set; }
        public virtual ICollection<TbMessagesReplies> TbMessagesRepliesSender { get; set; }
        public virtual ICollection<TbMessages> TbMessagesSender { get; set; }
        public virtual ICollection<TbMessagesSent> TbMessagesSentContact { get; set; }
        public virtual ICollection<TbMessagesSent> TbMessagesSentSender { get; set; }
        public virtual ICollection<TbNetworkDiscussionTopics> TbNetworkDiscussionTopics { get; set; }
        public virtual ICollection<TbNetworkMemberInvites> TbNetworkMemberInvites { get; set; }
        public virtual ICollection<TbNetworkMembers> TbNetworkMembers { get; set; }
        public virtual ICollection<TbNetworkPhotos> TbNetworkPhotos { get; set; }
        public virtual ICollection<TbNetworkPostResponses> TbNetworkPostResponses { get; set; }
        public virtual ICollection<TbNetworkPosts> TbNetworkPosts { get; set; }
        public virtual ICollection<TbNetworkRecentActivities> TbNetworkRecentActivities { get; set; }
        public virtual ICollection<TbNetworkRequests> TbNetworkRequests { get; set; }
        public virtual ICollection<TbNetworkVideos> TbNetworkVideos { get; set; }
        public virtual ICollection<TbNotificationSettings> TbNotificationSettings { get; set; }
        public virtual ICollection<TbSuggestions> TbSuggestions { get; set; }
        public virtual ICollection<TbVideos> TbVideos { get; set; }
    }
}
