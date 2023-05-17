import { Component, OnInit } from '@angular/core';
import { MemberProfileBasicInfoModel, SportsListModel } from '../../models/members/profile-member.model';
import { ActivatedRoute, Router } from '@angular/router';
import { MembersService } from '../../services/data/members.service';
import { IOrganizationsService } from '../../services/data/organizations.service';
import { ICommonService } from '../../services/common.service';
import { SessionMgtService } from '../../services/session-mgt.service';

@Component({
  selector: 'user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {

  public show: boolean = false;
  public showErrMsg: boolean = false;
  public isSaving = false;
  public isSuccess = false;
  public sector = "";
  public isAthlete = false;
  public years: any;

  sportsList = new Array<SportsListModel>();

  basicInfo: MemberProfileBasicInfoModel = {
    picturePath: "",
    memProfileName: "",
    titleDesc: "",
    memProfileStatus: "",
    memProfileGender: "",
    memProfileDOB: "",
    interestedDesc: "",
    memProfileLookingFor: "",
    currentCity: "",
    currentStatus: "",
    dobDay: "",
    dobMonth: "",
    dobYear: "",
    firstName: null,
    homeNeighborhood: "",
    hometown: "",
    interestedInType: "",
    joinedDate: "",
    lastName: "",
    lookingForEmployment: false,
    lookingForNetworking: false,
    lookingForPartnership: false,
    lookingForRecruitment: false,
    memberID: "",
    middleName: "",
    politicalView: "",
    religiousView: "",
    sex: "",
    showDOBType: "",
    showSexInProfile: "",
    getLGEntitiesCount: "",
    sport: "",
    leftRightHandFoot: "",
    bio: "",
    height: "",
    weight: "",
    preferredPosition: "",
    secondaryPosition: "",
  }

  basicInfoModel: MemberProfileBasicInfoModel = {
    picturePath: "",
    memProfileName: "",
    titleDesc: "",
    memProfileStatus: "",
    memProfileGender: "",
    memProfileDOB: "",
    interestedDesc: "",
    memProfileLookingFor: "",
    currentCity: "",
    currentStatus: "",
    dobDay: "",
    dobMonth: "",
    dobYear: "",
    firstName: null,
    homeNeighborhood: "",
    hometown: "",
    interestedInType: "",
    joinedDate: "",
    lastName: "",
    lookingForEmployment: false,
    lookingForNetworking: false,
    lookingForPartnership: false,
    lookingForRecruitment: false,
    memberID: "",
    middleName: "",
    politicalView: "",
    religiousView: "",
    sex: "",
    showDOBType: "",
    showSexInProfile: "",
    getLGEntitiesCount: "",
    sport: "",
    leftRightHandFoot: "",
    bio: "",
    height: "",
    weight: "",
    preferredPosition: "",
    secondaryPosition: "",
  }

  public constructor(public session: SessionMgtService, private route: ActivatedRoute, private router: Router,
    private membersSvc: MembersService, private orgSvc: IOrganizationsService, private comSvc: ICommonService) {
  }

  memberID: string;


  ngOnInit() {
    this.memberID = this.session.getSessionVal('userID');
    this.getBasicInfo(this.memberID);
    this.getSportsList();
    this.getBirthDayYears();
  }

  async getBasicInfo(memberId: string) {
    this.basicInfoModel = await this.membersSvc.getMemberBasicInfo(this.memberID.toString());

    if (this.basicInfoModel.currentStatus != "Athlete (Amateur)" && this.basicInfoModel.currentStatus != "Athlete (Professional)") {
      this.isAthlete = false;
    }
    else {
      this.isAthlete = true;
    }
  }

  async getSportsList() {
    this.sportsList = await this.comSvc.getSportsList();
    let x = this.sportsList;
  }

  async getIndustries(sec) { }

  onSectorChange(sector) { }

  async saveBasicInfo() {
    this.isSuccess = false;
    this.isSaving = true;
    if (this.basicInfoModel.currentStatus != "Athlete (Amateur)" && this.basicInfoModel.currentStatus != "Athlete (Professional)") {
      this.basicInfoModel.leftRightHandFoot = "";
      this.basicInfoModel.preferredPosition = "";
      this.basicInfoModel.secondaryPosition = "";
      this.basicInfoModel.height = "";
      this.basicInfoModel.weight = "";

    }
    if (this.basicInfoModel.middleName == null)
      this.basicInfoModel.middleName = "";

    await this.membersSvc.SaveMemberGeneralInfo(this.memberID, this.basicInfoModel);
    this.isSaving = false;
    this.isSuccess = true;
  }

  showAthleteAttributes(val: string) {
    if (val != "Athlete (Amateur)" && val != "Athlete (Professional)")
      this.isAthlete = false;
    else
      this.isAthlete = true;
  }

  public getBirthDayYears() {
    let baseYear = 2040;
    let y = [];

    for (let i = baseYear; i >= 1900; i--) {
      y.push(i);
    }
    this.years = Array.from(new Set(y)); 
  }
}
