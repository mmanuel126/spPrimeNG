import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SessionMgtService } from '../services/session-mgt.service';
import { PrivacySettingsModel } from '../models/settings/privacy-settings.model';
import { ISettingsService } from '../services/data/settings.service';

@Component({
    selector: 'privacy-setting-profile',
    templateUrl: './privacy-setting-profile.component.html',
    styleUrls: ['./privacy-setting-profile.component.css']
})
export class PrivacySettingProfileComponent implements OnInit {

    public show: boolean = false;
    public showErrMsg: boolean = false;
    public isProfileSuccess:boolean=false;
    public isSaving:boolean = false;

    asModelList = new Array<PrivacySettingsModel>();
    asModel = new PrivacySettingsModel();

    public constructor(public session: SessionMgtService, private route: ActivatedRoute, private router: Router,
        public setSvc: ISettingsService) {
    }

    memberID: string;

    ngOnInit() {
        this.memberID = this.session.getSessionVal('userID');
        this.getProfileSettings();
    }

    async getProfileSettings() {
        this.asModelList = await this.setSvc.GetProfileSettings(this.memberID);
        this.asModel = this.asModelList[0];
    }

    async saveProfileSettings() {
        this.isProfileSuccess = false;
        this.isSaving = true;
        await this.setSvc.SaveProfileSettings(this.memberID, this.asModel);
        this.isSaving = false;
        this.isProfileSuccess = true;
    }

}
