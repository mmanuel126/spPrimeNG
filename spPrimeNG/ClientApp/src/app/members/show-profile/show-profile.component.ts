import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from "@angular/router";
import { MembersService } from '../../services/data/members.service';
import { ConnectionsService } from '../../services/data/connections.service';
import { SessionMgtService } from '../../services/session-mgt.service';

import { InstagramUserModel, MemberProfileBasicInfoModel, YoutubePlayListModel, YoutubeVideosListModel } from '../../models/members/profile-member.model';
import { MemberProfileEducationModel } from '../../models/members/profile-education.model';
import { MemberProfileEmploymentModel } from '../../models/members/profile-employment.model';
import { MemberProfileContactInfoModel } from '../../models/members/profile-contact-info.model';
import { MemberProfileAboutModel } from '../../models/members/profile-about.model';

import { ContactModel } from '../../models/contacts/contact-model';
import { NetworkInfoModel } from '../../models/networks/network-info-model';
import { MemberEventsModel } from 'src/app/models/events/member-events-model';

import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ICommonService } from '../../services/common.service';
import { INetworksService } from '../../services/data/networks.service';
import { IEventsService } from '../../services/data/events.service';
import { ISettingsService } from 'src/app/services/data/settings.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-show-profile',
  templateUrl: './show-profile.component.html',
  styleUrls: ['./show-profile.component.css'],
})
export class ShowProfileComponent implements OnInit {

  code: string = "";
  spinner: boolean;
  cStatus: string = "";
  instagramUser: InstagramUserModel;

  //basic info variables  
  memberID: string;
  memImage: string;
  memName: string;
  memTitle: string;
  sport: string;
  bio: string = "";
  height: string;
  weight: string;
  memGender: string;
  memBirthDate: string;
  memLookingFor: string;
  LeftRightHandFoot: string;
  PreferredPosition: string;
  SecondaryPosition: string;
  showDOB: string;
  showSex: string;

  showAddress: boolean = true;
  showCellPhone: boolean = true;
  showEmail: boolean = true;
  showHomePhone: boolean = true;
  showLinks: boolean = true;

  showBasicInfo: string = "1";
  showPersonalInfo: string = "1";
  showContacts: string = "1";
  showEducation: string = "1";
  showWorkInfo: string = "1";
  contactIsAFriend: boolean;
  isSameUser: boolean;

  //member contact info vars
  memEmail: string;
  memOtherEmail: string;
  memIMname: string;
  memWebSite: string;
  memCellPhone: string;
  memOtherPhone: string;
  memAddress: string;
  memCity: string;
  memNeighborhood: string;
  memState: string;
  memZip: string;

  about: string;
  interests: string;
  activities: string;
  specialSkills: string;

  basicInfoModel: MemberProfileBasicInfoModel = new MemberProfileBasicInfoModel();
  contactInfoModel: MemberProfileContactInfoModel = new MemberProfileContactInfoModel();
  educationInfoModel: MemberProfileEducationModel[];
  employmentInfoModel: MemberProfileEmploymentModel[];
  myContactsList: ContactModel[];
  myNetworksList: NetworkInfoModel[];
  myEventsList: MemberEventsModel[];
  aboutInfoModel: MemberProfileAboutModel[];
  playList: YoutubePlayListModel[];
  videosList: YoutubeVideosListModel[];

  eduCount: number;
  empCount: number;
  contactCount: number;
  networkCount: number;
  eventCount: number;
  vidCount: number;
  videoId: string;

  contactName: string;

  showAddAsContact: boolean = false;
  showFollowMember: boolean =false;

 // private tabs: NgbTabset;
  public activeId: string;

  modHand: NgbModalRef;
  mySubscription: any;

  constructor(public session: SessionMgtService, private route: ActivatedRoute, private router: Router, public membersSvc: MembersService,
    public contactsSvc: ConnectionsService, public networksSvc: INetworksService, public eventsSvc: IEventsService,
    public ngbMod: NgbModal, public common: ICommonService, public settingsSvc: ISettingsService) {

    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    };

