import { Component, OnInit } from '@angular/core';
import { SessionMgtService } from '../services/session-mgt.service';
import { AccountSettingsInfoModel } from '../models/settings/account-settings-info.model';
import { ISettingsService } from '../services/data/settings.service';

@Component({
    selector: 'account-setting-secQuestions',
    templateUrl: './account-setting-secQuestions.component.html',
    styleUrls: ['./account-setting-secQuestions.component.css']
})
export class AccountSettingSecQuetionsComponent implements OnInit {

    public show: boolean = false;
    public showErrMsg: boolean = false;
    public isSaving = false;
    public isSecQuestSuccess = false;

    asModelList = new Array<AccountSettingsInfoModel>();
    asModel = new AccountSettingsInfoModel()

    public constructor(public session: SessionMgtService, public setSvc: ISettingsService) {
    }

    memberID: string;

    ngOnInit() {
        this.memberID = this.session.getSessionVal('userID');
        this.asModel.securityQuestion = "select";
        this.getAccountSettingInfo();
    }

    async getAccountSettingInfo() {
        this.asModelList = await this.setSvc.GetMemberNameInfo(this.memberID);
        this.asModel = this.asModelList[0];
    }

    async saveASSecQuestions() {
        this.isSecQuestSuccess = false;
        this.isSaving = true;
        await this.setSvc.SaveSecurityQuestionInfo(this.memberID, this.asModel.securityQuestion, this.asModel.securityAnswer);
        this.isSaving = false;
        this.isSecQuestSuccess = true;
    }

}
