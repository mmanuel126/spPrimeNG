import { Component, OnInit } from '@angular/core';
import { SessionMgtService } from '../services/session-mgt.service';
import { AccountSettingsInfoModel } from '../models/settings/account-settings-info.model';
import { ISettingsService } from '../services/data/settings.service';

@Component({
    selector: 'account-setting-name',
    templateUrl: './account-setting-name.component.html',
    styleUrls: ['./account-setting-name.component.css']
})
export class AccountSettingNameComponent implements OnInit {

    public show: boolean = false;
    public showErrMsg: boolean = false;
    public isSaving = false;
    public isNameSuccess = false;

    asModelList = new Array<AccountSettingsInfoModel>();
    asModel = new AccountSettingsInfoModel();
    
    public constructor(public session: SessionMgtService, public setSvc:ISettingsService) {
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

    async saveASName() {
        this.isNameSuccess = false;
        this.isSaving = true;
        await this.setSvc.SaveMemberNameInfo(this.memberID, this.asModel.firstName, this.asModel.middleName, this.asModel.lastName);
        this.isSaving = false;
        this.isNameSuccess = true;
    }

}
