import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionMgtService } from '../services/session-mgt.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ISettingsService } from '../services/data/settings.service';

@Component({
    selector: 'account-setting-deactivate',
    templateUrl: './account-setting-deactivate.component.html',
    styleUrls: ['./account-setting-deactivate.component.css']
})
export class AccountSettingDeactivateComponent implements OnInit {

    public show: boolean = false;
    public showErrMsg: boolean = false;
    public isSaving = false;
    modHand: NgbModalRef;

    public constructor(public session: SessionMgtService, private router: Router,
        public setSvc: ISettingsService, public modalService: NgbModal) {
    }

    memberID: string;
    asModel = new DeactivateModel();

    ngOnInit() {
        this.memberID = this.session.getSessionVal('userID');
        this.asModel.reason = "select"
    }

    jumpToConfirmModal(mod) {
        this.modHand = this.modalService.open(mod);
        return false;
    }

    async doDeactivate(modal) {
        this.isSaving = true;
        await this.setSvc.DeactivateAccount(this.memberID, this.asModel.reason, this.asModel.explanation);
        this.isSaving = false;
        this.modalService.dismissAll(modal);
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
