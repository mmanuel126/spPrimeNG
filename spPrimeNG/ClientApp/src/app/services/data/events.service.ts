import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICommonService } from '../common.service';
import { MemberEventsModel } from '../../models/events/member-events-model';
import { environment } from '../../../environments/environment';
import { lastValueFrom } from 'rxjs';

@Injectable()
export class EventsService implements IEventsService {

    EVENTS_SERVICE_URI: string = environment.webServiceURL + "event/";
    requestQuery: string;

    constructor(public httpClient: HttpClient, public common: ICommonService) { }

    async getMyEventsList(memberID: string) {
      let lst = new Array<MemberEventsModel>();
      this.requestQuery = `${this.EVENTS_SERVICE_URI}GetMemberEvents/${memberID}&show=`;
      let responseData = await lastValueFrom( this.httpClient.get(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));

        responseData = Array.of(responseData);
        let obj = responseData[0]; //get second level data only
        for (const element of obj) {

            let mp = new MemberEventsModel();

            if (element.eventID != null)
                mp.eventID = element.eventID.toString();
            else mp.eventID = "";

            if (element.cnt != null)
                mp.cnt = element.cnt.toString();
            else mp.cnt = "";

            if (element.planningWhat != null)
                mp.planningWhat = element.planningWhat.toString();
            else mp.planningWhat = "";

            if (element.location != null)
                mp.location = element.location.toString();
            else mp.location = "";

            if (element.eventDate != null)
                mp.eventDate = element.eventDate.toString();
            else mp.eventDate = "";

            if (element.RSVP != null)
                mp.RSVP = element.RSVP.toString();
            else mp.RSVP = "";

            if (element.eventParams != null)
                mp.eventParams = element.eventParams.toString();
            else mp.eventParams = "";

            if (element.startDate != null)
                mp.startDate = element.startDate.toString();
            else mp.startDate = "";

            if (element.endDate != null)
                mp.endDate = element.endDate.toString();
            else mp.endDate = "";

            if (element.showCancel != null)
                mp.showCancel = element.showCancel.toString();
            else mp.showCancel = "";

            if (element.eventImg != null)
               mp.eventImg = environment.eventImageUrlpath  + element.eventImg.toString();
            else mp.eventImg = environment.eventImageUrlpath + "default.png";

            lst.push(mp);
        }
        return lst;
    }

    //private truncate(value: string, maxLength: number) {
    //    if ((value == null) || (value == undefined) || (value.length == 0)) return value;
    //    return (value.length <= maxLength ? value : value.substring(0, maxLength))
    //}

    //private extractData(res: Response) {
    //    let body = res
    //    return body || [];
    //}

}

export abstract class IEventsService {
  abstract getMyEventsList(memberID: string);
}
