using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbNetworks
    {
        public TbNetworks()
        {
            TbEvents = new HashSet<TbEvents>();
            TbNetworkDiscussionTopics = new HashSet<TbNetworkDiscussionTopics>();
            TbNetworkMemberInvites = new HashSet<TbNetworkMemberInvites>();
            TbNetworkMembers = new HashSet<TbNetworkMembers>();
            TbNetworkPhotos = new HashSet<TbNetworkPhotos>();
            TbNetworkPostResponses = new HashSet<TbNetworkPostResponses>();
            TbNetworkPosts = new HashSet<TbNetworkPosts>();
            TbNetworkRecentActivities = new HashSet<TbNetworkRecentActivities>();
            TbNetworkRequests = new HashSet<TbNetworkRequests>();
            TbNetworkVideos = new HashSet<TbNetworkVideos>();
        }

        public int NetworkId { get; set; }
        public string NetworkName { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public int? CategoryTypeId { get; set; }
        public string RecentNews { get; set; }
        public string Office { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool? InActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Image { get; set; }

        public virtual TbNetworkCategory Category { get; set; }
        public virtual TbNetworkCategoryType CategoryType { get; set; }
        public virtual TbNetworkSettings TbNetworkSettings { get; set; }
        public virtual ICollection<TbEvents> TbEvents { get; set; }
        public virtual ICollection<TbNetworkDiscussionTopics> TbNetworkDiscussionTopics { get; set; }
        public virtual ICollection<TbNetworkMemberInvites> TbNetworkMemberInvites { get; set; }
        public virtual ICollection<TbNetworkMembers> TbNetworkMembers { get; set; }
        public virtual ICollection<TbNetworkPhotos> TbNetworkPhotos { get; set; }
        public virtual ICollection<TbNetworkPostResponses> TbNetworkPostResponses { get; set; }
        public virtual ICollection<TbNetworkPosts> TbNetworkPosts { get; set; }
        public virtual ICollection<TbNetworkRecentActivities> TbNetworkRecentActivities { get; set; }
        public virtual ICollection<TbNetworkRequests> TbNetworkRequests { get; set; }
        public virtual ICollection<TbNetworkVideos> TbNetworkVideos { get; set; }
    }
}
