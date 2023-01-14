import { of as observableOf, Subject, Observable } from 'rxjs';
import { catchError, switchMap, distinctUntilChanged, debounceTime } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { SessionMgtService } from '../../services/session-mgt.service';
import { MessagesService } from '../../services/data/messages.service';
import { SearchMessageInfoModel } from '../../models/messages/search-message-info.model';
import { ConnectionsService } from '../../services/data/connections.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-view-messages',
  templateUrl: './view-messages.component.html',
  styleUrls: ['./view-messages.component.css']
})
export class ViewMessagesComponent implements OnInit {

  public memberId: string;
  public msgCnt: number = 0;
  public messageInfoList: SearchMessageInfoModel[];
  public textWeightVal: string = "none";
  public textWeightShowVal: string = "all";
  public spinner: boolean = false;
  public dateTime: string;
  public subject: string;
  public body: string;
  public senderId: string;
  public senderImage: string;
  public senderName: string;
  public messageId: string;

  showErrMsg: boolean = false;
  errMsg: string;
  modHand: NgbModalRef;
  searchModel = new SearchModel();
  msgModel = new MessageModel();
  autoCompleteModel = new AutoCompleteModel();

  public contacts: Observable<any[]>;
  private searchContacts = new Subject<string>();
  public contactName = '';
  public flag: boolean = true;

  public memImageUrlPath: string = environment.memberImagesUrlPath;

  constructor(private session: SessionMgtService, public msgSvc: MessagesService, public contactSvc: ConnectionsService, public ngbMod: NgbModal) { }

  ngOnInit() {
    this.memberId = this.session.getSessionVal('userID');
    this.getMessageItems(this.memberId, "Inbox", "All");
  }

  async getMessageItems(memberID: string, type: string, showType: string) {
    this.messageInfoList = await this.msgSvc.getMemberMessages(this.memberId, type, showType);
    if (this.messageInfoList != null) {
      this.msgCnt = this.messageInfoList.length;
    }
  }

  async toggleMsgStatus(status: string, msgID: string, folder: string) {
    await this.msgSvc.toggleMessageState(status, msgID, folder);
  }

  getMessages(memberID: string, type: string, showType: string) {
    if (showType == "All") {
      this.textWeightShowVal = "all"
    }
    else {
      this.textWeightShowVal = "unRead"
    }
    this.getMessageItems(memberID, type, showType);
    return false;
  }

  async markItem(showType: string, msgID: string) {
    if (showType == "Read") {
      await this.toggleMsgStatus("1", msgID, "Inbox");
    }
    if (showType == "UnRead") {
      await this.toggleMsgStatus("0", msgID, "Inbox");
    }

    await this.getMessageItems(this.memberId, "Inbox", "All");
    return false;
  }

  selectAll() {
    for (const element of this.messageInfoList) {
      element.selected = true;
    }
    this.textWeightVal = "all";
    return false;
  }

  selectNone() {
    for (const element of this.messageInfoList) {
      element.selected = false;
    }
    this.textWeightVal = "none";
    return false;
  }

  markBoxes(showType: string) {
    this.markAsCheckBoxes(showType);
    return false;
  }

  async markAsCheckBoxes(showType: string) {
    this.spinner = true;
    for (const element of this.messageInfoList) {

      if (element.selected) {
        if (showType == "Read") {
          await this.toggleMsgStatus("1", element.messageID, "Inbox");
        }
        else {
          await this.toggleMsgStatus("0", element.messageID, "Inbox");
        }
      }
    }
    location.reload();
  }

  doSearch() {
    if (!this.searchModel.key)
    {
      this.getMessageItems(this.memberId, "Inbox", "All");
    }
    else {
      this.getSearchMessages(this.memberId, this.searchModel.key, "Inbox");
    }
  }

  async getSearchMessages(memberID: string, searchKey: string, type: string) {
    this.spinner = true;
    this.messageInfoList = await this.msgSvc.searchMessage(memberID, searchKey, type);
    if (this.messageInfoList != null) {
      this.msgCnt = this.messageInfoList.length;
    }
    this.spinner = false;
  }

  DeleteItem(msgId: string) {
    this.DeleteMessage(msgId);
    return false;
  }

  async DeleteMessage(msgId: string) {
    this.spinner = true;
    await this.msgSvc.deleteMessage(msgId);
    location.reload();
  }

  DeleteItems() {
    this.DeleteMessages();
    return false;
  }

  async DeleteMessages() {
    this.spinner = true;
    for (const element of this.messageInfoList) {

      if (element.selected) {
        await this.msgSvc.deleteMessage(element.messageID);
      }
    }
    location.reload();
  }

  async showNewMessagePopup(newMsgModal) {
    this.autoCompleteModel = new AutoCompleteModel();
    let contacts = this.searchContacts.pipe(
      debounceTime(300),        // wait for 300ms pause in events  
      distinctUntilChanged(),   // ignore if next search term is same as previous  
      switchMap(term => term   // switch to new observable each time  
        // return the http search observable  
        ? this.contactSvc.searchMemberConnections(this.memberId, term)
        // or the observable of empty heroes if no search term  
        : observableOf<any[]>([])),
      catchError(error => {
        // real error handling to do
        console.log(error);
        return observableOf<any[]>([]);
      })); this.contacts = contacts;
    this.modHand = this.ngbMod.open(newMsgModal);
    return false;
  }

  // Push a search term into the observable stream.  
  searchContact(name: string): void {
    this.flag = true;
    this.searchContacts.next(this.autoCompleteModel.name);
  }

  onselectContact(name, id) { 
    this.autoCompleteModel.name = name;
    this.autoCompleteModel.id = id;
    this.flag = false; 
    return false;
  }

  async showOpenMessagePopup(openMsgModal,
    fromID, fromImage,
    fromName, subject, dateTime, body, msgID
  ) {
    this.senderId = fromID;
    this.senderImage = fromImage;
    this.senderName = fromName;
    this.subject = subject;
    this.dateTime = dateTime;
    this.body = body
    this.messageId = msgID;

    this.modHand = this.ngbMod.open(openMsgModal);

    this.toggleMsgStatus("1", msgID, "Inbox");
    await this.getMessageItems(this.memberId, "Inbox", "All");
    return false;
  }

  async sendNewMsg() {
    if (this.autoCompleteModel.id == null || this.autoCompleteModel.id == "") {
      this.showErrMsg = true;
      this.errMsg = "Selected contact to send message is not valid!";
    }
    else {
      this.showErrMsg = false;
      this.spinner = true;
      await this.msgSvc.sendMessage(this.memberId, this.autoCompleteModel.id, this.msgModel.subject, this.msgModel.message)
      this.autoCompleteModel.id = ""; this.autoCompleteModel.name = "";
      this.msgModel.message = ""; this.msgModel.subject = ""; this.msgModel.toId = "";
      this.spinner = false;
      this.modHand.close();
      await this.getMessageItems(this.memberId, "Inbox", "All");
    }
  }

  async SendMessage() {
    this.spinner = true;
    let msg = this.msgModel.message;
    await this.msgSvc.sendMessage(this.memberId, this.senderId, this.subject, msg);
    this.modHand.close();
    this.msgModel.message = "";
    await this.getMessageItems(this.memberId, "Inbox", "All");
    this.spinner = false;
    return false;
  }
}

export class SearchModel {
  key: string;
}

export class MessageModel {
  message: string;
  toId: string;
  subject: string;
}

export class AutoCompleteModel {
  name: string;
  id: string;
}

