import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from "@angular/router";
import { MembersService } from '../../services/data/members.service';
import { MemberProfileBasicInfoModel } from '../../models/members/profile-member.model';
import { SessionMgtService } from '../../services/session-mgt.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  //basic info variables  
  memberID: string;
  memImage: string;
  memName: string;
  memTitle: string;
  adId: string;

  basicInfoModel: MemberProfileBasicInfoModel = new MemberProfileBasicInfoModel();
  public isSaving = false;
  public isSuccess = false;
  channelID: string;
  instagramURL: string;
  defaultTab: number;
  mySubscription: any;

  constructor(private router: Router, public session: SessionMgtService, private route: ActivatedRoute, public membersSvc: MembersService) {
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
    this.memberID = this.session.getSessionVal('userID');
    this.adId = this.route.snapshot.queryParamMap.get('adId');
    if (this.adId == "2")
      this.defaultTab = 3;
    else
      this.defaultTab = 0;
    this.getBasicInfo();
    this.getChannelID();
    this.getInstagramURL();
  }

  ngOnDestroy() {
    if (this.mySubscription) {
      this.mySubscription.unsubscribe();
    }
  }

  async getBasicInfo() {
    this.basicInfoModel = await this.membersSvc.getMemberBasicInfo(this.memberID.toString());
    if (this.basicInfoModel.picturePath == null)
      this.memImage = environment.memberImagesUrlPath + "default.png";
    else
      this.memImage = environment.memberImagesUrlPath + this.basicInfoModel.picturePath;

    this.memTitle = this.basicInfoModel.titleDesc;
    this.memName = this.basicInfoModel.firstName + " " + this.basicInfoModel.lastName;
  }

  async getChannelID() {
    this.channelID = await this.membersSvc.getChannelId(this.memberID);
  }

  async getInstagramURL() {
    this.instagramURL = await this.membersSvc.getInstagramURL(this.memberID);
  }

  async saveChannelID() {
    this.isSuccess = false;
    this.isSaving = true;
    await this.membersSvc.saveChannelID(this.memberID, this.channelID);
    this.isSaving = false;
    this.isSuccess = true;
  }

  async saveInstagramURL() {
    this.isSuccess = false;
    this.isSaving = true;
    await this.membersSvc.saveInstagramURL(this.memberID, this.instagramURL);
    this.isSaving = false;
    this.isSuccess = true;
  }
}
