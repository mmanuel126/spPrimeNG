import { Component, NgZone } from '@angular/core';
import { RecentNewsModel } from '../models/recent-news.model';
import { MembersService } from '../services/data/members.service';
import { ICommonService } from '../services/common.service';
import { SessionMgtService } from '../services/session-mgt.service';
import { RecentPostsModel } from '../models/recent-posts.model';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { MessagesService } from '../services/data/messages.service';
import { StateService } from '../services/state.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public webSiteDomain = environment.webSiteDomain;
  recentNews: Promise<RecentNewsModel[]>;
  recentPosts: RecentPostsModel[];
  errorMessage: string;
  memberID: string = "";
  p: number = 1;
  memberImageUrlpath: string = environment.memberImagesUrlPath;

  postID: string;
  modHand: NgbModalRef;
  public show: boolean = false;

  msgBadgeCnt: string;

  postModel: PostModel = {
    postText: "",
  }

  progGressMdl: ProgressModel = {
    labelText: "",
  }

  public constructor(public httpClient: HttpClient, public stateService: StateService, public msgSvc: MessagesService, public membersSvc: MembersService, private router: Router,
    public common: ICommonService, public session: SessionMgtService, public modalService: NgbModal, public zone: NgZone) {
    this.memberID = this.session.getSessionVal('userID');
  }

  ngOnInit() {
    this.getRecentData();
  }

  async getRecentData() {
    this.recentNews = this.membersSvc.getRecentNews();
    this.show = true;
    this.recentPosts = await this.membersSvc.getRecentPosts(this.memberID);
    this.show = false;
  }

  async doPost() {
    this.membersSvc.doPost(this.memberID, this.postModel.postText, "0");
    this.modHand.close();
    this.show = true;
    this.recentPosts = await this.membersSvc.getRecentPosts(this.memberID);
    this.show = false;
    this.postModel.postText = "";
    return false;
  }

  async doPostReply() {
    this.membersSvc.doPost(this.memberID, this.postModel.postText, this.postID);
    this.modHand.close();
    this.show = true;
    this.recentPosts = await this.membersSvc.getRecentPosts(this.memberID);
    this.show = false;
    this.postModel.postText = "";
    return false;
  }

  async refreshPosts() {
    this.show = true;
    this.recentPosts = await this.membersSvc.getRecentPosts(this.memberID);
    this.show = false;
    return false;
  }

  async showMemberProfile(id: string) {
    this.router.navigate(['members/show-profile'], { queryParams: { memberID: id } });
    return false;
  }

  jumpToComment(postID: string, commentModal) {
    this.postID = postID;
    this.modHand = this.modalService.open(commentModal);
    return false;
  }
}

export class PostModel {
  postText: string;
}

export class ProgressModel {
  labelText: string;
}

