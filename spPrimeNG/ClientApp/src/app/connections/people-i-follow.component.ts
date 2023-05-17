import { Component, ViewChild, OnInit } from '@angular/core';
import { SessionMgtService } from '../services/session-mgt.service';
import { ConnectionsService } from '../services/data/connections.service';
import { ContactModel } from '../models/contacts/contact-model';
import { Observable } from 'rxjs';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { environment } from '../../environments/environment';

@Component({
  selector: 'people-i-follow',
  templateUrl: './people-i-follow.component.html',
  styleUrls: ['./people-i-follow.component.css']
})
export class PeopleIFollowComponent implements OnInit {

  public memberId: string;
  public contactCnt: number = 0;
  public contactInfoList: ContactModel[];
  public spinner: boolean = false;
  public contactId = "";

  showErrMsg: boolean = false;
  errMsg: string;

  showModalBox: boolean = false;

  @ViewChild('closebutton') closebutton;
  modHand: NgbModalRef;

  searchModel = new SearchModel();

  public connections: Observable<any[]>;
  public contactName = '';
  public flag: boolean = true;
  public memberImagesUrlPath: string;

  constructor(private session: SessionMgtService, public contactSvc: ConnectionsService, public ngbMod: NgbModal) {
    this.memberImagesUrlPath = environment.memberImagesUrlPath;
  }

  ngOnInit(): void {
    this.memberId = this.session.getSessionVal('userID');
    this.getPeopleIFollow(this.memberId);
  }

  async getPeopleIFollow(memberId: string) {
    this.contactInfoList = await this.contactSvc.getPeopleIFollow(memberId);
    if (this.contactInfoList != null) {
      this.contactCnt = this.contactInfoList.length;
    }
  }

  showUnfollowPopup(id) {
    this.contactId = id;
    this.showModalBox = true;
    return false;
  }

  async doUnfollowMember() {
    this.spinner = true;
    await this.contactSvc.unFollowMember(this.memberId, this.contactId);
    this.getPeopleIFollow(this.memberId);
    this.closebutton.nativeElement.click();
    this.spinner = false;
    return false;
  }
}

export class SearchModel {
  key: string;
}

