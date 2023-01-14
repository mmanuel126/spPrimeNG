import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { SessionMgtService } from '../services/session-mgt.service';
import { MessagesService } from '../services/data/messages.service'

@Injectable()
export class StateService {
  public unReadMessages = new BehaviorSubject('UnReadeMessages');

  constructor(public session: SessionMgtService, public msgSvc: MessagesService,) { }
  msgCnt: string;

  setUnReadMessages(msgCnt: string) {

    this.unReadMessages.next(msgCnt);
  }

}