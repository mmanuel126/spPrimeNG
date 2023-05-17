import { Component,ViewChild, OnInit } from '@angular/core';
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

  @ViewChild('closebutton') closebutton;

  public memberId: string;
  public contactCnt: number = 0;
  public contactInfoList: ContactModel[];
  public spinner: boolean = false;
  public contactId = "";

  showModalBox: boolean = false;

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
      this.contactCnt = this.contactInfoList.length; console.log(this.contactInfoList);
    }
  }

  addSearchContactPopup(id) {
    this.connectionID = id;
    this.showModalBox = true;
  }

  sendRequest() {
    this.sendTheRequest();
    this.closebutton.nativeElement.click();
    return false;
  }

  async sendTheRequest() {
    this.spinner = true;
    let loggedUserID = this.session.getSessionVal('userID');
    await this.contactSvc.addConnection(loggedUserID,this.connectionID);
    this.getPeopleFollowingMe(loggedUserID);
    this.spinner = false;
  }

  /*
  async addContact(contactId: string) {
    this.spinner = true;
    await this.contactSvc.addConnection(this.memberId, contactId);
    this.spinner = false;
  } */

}



