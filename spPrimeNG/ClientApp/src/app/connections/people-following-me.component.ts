import { Component, OnInit } from '@angular/core';
import { SessionMgtService } from '../services/session-mgt.service';
import { ConnectionsService } from '../services/data/connections.service';
import { ContactModel } from '../models/contacts/contact-model';
import { Observable } from 'rxjs';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { environment } from '../../environments/environment';

@Component({
  selector: 'people-following-me',
  templateUrl: './people-following-me.component.html',
  styleUrls: ['./people-following-me.component.css']
})
export class PeopleFollowingMeComponent implements OnInit {

  public memberId: string;
  public contactCnt: number = 0;
  public contactInfoList: ContactModel[];
  public spinner: boolean = false;
  public contactId = "";

  showErrMsg: boolean = false;
  errMsg: string;

  modHand: NgbModalRef;

  public connections: Observable<any[]>;
  //private searchContacts = new Subject<string>();
  public connectionID = '';
  public flag: boolean = true;
  public memberImagesUrlPath: string;

  showAddAsContact: boolean = false;

  constructor(private session: SessionMgtService, public contactSvc: ConnectionsService, public ngbMod: NgbModal) {
    this.memberImagesUrlPath = environment.memberImagesUrlPath;
  }

  ngOnInit(): void {
    this.memberId = this.session.getSessionVal('userID');
    this.getPeopleFollowingMe(this.memberId);
  }

  async getPeopleFollowingMe(memberId: string) {

    this.contactInfoList = await this.contactSvc.getPeopleFollowingMe(memberId);
    if (this.contactInfoList != null) {
      this.contactCnt = this.contactInfoList.length;
    }
  }

  addSearchContactPopup(mod, id) {
    this.connectionID = id;
    this.modHand = this.ngbMod.open(mod);
  }

  sendRequest() {
    this.sendTheRequest();
    this.modHand.close();
    return false;
  }

  async sendTheRequest() {
    let loggedUserID = this.session.getSessionVal('userID');
    await this.contactSvc.addConnection(loggedUserID,this.connectionID);
    this.getPeopleFollowingMe(loggedUserID);
  }

}



