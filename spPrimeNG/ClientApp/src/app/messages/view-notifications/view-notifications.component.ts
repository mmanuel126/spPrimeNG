
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { SessionMgtService } from '../../services/session-mgt.service';
import { MessagesService } from '../../services/data/messages.service';
import { MemberNotificationsModel } from '../../models/messages/member-notifications.model';
import { ConnectionsService } from '../../services/data/connections.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-view-notifications',
  templateUrl: './view-notifications.component.html',
  styleUrls: ['./view-notifications.component.css']
})
export class ViewNotificationsComponent implements OnInit {

  public memberId: string;
  public msgCnt: number = 0;
  public messageInfoList: MemberNotificationsModel[];
  public textWeightVal: string = "none";
  public textWeightShowVal: string = "all";
  public spinner: boolean = false;

  showErrMsg: boolean = false;
  errMsg: string;

  modHand: NgbModalRef;

  public contacts: Observable<any[]>;
  public contactName = '';
  public flag: boolean = true;

  constructor(private session: SessionMgtService, public msgSvc: MessagesService, public contactSvc: ConnectionsService, public ngbMod: NgbModal) { }

  ngOnInit() {
    this.memberId = this.session.getSessionVal('userID');
    this.getNotificationItems(this.memberId, "All");
  }

  async getNotificationItems(memberID: string, showType: string) {
    this.messageInfoList = await this.msgSvc.getMemberNotifications(memberID, showType);
    if (this.messageInfoList != null) {
      this.msgCnt = this.messageInfoList.length;
    }
  }

}

