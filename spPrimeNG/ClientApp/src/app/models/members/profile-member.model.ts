export class MemberProfileBasicInfoModel {
    picturePath: string;
    memProfileName: string;
    titleDesc: string;
    memProfileStatus: string;
    memProfileGender: string;
    memProfileDOB: string;
    interestedDesc: string;
    memProfileLookingFor: string;
    currentCity: string;
    currentStatus: string;
    dobDay: string;
    dobMonth: string;
    dobYear: string;
    firstName: string;
    homeNeighborhood: string;
    hometown: string;
    interestedInType: string;
    joinedDate: string;
    lastName: string;
    lookingForEmployment: boolean;
    lookingForNetworking: boolean;
    lookingForPartnership: boolean;
    lookingForRecruitment: boolean;
    memberID: string;
    middleName: string;
    politicalView: string;
    religiousView: string;
    sex: string;
    showDOBType: string;
    showSexInProfile: string;
    getLGEntitiesCount: string;
    sport: string;
    bio: string;
    height: string;
    weight: string;
    leftRightHandFoot: string;
    preferredPosition: string;
    secondaryPosition: string;
}

export class MemberProfileBasicInfoSaveModel {
    PicturePath: string;
    memProfileName: string;
    TitleDesc: string;
    Sector: string;
    Industry: string;
    memProfileStatus: string;
    memProfileGender: string;
    memProfileDOB: string;
    InterestedDesc: string;
    memProfileLookingFor: string;
    CurrentCity: string;
    CurrentStatus: string;
    DOBDay: string;
    DOBMonth: string;
    DOBYear: string;
    FirstName: string;
    HomeNeighborhood: string;
    Hometown: string;
    InterestedInType: string;
    JoinedDate: string;
    LastName: string;
    LookingForEmployment: string;
    LookingForNetworking: string;
    LookingForPartnership: string;
    LookingForRecruitment: string;
    MemberID: string;
    MiddleName: string;
    PoliticalView: string;
    ReligiousView: string;
    Sex: string;
    ShowDOBType: string;
    ShowSexInProfile: string;
    GetLGEntitiesCount: string;
    industriesList: string[];
}

export class MemberProfileAboutInfoModel {
    aboutMe: string;
    activities: string;
    hobbies: string;
    specialSkills: string;
}

export class YoutubePlayListModel {
    Etag: string;
    Id: string;
    Title: string;
    Description: string;
    DefaultThumbnail: string;
    DefaultThumbnailHeight: string;
    DefaultThumbnailWidth: string;
}

export class YoutubeVideosListModel {
    Etag: string;
    Id: string;
    Title: string;
    Description: string;
    DefaultThumbnail: string;
    DefaultThumbnailHeight: string;
    DefaultThumbnailWidth: string;
    PublishedAt: string;
}

export class SportsListModel {
    name: string;
    id: string;
    desscription: string;
}

export class InstagramUserModel {
    user_id: string;
    access_token: string;
}


