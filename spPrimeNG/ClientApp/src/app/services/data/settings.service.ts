import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ICommonService } from '../common.service';
import { environment } from '../../../environments/environment';
import { AccountSettingsInfoModel } from 'src/app/models/settings/account-settings-info.model';
import { NotificationsSettingModel } from 'src/app/models/settings/notifications-setting.model';
import { PrivacySettingsModel } from 'src/app/models/settings/privacy-settings.model';
import { lastValueFrom } from 'rxjs';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8',
        'authorization': 'Bearer ' + localStorage.getItem("access_token")
    })
}

@Injectable()
export class SettingsService implements ISettingsService {

    SETTINGS_SERVICE_URI: string = environment.webServiceURL + "setting/";
    requestQuery: string;

    constructor(public httpClient: HttpClient, public common: ICommonService) { }

    async GetMemberNameInfo(memberID: string) {
        this.requestQuery = `${this.SETTINGS_SERVICE_URI}GetMemberNameInfo/${memberID}`;
        let responseData = await lastValueFrom(this.httpClient.get<Array<AccountSettingsInfoModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }

    async GetMemberNotifications(memberID: string) {
        this.requestQuery = `${this.SETTINGS_SERVICE_URI}GetMemberNotifications/${memberID}`;
        let responseData = await lastValueFrom(this.httpClient.get<Array<NotificationsSettingModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }

    async SaveMemberNameInfo(memberID: string, firstName: string, middleName: string, lastName: string) {
        this.requestQuery = `${this.SETTINGS_SERVICE_URI}SaveMemberNameInfo/${memberID}?fName=${firstName}&mName=${middleName}&lName=${lastName}`;
        await lastValueFrom(this.httpClient.put(this.requestQuery, null,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async SaveMemberEmailInfo(memberID: string, email: string) {
        this.requestQuery = `${this.SETTINGS_SERVICE_URI}SaveMemberEmailInfo/${memberID}?email=${email}`;
        await lastValueFrom(this.httpClient.put(this.requestQuery, null,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async SavePasswordInfo(memberID: string, pwd: string) {
        let postBody: Object = {
            memberID: memberID,
            pwd: pwd
        }
        let requestData = JSON.stringify(postBody);

        this.requestQuery = `${this.SETTINGS_SERVICE_URI}SavePasswordInfo`;
        await lastValueFrom(this.httpClient.put(this.requestQuery, requestData,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async SaveSecurityQuestionInfo(memberID: string, question: string, answer: string) {
        this.requestQuery = `${this.SETTINGS_SERVICE_URI}SaveSecurityQuestionInfo/${memberID}?questionID=${question}&answer=${answer}`;
        await lastValueFrom(this.httpClient.put(this.requestQuery, null,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async DeactivateAccount(memberID: string, reason: string, explanation: string) {
        let futureEmail = false;
        this.requestQuery = `${this.SETTINGS_SERVICE_URI}DeactivateAccount/${memberID}?reason=${reason}&explanation=${explanation}&futureEmail=${futureEmail}`;
        await lastValueFrom(this.httpClient.put(this.requestQuery, null,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async SaveNotificationSettings(memberID: string, body: NotificationsSettingModel) {
        this.requestQuery = `${this.SETTINGS_SERVICE_URI}SaveNotificationSettings/${memberID}`;
        let requestData = JSON.stringify(body);
        await lastValueFrom(this.httpClient.put(this.requestQuery, requestData, httpOptions));
    }

    async GetProfileSettings(memberID: string) {
        this.requestQuery = `${this.SETTINGS_SERVICE_URI}GetProfileSettings/${memberID}`;
        let responseData = await lastValueFrom(this.httpClient.get<Array<PrivacySettingsModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }

    async SaveProfileSettings(memberID: string, body: PrivacySettingsModel) {
        this.requestQuery = `${this.SETTINGS_SERVICE_URI}SaveProfileSettings/${memberID}`;
        let requestData = JSON.stringify(body);
        await lastValueFrom(this.httpClient.put(this.requestQuery, requestData,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async GetSearchSettings(memberID: string) {
        this.requestQuery = `${this.SETTINGS_SERVICE_URI}GetPrivacySearchSettings/${memberID}`;
        let responseData = await lastValueFrom(this.httpClient.get<Array<PrivacySettingsModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }

    async SaveSearchSettings(memberID: string, body: PrivacySettingsModel) {
        this.requestQuery = `${this.SETTINGS_SERVICE_URI}SavePrivacySearchSettings/${memberID}
        ?visibility=${body.visibility}&viewProfilePicture=${body.viewProfilePicture}
        &viewFriendsList=${body.viewFriendsList}&viewLinkToRequestAddingYouAsFriend=${body.viewLinksToRequestAddingYouAsFriend}
        &viewLinkToSendYouMsg=${body.viewLinkTSendYouMsg}`;
        await lastValueFrom(this.httpClient.put(this.requestQuery, null,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }
}

export abstract class ISettingsService {
    abstract SaveSearchSettings(memberID: string, body: PrivacySettingsModel);
    abstract GetSearchSettings(memberID: string);
    abstract SaveProfileSettings(memberID: string, body: PrivacySettingsModel);
    abstract GetProfileSettings(memberID: string)
    abstract SaveNotificationSettings(memberID: string, body: NotificationsSettingModel);
    abstract DeactivateAccount(memberID: string, reason: string, explanation: string);
    abstract SaveSecurityQuestionInfo(memberID: string, question: string, answer: string);
    abstract SaveMemberEmailInfo(memberID: string, email: string);
    abstract SaveMemberNameInfo(memberID: string, firstName: string, middleName: string, lastName: string);
    abstract GetMemberNotifications(memberID: string);
    abstract GetMemberNameInfo(memberID: string);
    abstract SavePasswordInfo(memberID: string, pwd: string);
}
