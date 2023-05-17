import { Component, ViewChild, OnInit } from '@angular/core';
import { SessionMgtService } from '../services/session-mgt.service';
import { ConnectionsService } from '../services/data/connections.service';
import { ContactModel } from '../models/contacts/contact-model';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-find-connections',
  templateUrl: './find-connections.component.html',
  styleUrls: ['./find-connections.component.css']
})
export class FindConnectionsComponent implements OnInit {

  @ViewChild('closebutton') closebutton;

  public memberId: string;
  public contactCnt: number = -1;
  public contactInfoList: ContactModel[];

  public searchType: string = "";

  public cntSuggestInfoList: ContactModel[];
  public cntSuggestCnt: number = 0;

  public spinner: boolean = false;
  public contactId = "";

  showModalBox: boolean = false;

  showErrMsg: boolean = false;
  errMsg: string;

  modHand: NgbModalRef;

  public contacts: Observable<any[]>;
 // private searchContacts = new Subject<string>();
  public contactName = '';
  public flag: boolean = true;

  searchModel = new SearchModel();
  public memberImagesUrlPath: string;

  constructor(private session: SessionMgtService, public contactSvc: ConnectionsService, public ngbMod: NgbModal) {
    this.memberImagesUrlPath = environment.memberImagesUrlPath;
  }

  ngOnInit() {
    this.memberId = this.session.getSessionVal('userID');
    this.searchModel.key = "";
  }

  async getContactSuggestions(memberID: string) {
    this.cntSuggestInfoList = await this.contactSvc.getConnectionSuggestions(memberID);
    if (this.cntSuggestInfoList != null) {
      this.cntSuggestCnt = this.cntSuggestInfoList.length;
    }
  }

  AddSuggestion(contactId: string) {

    this.addContact(contactId);
    this.getContactSuggestions(this.memberId);
    return false;
  }
  
  async addContact(contactId: string) {
    this.spinner = true;
    await this.contactSvc.addConnection(this.memberId, contactId);
    this.spinner = false;
  }
  
  addSearchedItem(contactId: string) {
    this.addContact(contactId);
    this.getSearchContacts(this.memberId, this.searchModel.key);
    return false;
  }

  /*
  async /*addTheContact(contactId: string) {
    this.spinner = true;
    await this.getSearchContacts(this.memberId, this.searchModel.key);
    this.spinner = false;
  }
  */

  doSearch() {
    this.getSearchContacts(this.memberId, this.searchModel.key);
  }

  async getSearchContacts(memberID: string, searchKey: string) {
    this.spinner = true;
    if (searchKey != undefined || searchKey != "") {
      this.contactInfoList = await this.contactSvc.getSearchConnections(memberID, searchKey);
    }
    if (this.contactInfoList != null) {
      this.contactCnt = this.contactInfoList.length;
    }
    else {
      this.contactCnt = 0;
    }
    this.spinner = false;
  }

  addSearchContactPopup(contactId, name, type) {
    this.contactId = contactId;
    this.contactName = name;
    this.searchType = type;
    this.showModalBox = true;
    return false;
  }

  sendRequest() {
    this.sendTheRequest();
    this.closebutton.nativeElement.click();
    return false;
  }

  async sendTheRequest() {
    this.spinner = true;
    if (this.searchType == "suggestion") {
      await this.contactSvc.addConnection(this.memberId, this.contactId)
      await this.getContactSuggestions(this.memberId);
    }
    else {
      await this.contactSvc.addConnection(this.memberId, this.contactId)
      await this.getSearchContacts(this.memberId, this.searchModel.key);
    }
    this.spinner = false;
  }
}

export class SearchModel {
  key: string;
}
