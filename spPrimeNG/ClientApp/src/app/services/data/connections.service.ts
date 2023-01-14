import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICommonService } from '../common.service';
import { ContactModel, SearchResultModel } from '../../models/contacts/contact-model';
import { SearchModel } from '../../models/contacts/search-model';
import { environment } from '../../../environments/environment';
import { lastValueFrom } from 'rxjs';

@Injectable()
export class ConnectionsService {

    CONNECTIONS_SERVICE_URI: string = environment.webServiceURL + "connection/";
    CONNECTIONS_SERVICE_URI2: string = environment.webServiceURL + "Connection/";
    requestQuery: string;

    constructor(public httpClient: HttpClient, public common: ICommonService) { }

    async getMyConnectionsList(memberID: string) {

        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}GetMemberConnections?memberID=${memberID}&show=`;
        let responseData = await lastValueFrom(this.httpClient.get<Array<ContactModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }

    async searchMemberConnections(memberID: string, searchText: string) {
      this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}SearchMemberConnections?memberID=${memberID}&searchText=${searchText}`;
      let responseData = await lastValueFrom( this.httpClient.get<Array<ContactModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }

    async getSearchResults(memberID: string, searchText: string) {
      this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}SearchAllResults?memberID=${memberID}&searchText=${searchText}`;
    let responseData = await lastValueFrom(this.httpClient.get<Array<SearchResultModel>>(this.requestQuery,
        {
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'authorization': 'Bearer ' + localStorage.getItem("access_token")
          }
        }
      ));
      return responseData;
    }

    async getMyConnections(memberId: string) {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}GetMemberConnections?memberID=${memberId}&show=`;
        let responseData = await lastValueFrom(this.httpClient.get<Array<ContactModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }

    async getPeopleIFollow(memberId: string) {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}GetPeopleIFollow?memberID=${memberId}`;
        let responseData = await lastValueFrom(this.httpClient.get<Array<ContactModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }

    async getPeopleFollowingMe(memberId:string)
    {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}GetWhosFollowingMe?memberID=${memberId}`;
      let responseData = await lastValueFrom(this.httpClient.get<Array<ContactModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }
    
    async followConnection(memberId: string, contactId: string) {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}FollowMember?memberID=${memberId}&contactID=${contactId}`;
        await lastValueFrom(this.httpClient.post(this.requestQuery,null,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async isFollowingConnection(loggedUserID: string, contactID:string)
    {
        let memberID= loggedUserID;
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI2}IsFollowingConnection?memberID=${memberID}&contactID=${contactID}`;
        
        let response = await lastValueFrom(this.httpClient.get(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }, responseType: 'text'
            }
        ));
        return response.toString();
    }

    async unFollowMember(memberId: string, contactId: string) {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}UnfollowMember?memberID=${memberId}&contactID=${contactId}`;
        await lastValueFrom(this.httpClient.post(this.requestQuery,null,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async deleteConnection(memberId: string, contactId: string) {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}DeleteConnection?memberID=${memberId}&contactID=${contactId}`;
        await lastValueFrom(this.httpClient.delete(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async getConnectionRequests(memberId: string) {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}GetMemberConnections?memberID=${memberId}&show=Requests`;
        let responseData = await lastValueFrom(this.httpClient.get<Array<ContactModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }

    async getConnectionSuggestions(memberId: string) {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}GetMemberConnectionSuggestions?memberID=${memberId}`;
        let responseData = await lastValueFrom(this.httpClient.get<Array<ContactModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }

    async acceptRequest(memberId: string, contactId: string) {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}AcceptRequest?memberID=${memberId}&contactID=${contactId}`;
        await lastValueFrom(this.httpClient.put(this.requestQuery, null,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async rejectRequest(memberId: string, contactId: string) {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}RejectRequest?memberID=${memberId}&contactID=${contactId}`;
        await lastValueFrom(this.httpClient.put(this.requestQuery, null,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async getSearchConnections(memberId: string, searchText: string) {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}GetSearchConnections?userID=${memberId}&searchText=${searchText}`;
        let responseData = await lastValueFrom(this.httpClient.get<Array<ContactModel>>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
    }

    async addConnection(memberId: string, contactId: string) {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}SendRequestConnection?memberID=${memberId}&contactID=${contactId}`;
        await lastValueFrom(this.httpClient.put(this.requestQuery, null,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async getSearchList(memberId: string, searchText: string) {
        this.requestQuery = `${this.CONNECTIONS_SERVICE_URI}SearchResults?memberID=${memberId}&searchText=${searchText}`;
        let responseData = await lastValueFrom(this.httpClient.get<Array<SearchModel>>(this.requestQuery,
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
