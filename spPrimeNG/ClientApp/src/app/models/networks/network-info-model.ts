export class NetworkInfoModel {
     networkID: string;
     networkName: string;
     networkDesc: string;
     categoryID: string;
     networkImage: string;
     memberCount: string;
     createdDate: string;
     networkOwner: string;
     categoryTypeID: string;
     recentNews: string;
     office: string;
     email: string;
     webSite: string;
     street: string;
     city: string;
     state: string;
     zip: string;
     inActive: string;
     categoryDesc: string;
     categoryTypeDesc: string;
     access: string;
     IsAlreadyMemberID: string;
}

export class NetworkPostsModel {
     postID: string;
     title: string;
     description: string;
     postDate: string;
     attachFile: string;
     memberID: string;
     picturePath: string;
     memberName: string;
     firstName: string;
     children: Array<NetworkPostChildModel>;
}

export class NetworkPostChildModel {
     postResponseID: string;
     postID: string;
     description: string;
     dateResponded: string;
     memberID: string;
     memberName: string;
     firstName: string;
     picturePath: string;
}

export class CategoryModel {
     categoryId: string;
     description: string;
}

export class CategoryTypeModel {
     categoryTypeId: string;
     description: string;
}

export class NetworkTopicsModel {
     topicID: string;
     topicDesc: string;
     memberName: string;
     createdDate: string;
     memberID: string;
     topicPostCnt: string;
     latestPostMemberID: string;
     latestPostMemberName: string;
     latestPostDate: string;
}

export class NetworkMemberModel {
     memberID: string;
     memberName: string;
     picturePath: string;
     networkID: string;
     IsOwner: string;
     IsAdmin: string;
     joinedDate: string;
     access: string;
     titleDesc: string;
     inviteID: string;
}

export class NetworkSettingsModel {
     networkId: string;
     nonAdminCanWrite: string;
     showGroupEvents: string;
     showProfileBox: string;
     showProfileTab: string;
     enableDiscussionBoard: string;
     enablePhotos: string;
     onlyAllowAdminsToUploadPhotos: string;
     showEnablePhotoProfileBox: string;
     showEnableProfileTab: string;
     enableVideos: string;
     onlyAllowAdminsToUploadVideos: string;
     showVideoProfileBox: string;
     showVideoProfileTab: string;
     enableLinks: string;
     onlyAllowAdminsToPostLinks: string;
     access: string;
}

