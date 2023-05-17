import { Component, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { SessionMgtService } from '../services/session-mgt.service';
import { ISettingsService } from '../services/data/settings.service';
import { Router } from '@angular/router';

@Component({
  selector: 'account-setting',
  templateUrl: './account-setting.component.html',
  styleUrls: ['./account-setting.component.css']
})
export class AccountSettingComponent implements OnInit {

  @ViewChild('closeDeactivateButton') closeDeactivateButton;
  showConfirmModalBox: boolean = false;
  public isSaving = false;

  constructor(public setSvc: ISettingsService, public session: SessionMgtService,
    private router: Router, private route: ActivatedRoute) { }

  memberID: string;
  asModel = new DeactivateModel();

  ngOnInit(): void {
    this.memberID = this.session.getSessionVal('userID');
    this.asModel.reason = "select"
  }

  jumpToConfirmModal() {
    //this.modHand = this.modalService.open(mod);
    this.showConfirmModalBox = true;
    return false;
  }

  async doDeactivate() {
    this.isSaving = true;
    await this.setSvc.DeactivateAccount(this.memberID, this.asModel.reason, this.asModel.explanation);
    this.isSaving = false;
    //this.modalService.dismissAll(modal);
    this.closeDeactivateButton.nativeElement.click();
    this.session.setSessionVar('isUserLogin', null);
    this.session.setSessionVar('userID', null);
    this.session.setSessionVar('userEmail', null);
    this.session.setSessionVar('userImage', null);
    this.session.setSessionVar('pwd', null);
    this.router.navigate(['/']);
  }
}

export class DeactivateModel {
  reason: string;
  explanation: string;
}
