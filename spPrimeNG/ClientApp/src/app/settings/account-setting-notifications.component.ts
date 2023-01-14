import { Component, OnInit } from '@angular/core';
import { SessionMgtService } from '../services/session-mgt.service';
import { NotificationsSettingModel } from '../models/settings/notifications-setting.model';
import { ISettingsService } from '../services/data/settings.service';
import { environment } from '../../environments/environment';

@Component({
    selector: 'account-setting-notifications',
    templateUrl: './account-setting-notifications.component.html',
    styleUrls: ['./account-setting-notifications.component.css']
})
export class AccountSettingNotificationsComponent implements OnInit {

    public show: boolean = false;
    public showErrMsg: boolean = false;
    public isSaving = false;
    public isNotificationSuccess = false;
    webSiteName = environment.appLogoText;

    asModelList = new Array<NotificationsSettingModel>();
    asModel = new NotificationsSettingModel();

    public constructor(public session: SessionMgtService, public setSvc: ISettingsService) {
    }

    memberID: string;

    ngOnInit() {
        this.memberID = this.session.getSessionVal('userID');
        this.getMemberNotificationSettings();
    }

    async getMemberNotificationSettings() {
        this.asModelList = await this.setSvc.GetMemberNotifications(this.memberID);
        if (this.asModelList.length != 0)
            this.asModel = this.asModelList[0];
        console.log(this.asModelList);
        console.log(this.asModel);
    }

    async saveASNotifications() {
        this.isNotificationSuccess = false;
        this.isSaving = true;
        await this.setSvc.SaveNotificationSettings(this.memberID, this.asModel);
        this.isSaving = false;
        this.isNotificationSuccess = true;
    }

}
