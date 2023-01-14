import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICommonService } from '../common.service';
import { environment } from '../../../environments/environment';
import { CompanyBySectorIndustryModel } from 'src/app/models/organization/company-by-sec-industry.model';
import { lastValueFrom } from 'rxjs';

@Injectable()
export class OrganizationsService implements IOrganizationsService {

    ORGANIZATIONS_SERVICE_URI: string = environment.webServiceURL + "organizations/";
    COMMON_SERVICE_URI: string = environment.webServiceURL + "common/";

    requestQuery: string;

    constructor(public httpClient: HttpClient, public common: ICommonService) { }

    async getCompanySectors() {

        let lst = new Array<string>();
        this.requestQuery = `${this.ORGANIZATIONS_SERVICE_URI}GetCompanySectors`;
        let responseData = await lastValueFrom(this.httpClient.get(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        responseData = Array.of(responseData);

        let obj = responseData[0];
        for (const element of obj) {
            lst.push(element);
        }
        return lst;
    }

    async getIndustries(sector: string) {

        let lst = new Array<string>();
        this.requestQuery = `${this.ORGANIZATIONS_SERVICE_URI}GetIndustries?sector=${sector}`;
        let responseData = await lastValueFrom(this.httpClient.get(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        responseData = Array.of(responseData);

        let obj = responseData[0];
        for (const element of obj) {
            lst.push(element);
        }
        return lst;
    }

    async getCompanies(sector: string, industry: string) {
        let lst = new Array<CompanyBySectorIndustryModel>();
        this.requestQuery = `${this.ORGANIZATIONS_SERVICE_URI}GetCompanies?sector=${sector}&industry=${industry}`;
        let responseData = await lastValueFrom(this.httpClient.get(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        responseData = Array.of(responseData);

        let obj = responseData[0];
        for (const element of obj) {
            let s = new CompanyBySectorIndustryModel();
            s.id = element.id;
            s.name = element.name;
            lst.push(s);
        }
        return lst;
    }
}

export abstract class IOrganizationsService {
    abstract getCompanySectors();
    abstract getIndustries(sector: string);
    abstract getCompanies(sector: string, industry: string);

}
