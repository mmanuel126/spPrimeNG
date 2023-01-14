import { Component, OnInit } from '@angular/core';
import { SessionMgtService } from '../services/session-mgt.service';
import { ISettingsService } from '../services/data/settings.service';
import { PrivacySettingsModel } from '../models/settings/privacy-settings.model';

@Component({
    selector: 'privacy-setting-search',
    templateUrl: './privacy-setting-search.component.html',
    styleUrls: ['./privacy-setting-search.component.css']
})
export class PrivacySettingSearchComponent implements OnInit {

    public show: boolean = false;
    public showErrMsg: boolean = false;
    public isSaving :boolean = false;
    public isSearchSuccess: boolean = false;
    

    asModelList = new Array<PrivacySettingsModel>();
    asModel = new PrivacySettingsModel();

    public constructor(public session: SessionMgtService, public setSvc: ISettingsService) {
    }

    memberID: string;

    ngOnInit() {
        this.memberID = this.session.getSessionVal('userID');
        this.getSearchSettings();
    }

    async getSearchSettings() {
        this.asModelList = await this.setSvc.GetSearchSettings(this.memberID);
        this.asModel = this.asModelList[0];
    }

    async saveSearchSettings() {
        this.isSaving = true;
        this.isSearchSuccess = false;
        await this.setSvc.SaveSearchSettings(this.memberID, this.asModel);
        this.isSaving = false;
        this.isSearchSuccess = true;
    }


}
