import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { SportsListModel } from 'src/app/models/members/profile-member.model';
import { SchoolsByStateModel } from 'src/app/models/organization/schools-by-state.model';
import { firstValueFrom, lastValueFrom } from 'rxjs'

@Injectable()

export class CommonService implements ICommonService {

  COMMON_SERVICE_URI: string = environment.webServiceURL + "common/";
  requestQuery: string;

  constructor(public http: HttpClient) { }

  async encryptString(str: string) {
    this.requestQuery = `${this.COMMON_SERVICE_URI}EncryptString?encrypt=${str}`;
    let response = await firstValueFrom(this.http.get(this.requestQuery,
      {
        headers: {
          'Content-Type': 'application/json; charset=utf-8'
        }, responseType: 'text'
      }));
    return response.toString();
  }

  async decryptString(str: string) {
    this.requestQuery = `${this.COMMON_SERVICE_URI}DecryptString?encrypted=${str}`;
    let response = await firstValueFrom(this.http.get(this.requestQuery,
      {
        headers: {
          'Content-Type': 'application/json; charset=utf-8'
        }, responseType: 'text'
      }));
    return response.toString();
  }

  async getSportsList() {
    this.requestQuery = `${this.COMMON_SERVICE_URI}GetSportsList`;
    let responseData = await lastValueFrom(this.http.get<Array<SportsListModel>>(this.requestQuery,
      {
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          'authorization': 'Bearer ' + localStorage.getItem("access_token")
        }
      }
    ));
    return responseData;
  }

  async getSchoolsByState(state: string, instType: string) {
    this.requestQuery = `${this.COMMON_SERVICE_URI}GetSchoolByState?state=${state}&institutionType=${instType}`;
    let responseData = await lastValueFrom(this.http.get<Array<SchoolsByStateModel>>(this.requestQuery,
      {
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          'authorization': 'Bearer ' + localStorage.getItem("access_token")
        }
      }
    ));
    return responseData;
  }

  async logError(message: string, stack: string) {
    this.requestQuery = `${this.COMMON_SERVICE_URI}Logs`;
    let params = new HttpParams()
      .append('message', message)
      .append('stack', stack);
    await firstValueFrom(this.http.get(this.requestQuery, { params: params }));
  }

  async getYears(maxYear:number, baseYear:number ) {
    //this will get a number of years backward to display on drop downs.
    let years = []; 
    for (let i = maxYear; i > baseYear; i--) {
      years.push(i);
    }
    return years
  }

}

export abstract class ICommonService {
    abstract encryptString(str: string): Promise<string>;
    abstract decryptString(str: string): Promise<string>;
    abstract getSportsList(): Promise<Array<SportsListModel>>;
    abstract getSchoolsByState(state: string, instType: string);
    abstract logError(message: string, stack: string);
    abstract getYears(maxYear:number, baseYear:number);
}

