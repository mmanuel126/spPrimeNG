import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SessionMgtService } from '../services/session-mgt.service';
import { Register } from '../models/register.model';
import { ISettingsService } from '../services/data/settings.service';

@Component({
    selector: 'account-setting-password',
    templateUrl: './account-setting-password.component.html',
    styleUrls: ['./account-setting-password.component.css']
})
export class AccountSettingPasswordComponent implements OnInit {

    public show: boolean = false;
    public showErrMsg: boolean = false;
    public isSaving = false;
    public isPwdSuccess = false;
    public isValidPwd = true;

    asModel = new Register();

    public constructor(public session: SessionMgtService, private route: ActivatedRoute, private router: Router,
        public setSvc: ISettingsService) {
    }

    memberID: string;

    ngOnInit() {
        this.memberID = this.session.getSessionVal('userID');
    }

    async saveASPassword(frm: NgForm) {
        this.isPwdSuccess = false;
        this.isSaving = true;
        if (!this.asModel.code.includes(this.session.getSessionVal('pwd'))) {
            this.isValidPwd = false;
        }
        else {
            await this.setSvc.SavePasswordInfo(this.memberID, this.asModel.password);
        }

        this.isSaving = false;
        this.isPwdSuccess = true;
    }
}