    this.mySubscription = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        // Trick the Router into believing it's last link wasn't previously loaded
        this.router.navigated = false;
      }
    });
  }

  ngOnInit(): void {
    const tag = document.createElement('script');
    tag.src = 'https://www.youtube.com/iframe_api';
    document.body.appendChild(tag);
    this.memberID = this.route.snapshot.queryParamMap.get('memberID');
    this.code = this.route.snapshot.queryParamMap.get('code');
    this.getPrivacySettings();
    this.checkIfMemberIsAFriend(this.memberID);
    this.checkIfSameUser(this.memberID);
    this.getBasicInfo();
    this.getContactInfo();
    this.checkIfToShowAsContact(this.memberID);
    this.checkIfToShowFollowMember(this.memberID);
    this.getEducationInfo();
    this.getVideoPlayList();
  }

  ngOnDestroy() {
    if (this.mySubscription) {
      this.mySubscription.unsubscribe();
    }
  }

  async checkIfToShowAsContact(memberID: string) {
    let loggedUserID = this.session.getSessionVal('userID');
    let isFriend = await this.membersSvc.IsFriendByContactID(loggedUserID, memberID);

    if (loggedUserID != memberID && isFriend != "true") {
      this.showAddAsContact = true;
    }
  }

  async checkIfToShowFollowMember(memberID: string) {
    let loggedUserID = this.session.getSessionVal('userID');
    let isFollowing = await this.contactsSvc.isFollowingConnection(loggedUserID, memberID);

    if (loggedUserID != memberID && isFollowing != "true") {
      this.showFollowMember = true;
    }
  }

  onTabChange($event: any) {
    let tab = $event.nextId;
    if (tab == "basicInfo")
      this.getBasicInfo();
    else if (tab == "contact")
      this.getContactInfo();
    else if (tab == "education")
      this.getEducationInfo();
    else if (tab == "employment")
      this.getEmploymentInfo();
    else if (tab == "about")
      this.getAboutInfo();
  }

  async getVideoPlayList() {
    this.playList = await this.membersSvc.getVideoPlayList(this.memberID);
  }

  async onPlayListChange(id) {
    this.videosList = await this.membersSvc.getVideosList(id);
    this.vidCount = this.videosList.length;
  }

  async showOpenPopup(openModal, videoID: string) {
    this.videoId = videoID;
    this.modHand = this.ngbMod.open(openModal);
    return false;
  }

  async getBasicInfo() {
    this.basicInfoModel = await this.membersSvc.getMemberBasicInfo(this.memberID.toString());

    if (this.basicInfoModel.currentStatus != null)
      this.cStatus = this.basicInfoModel.currentStatus;

    if (this.basicInfoModel.bio != null)
      this.bio = this.basicInfoModel.bio;

    if (this.basicInfoModel.picturePath == null)
      this.memImage = environment.memberImagesUrlPath + "default.png";
    else
      this.memImage = environment.memberImagesUrlPath + this.basicInfoModel.picturePath;

    this.memTitle = this.basicInfoModel.titleDesc;
    this.memName = this.basicInfoModel.firstName + " " + this.basicInfoModel.lastName;
    if (this.basicInfoModel.sport == null)
      this.sport = "";
    else
      this.sport = this.basicInfoModel.sport;

    let str = "";
    if (this.basicInfoModel.lookingForEmployment)
      str = "Employment, ";
    if (this.basicInfoModel.lookingForNetworking)
      str = str + "Networking, ";

    if (this.basicInfoModel.lookingForPartnership)
      str = str + "Partnership, ";

    if (this.basicInfoModel.lookingForRecruitment)
      str = str + "Recruitment, ";
    str = str.slice(0, -2);

    this.memLookingFor = str;
  }

  async getContactInfo() {
    this.contactInfoModel = await this.membersSvc.getMemberContactInfo(this.memberID.toString());
    this.memOtherEmail = this.contactInfoModel.otherEmail;
    this.showEmail = this.contactInfoModel.showEmailToMembers;

  }

  async getAboutInfo() {
    this.aboutInfoModel = await this.membersSvc.getAboutMeInfo(this.memberID);
    if (this.aboutInfoModel.length != 0) {
      this.about = this.aboutInfoModel[0].aboutMe;
      this.interests = this.aboutInfoModel[0].interests;
      this.activities = this.aboutInfoModel[0].activities;
      this.specialSkills = this.aboutInfoModel[0].specialSkills;
    }
    else {
      this.about = "";
      this.interests = "";
      this.activities = "";
      this.specialSkills = "";
    }
  }

  async getEducationInfo() {
    this.educationInfoModel = await this.membersSvc.getMemberEducationInfo(this.memberID.toString());
    this.eduCount = this.educationInfoModel.length;
  }

  async getEmploymentInfo() {
    this.employmentInfoModel = await this.membersSvc.getMemberEmploymentInfo(this.memberID.toString());
    this.empCount = this.employmentInfoModel.length;
  }

  async getMyContactsList() {
    this.myContactsList = await this.contactsSvc.getMyConnectionsList(this.memberID.toString());
    this.contactCount = this.myContactsList.length;
  }

  async getMyNetworksList() {
    this.myNetworksList = await this.networksSvc.getMyNetworksList(this.memberID.toString());
    this.networkCount = this.myNetworksList.length;
  }

  async getMyEventsList() {
    this.myEventsList = await this.eventsSvc.getMyEventsList(this.memberID.toString());
    this.eventCount = this.myEventsList.length;
  }

  addSearchContactPopup(mod, name) {

    this.contactName = name;
    this.modHand = this.ngbMod.open(mod);
  }

  sendRequest(type) {
    this.sendTheRequest(type);
    this.modHand.close();
    return false;
  }

  async sendTheRequest(type) {
    let loggedUserID = this.session.getSessionVal('userID');
    let contactID = this.route.snapshot.queryParamMap.get('memberID');
    await this.contactsSvc.addConnection(loggedUserID, contactID)
    this.showAddAsContact = false;
  }

  async getPrivacySettings() {
    let privSettingsList = await this.settingsSvc.GetProfileSettings(this.memberID.toString());
    if (privSettingsList.length != 0) {
      this.showBasicInfo = privSettingsList[0].basicInfo;
      this.showContacts = privSettingsList[0].contactInfo;
      this.showPersonalInfo = privSettingsList[0].personalInfo;
      this.showEducation = privSettingsList[0].education;
      this.showWorkInfo = privSettingsList[0].workInfo;
    }
  }
  
  async doFollowMember() {
    let loggedUserID = this.session.getSessionVal('userID');
    await this.contactsSvc.followConnection(loggedUserID, this.memberID);
    this.showFollowMember = false;
  }

  async checkIfMemberIsAFriend(memberID: string) {
    let loggedUserID = this.session.getSessionVal('userID');
    let isFriend = await this.membersSvc.IsFriendByContactID(loggedUserID, memberID);

    if (isFriend == "true") {
      this.contactIsAFriend = true;
    }
    else {
      this.contactIsAFriend = false;
    }
  }

  checkIfSameUser(memberID: string) {
    let loggedUserID = this.session.getSessionVal('userID');
    if (loggedUserID == memberID) {
      this.isSameUser = true;
    }
    else {
      this.isSameUser = false;
    }
  }

  doShowProfile(memberId: string) {

    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate(['members/show-profile'], { queryParams: { memberID: memberId } });
    return false;
  }

}
