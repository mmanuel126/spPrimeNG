import { Component, OnInit } from '@angular/core';
import { SessionMgtService } from '../services/session-mgt.service';
import { ConnectionsService } from '../services/data/connections.service';
import { ContactModel } from '../models/contacts/contact-model';
import { Subject, Observable } from 'rxjs';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent implements OnInit {

  public memberId: string;
  public contactCnt: number = 0;
  public contactInfoList: ContactModel[];
  public spinner: boolean = false;
  public contactId = "";

  showErrMsg: boolean = false;
  errMsg: string;

  modHand: NgbModalRef;

  public contacts: Observable<any[]>;
  //private searchContacts = new Subject<string>();
  public contactName = '';
  public flag: boolean = true;
  public memberImagesUrlPath: string;

  constructor(private session: SessionMgtService, public contactSvc: ConnectionsService, public ngbMod: NgbModal) {
    this.memberImagesUrlPath = environment.memberImagesUrlPath;
  }

  ngOnInit(): void {
    this.memberId = this.session.getSessionVal('userID');
    this.getContactRequests(this.memberId);
  }

  async getContactRequests(memberId: string) {
    this.contactInfoList = await this.contactSvc.getConnectionRequests(memberId);
    if (this.contactInfoList != null) {
      this.contactCnt = this.contactInfoList.length;
    }
  }

  acceptRequest(contactId) {
    this.acceptTheRequest(contactId)
    return false;
  }

  async acceptTheRequest(contactId) {
    this.spinner = true;
    await this.contactSvc.acceptRequest(this.memberId, contactId);
    await this.getContactRequests(this.memberId);
    this.spinner = false;
  }

  rejectRequest(contactId: string) {
    this.rejectTheRequest(contactId);
    return false;
  }

  async rejectTheRequest(contactId: string) {
    this.spinner = true;
    await this.contactSvc.rejectRequest(this.memberId, contactId);
    this.getContactRequests(this.memberId);
    this.spinner = false;
  }

}
