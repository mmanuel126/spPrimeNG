import { Component, OnInit } from '@angular/core';
import { SessionMgtService } from '../services/session-mgt.service';
import { AccountSettingsInfoModel } from '../models/settings/account-settings-info.model';
import { ISettingsService } from '../services/data/settings.service';

@Component({
    selector: 'account-setting-email',
    templateUrl: './account-setting-email.component.html',
    styleUrls: ['./account-setting-email.component.css']
})
export class AccountSettingEmailComponent implements OnInit {

    public show: boolean = false;
    public showErrMsg: boolean = false;
    public isSaving = false;
    public isEmailSuccess = false;

    asModelList = new Array<AccountSettingsInfoModel>();
    asModel = new AccountSettingsInfoModel()

    public constructor(public session: SessionMgtService, public setSvc: ISettingsService) {
    }

    memberID: string;

    ngOnInit() {
        this.memberID = this.session.getSessionVal('userID');
        this.getAccountSettingInfo();
    }

    async getAccountSettingInfo() {
        this.asModelList = await this.setSvc.GetMemberNameInfo(this.memberID);
        this.asModel = this.asModelList[0];
    }

    async saveASEMail() {
        this.isEmailSuccess = false;
        this.isSaving = true;
        await this.setSvc.SaveMemberEmailInfo(this.memberID, this.asModel.email);
        this.isSaving = false;
        this.isEmailSuccess = true;
    }
}
