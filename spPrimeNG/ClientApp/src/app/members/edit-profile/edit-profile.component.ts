import { Component, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from "@angular/router";
import { MembersService } from '../../services/data/members.service';
import { MemberProfileBasicInfoModel } from '../../models/members/profile-member.model';
import { SessionMgtService } from '../../services/session-mgt.service';
import { environment } from '../../../environments/environment';
import { MemberProfileEducationModel } from '../../models/members/profile-education.model';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap/modal/modal-ref';
import { SchoolsByStateModel } from '../../models/organization/schools-by-state.model';
import { ICommonService } from '../../services/common.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { StatesModel } from '../../models/states.model';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  @ViewChild('closeEditButton') closeEditButton;
  showEditModalBox: boolean = false;

  @ViewChild('ddlSchoolType') ddlSchoolType;
  @ViewChild('ddlST') ddlState;
  @ViewChild('ddlSC') ddlSchool;
  @ViewChild('ddlDG') ddlDegree;
  @ViewChild('ddlYR') ddlYear;
  @ViewChild('ddlCL') ddlCompLevel;

  @ViewChild('closeAddButton') closeAddButton;
  showAddModalBox: boolean = false;

  @ViewChild('closeRemoveButton') closeRemoveButton;
  showRemoveModalBox: boolean = false;

  public show: boolean = false;
  public showErrMsg: boolean = false;
  public noSchools: boolean = false;
  public years: any;

  public states: StatesModel[];
  schoolsList: SchoolsByStateModel[];
  schoolName: string;
  schoolID: string;
  schoolType: string;

  modHand: NgbModalRef;

  eduInfoEdit: MemberProfileEducationModel = {
    schoolID: "",
    schoolImage: "",
    Societies: "",
    schoolAddress: "select",
    schoolName: "",
    schoolType: "3",
    webSite: "",
    major: "", degree: "select", yearClass: "select",
    degreeTypeID: "",
    sportLevelType: ""
  }

  eduInfo: MemberProfileEducationModel = {
    schoolID: "",
    schoolImage: "",
    Societies: "",
    schoolAddress: "select",
    schoolName: "",
    schoolType: "3",
    webSite: "",
    major: "", degree: "select", yearClass: "select", degreeTypeID: "",
    sportLevelType: ""
  }

  //basic info variables  
  memberID: string;
  memImage: string;
  memName: string;
  memTitle: string;
  adId: string;

  basicInfoModel: MemberProfileBasicInfoModel = new MemberProfileBasicInfoModel();
  public isSaving = false;
  public isSuccess = false;
  public isSuccessVidSave = false;
  public channelID: string;
  public instagramURL: string;
  defaultTab: number;
  mySubscription: any;

  constructor(private router: Router, public session: SessionMgtService,
    private route: ActivatedRoute, public membersSvc: MembersService,
    private comSvc: ICommonService,
    public modalService: NgbModal)
  {
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

  educationInfoModel: MemberProfileEducationModel[];

  ngOnInit(): void {
    this.memberID = this.session.getSessionVal('userID');
    this.adId = this.route.snapshot.queryParamMap.get('adId');
    if (this.adId == "2")
      this.defaultTab = 3;
    else
      this.defaultTab = 0;
    this.getBasicInfo();
    this.getChannelID();
    this.getInstagramURL();

    this.memberID = this.session.getSessionVal('userID');
    this.getEducationYears();
    this.getEducationInfo(); 
    this.getStates();
  }

  ngOnDestroy() {
    if (this.mySubscription) {
      this.mySubscription.unsubscribe();
    }
  }

  async getBasicInfo() {
    this.basicInfoModel = await this.membersSvc.getMemberBasicInfo(this.memberID.toString());
   
    if (this.basicInfoModel.picturePath.length == 0) {
      this.memImage = environment.memberImagesUrlPath + "default.png"; 
    }
    else {
      this.memImage = environment.memberImagesUrlPath + this.basicInfoModel.picturePath; 
    }
    this.memTitle = this.basicInfoModel.titleDesc;
    this.memName = this.basicInfoModel.firstName + " " + this.basicInfoModel.lastName;
  }

  async getChannelID() {
    this.channelID = await this.membersSvc.getChannelId(this.memberID);
  }

  async getInstagramURL() {
    let c = await this.membersSvc.getInstagramURL(this.memberID);
    this.instagramURL = c;
  }

  async saveChannelID() {
    this.isSuccessVidSave = false;
    this.isSaving = true;
    await this.membersSvc.saveChannelID(this.memberID, this.channelID);
    this.isSaving = false;
    this.isSuccessVidSave = true;
  }

  async saveInstagramURL() {
    this.isSuccess = false;
    this.isSaving = true;
    await this.membersSvc.saveInstagramURL(this.memberID, this.instagramURL);
    this.isSaving = false;
    this.isSuccess = true;
  }

  ///
  async getEducationInfo() {
    this.educationInfoModel = await this.membersSvc.getMemberEducationInfo(this.memberID.toString());
    if (this.educationInfoModel.length == 0) {
      this.noSchools = true;
    }
    else { this.noSchools = false }
  }

  async getStates() {
    this.states = await this.comSvc.getStates();
  }

  jumpToAddSchool() {
    this.showAddModalBox = true;
    return false;
  }

  jumpToEditSchool(id: string, name: string, major: string, degree: string, classYear: string, sportLevelType:string, schoolType:string) {
    this.schoolName = name;
    this.eduInfoEdit.degree = degree;
    this.eduInfoEdit.major = major;
    this.eduInfoEdit.yearClass = classYear;
    this.eduInfoEdit.schoolID = id;
    this.eduInfoEdit.sportLevelType = sportLevelType;
    this.eduInfoEdit.schoolType = schoolType;
    this.getEducationYears();
    this.showEditModalBox = true;
    return false;
  }

  jumpToRemoveSchool(id: string, type: string, name: string) {
    this.schoolName = name;
    this.schoolID = id;
    this.schoolType = type;
    this.showRemoveModalBox = true;
    return false;
  }

  async onStateChange(ev: any) {
    let state = (ev.target as HTMLInputElement).value;
    this.schoolsList = await this.comSvc.getSchoolsByState(state, this.eduInfo.schoolType);
  }

  async doAddNewSchool(form) {
    this.isSaving = true;
    await this.membersSvc.AddNewSchool(this.memberID, this.eduInfo);
    this.getEducationInfo();
    this.isSaving = false;
    //do reset controls
    this.ddlSchoolType.nativeElement.selectedIndex = 0;
    this.ddlState.nativeElement.selectedIndex = 0;
    this.ddlSchool.nativeElement.selectedIndex = 0;
    this.eduInfo.major = "";
    this.ddlDegree.nativeElement.selectedIndex = 0;
    this.ddlYear.nativeElement.selectedIndex = 0;
    this.ddlCompLevel.nativeElement.selectedIndex = 0;
    this.closeAddButton.nativeElement.click();
  }

  async doUpdateSchool() {
    this.isSaving = true;
    await this.membersSvc.UpdateSchool(this.memberID, this.eduInfoEdit);
    this.getEducationInfo();
    this.isSaving = false;
    this.closeEditButton.nativeElement.click();
  }

  async doRemoveSchool() {
    this.isSaving = true;
    await this.membersSvc.RemoveSchool(this.memberID, this.schoolID, this.schoolType);
    this.getEducationInfo();
    this.isSaving = false;
    this.closeRemoveButton.nativeElement.click();
  }

  goToLink(url: string) {
    window.open(url + '/', '_blank');
    return false;
  }

  public getEducationYears() {
    let baseYear = 2040;
    let y = [];

    for (let i = baseYear; i >= 1900; i--) {
      y.push(i);
    }
    this.years = Array.from(new Set(y)); 
  }
}
