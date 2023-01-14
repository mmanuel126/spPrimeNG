import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { AdsModel } from '../models/ads.model';
import { lastValueFrom } from 'rxjs';

@Injectable()

export class AdsService implements IAdsService {

    COMMON_SERVICE_URI: string = environment.webServiceURL + "common/";
    requestQuery: string;

    constructor(public http: HttpClient) { }

    async getAds(type: string) {

        this.requestQuery = `${this.COMMON_SERVICE_URI}GetAds?type=${type}`;
        let responseData = await lastValueFrom(this.http.get<Array<AdsModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }

}

export abstract class IAdsService {
    abstract getAds(type: string);
}
