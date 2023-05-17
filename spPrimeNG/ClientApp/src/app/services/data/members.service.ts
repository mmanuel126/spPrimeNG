import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { RecentNewsModel } from '../../models/recent-news.model';
import { RecentPostsModel, RecentPostChildModel } from '../../models/recent-posts.model';
import { ICommonService } from '../../services/common.service';
import { SessionMgtService } from '../../services/session-mgt.service';
import { InstagramUserModel, MemberProfileBasicInfoModel, YoutubePlayListModel, YoutubeVideosListModel } from '../../models/members/profile-member.model';
import { MemberProfileEducationModel } from '../../models/members/profile-education.model';
import { MemberProfileEmploymentModel } from '../../models/members/profile-employment.model';
import { MemberProfileContactInfoModel } from '../../models/members/profile-contact-info.model';
import { MemberProfileAboutModel } from '../../models/members/profile-about.model';
import { RegisteredUser } from '../../models/registered-user.model';
import { environment } from '../../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../../services/auth.service';
import { catchError, tap } from 'rxjs/operators';
import { firstValueFrom, lastValueFrom, Observable, of } from 'rxjs';

@Injectable()
export class MembersService {

    headers = new HttpHeaders({
        'Content-Type': 'application/text; charset=utf-8',
        'Authorization': 'Bearer ' + localStorage.getItem('access_token'),
    });

  MEMBERS_SERVICE_URI: string = environment.webServiceURL  + "member/";
  ORGANIZATIONS_SERVICE_URI: string = environment.webServiceURL  + "organizations/";
    requestQuery: string;
    helper = new JwtHelperService();
    accessToken = localStorage.getItem("access_token");

    constructor(public httpClient: HttpClient, public common: ICommonService, public session: SessionMgtService, public auth: AuthService) {
    }

    // authenticates user from service data call
    async AuthenticateUser(email: string, pwd: string, rememberMe: string, screen: string) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}AuthenticateUser?email=${email}&pwd=${pwd}&rememberMe=${rememberMe}&screen=${screen}`;
        let response = await firstValueFrom(this.httpClient.get(this.requestQuery, { responseType: 'text' }));
        return response.toString();
    }

    // Get recent news
    async getRecentNews() {
        let lst = new Array<RecentNewsModel>();
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}getRecentNews`;
        let responseData = await lastValueFrom(this.httpClient.get(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        responseData = Array.of(responseData);
        let mainData = responseData[0];

        for (const element of mainData) {
            let AddNewsArray = new RecentNewsModel();
            if (element.imageUrl != null)
                AddNewsArray.newsImgUrl = element.imageUrl.toString().replace("~", "").replace("Images", "images");
            else
                AddNewsArray.newsImgUrl = "/images/RecentNews/default.png";

            if (element.headerText != null)
                AddNewsArray.newsTitle = element.headerText;
            else
                AddNewsArray.newsTitle = "";

            if (element.postingDate != null) {
                let date = new Date(element.postingDate);
                let dtFormat = date.getMonth() + '/' + date.getDate() + '/' + date.getFullYear();
                AddNewsArray.newsDatePosted = dtFormat;
            }
            else
                AddNewsArray.newsDatePosted = "";

            if (element.textField != null)
                AddNewsArray.newsDetail = this.truncate(element.textField, 120);
            else
                AddNewsArray.newsDetail = "";

            if (element.navigateUrl != null)
                AddNewsArray.newsUrl = element.navigateUrl;
            else
                AddNewsArray.newsUrl = "";

            if (element.id != null)
                AddNewsArray.newsID = element.id;
            else
                AddNewsArray.newsID = "";

            lst.push(AddNewsArray);
        }
        return lst;
    }

    //get recent posts
    async getRecentPosts(memberID: string) {
        let lst = new Array<RecentPostsModel>();
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}getRecentPosts/${memberID}`;
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
            let mp = new RecentPostsModel();
            if (element.picturePath != null)
                mp.picturePath = element.picturePath.toString();
            else mp.picturePath = "";

            if (element.datePosted != null)
                mp.datePosted = element.datePosted.toString();
            else mp.datePosted = "";

            if (element.description != null)
                mp.description = element.description.toString();
            else mp.description = "";

            if (element.firstName != null)
                mp.firstName = element.firstName.toString();
            else mp.firstName = "";

            if (element.memberID != null)
                mp.memberID = element.memberID; 
            else mp.memberID = "";

            if (element.memberName != null)
                mp.memberName = element.memberName.toString();
            else mp.memberName = "";

            if (element.postID != null)
                mp.postID = element.postID.toString();
            else mp.postID = "";

            this.requestQuery = `${this.MEMBERS_SERVICE_URI}getRecentPostResponses/${mp.postID}`;
            
            let responseData = await lastValueFrom(this.httpClient.get(this.requestQuery,
                {
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'authorization': 'Bearer ' + localStorage.getItem("access_token")
                    }
                }));
            
            responseData = Array.of(responseData);
            let objC = responseData[0];
            let lstChild = new Array<RecentPostChildModel>();

            for (const element of objC) {

                let mp2 = new RecentPostChildModel();

                if (element.picturePath != null)
                    mp2.picturePath = element.picturePath.toString();
                else mp2.picturePath = "";

                if (element.dateResponded != null)
                    mp2.dateResponded = element.dateResponded.toString();
                else mp2.dateResponded = "";

                if (element.description != null)
                    mp2.description = element.description.toString();
                else mp2.description = "";

                if (element.firstName != null)
                    mp2.firstName = element.firstName.toString();
                else mp2.firstName = "";

                if (element.memberID != null)
                    mp2.memberID = element.memberID; 
                else mp2.memberID = "";

                if (element.memberName != null)
                    mp2.memberName = element.memberName.toString();
                else mp2.memberName = "";

                if (element.postID != null)
                    mp2.postID = element.postID.toString();
                else mp2.postID = "";

                if (element.postResponseID != null)
                    mp2.postResponseID = element.postResponseID.toString();
                else mp2.postResponseID = "";

                lstChild.push(mp2);
            }
            mp.children = lstChild;
            lst.push(mp);
        }
        return lst;
    }

    async doPost(memberID: string, txt: string, postID: string) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}SavePosts/${memberID}/${postID}?postMsg=${txt}`;
        let response = await firstValueFrom(this.httpClient.post(this.requestQuery, null,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return response.toString();
    }

    async SendAdvertismentInfo(firstName: string, lastName: string,
        company: string, email: string, phone: string, countryVal: string, title: string) {
        let url = `${this.MEMBERS_SERVICE_URI}SendAdvertisementInfo?FirstName=${firstName}&LastName=${lastName}&Company=${company}
                &Email=${email}&Phone=${phone}&Country=${countryVal}&Title=${title}`;
        await firstValueFrom(this.httpClient.get(url, { responseType: 'text' }));
    }

    async ResetPassword(email: string) {
        let url = `${this.MEMBERS_SERVICE_URI}ResetPassword?email=${email}`;

        await firstValueFrom(this.httpClient.get(url,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }, responseType: 'text'
            }));
    }

    async isResetCodeExpired(code: string) {
        let url = `${this.MEMBERS_SERVICE_URI}IsResetCodeExpired?code=${code}`;
        let response = await  firstValueFrom( this.httpClient.get(url,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }, responseType: 'text'
            }));
        return response.toString();
    }

    async changePassword(pwd: string, email: string, code: string) {
        let url = `${this.MEMBERS_SERVICE_URI}ChangePassword?pwd=${pwd}&email=${email}&code=${code}`;
        let response = await firstValueFrom( this.httpClient.get(url,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }, responseType: 'text'
            }
        ));
        return response.toString();
    }

    async registerUser(firstName: string, lastName: string, email: string, passWord: string,
        gender: string, month: string, day: string, year: string) {
        let url = `${this.MEMBERS_SERVICE_URI}RegisterUserToLG?fName=${firstName}&lName=${lastName}&email=${email}&pwd=${passWord}&day=${day}&month=${month}&year=${year}&gender=${gender}`;
        let response = await firstValueFrom(this.httpClient.get(url,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }, responseType: 'text'
            }
        ));
        return response.toString();
    }

    async validateNewRegisteredUser(email: string, code: string) {
        let url = `${this.MEMBERS_SERVICE_URI}ValidateNewRegisteredUser?email=${email}&code=${code}`;
        let response = await firstValueFrom(this.httpClient.get<Array<RegisteredUser>>(url,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return response;
    }

    async getMemberBasicInfo(memberID: string) {
      this.requestQuery = `${this.MEMBERS_SERVICE_URI}GetMemberGeneralInfoV2/${memberID}`;
      let responseData = await firstValueFrom(this.httpClient.get<MemberProfileBasicInfoModel>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
        return responseData;
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

    async getMemberContactInfo(memberID: string) {

       //let lst = new Array<MemberProfileContactInfoModel>();
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}GetMemberContactInfo/${memberID}`;
        let responseData = await firstValueFrom(this.httpClient.get<MemberProfileContactInfoModel>(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
          }
        ));
        return responseData;
    }

    async getMemberEducationInfo(memberID: string) {
        let lst = new Array<MemberProfileEducationModel>();
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}GetMemberEducationInfo/${memberID}`;
        let responseData = await firstValueFrom(this.httpClient.get(this.requestQuery,
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

            let mp = new MemberProfileEducationModel();

            if (element["schoolID"] != null)
                mp.schoolID = element["schoolID"].toString();
            else mp.schoolID = "";

            if (element["schoolName"] != null)
                mp.schoolName = element["schoolName"].toString();
            else mp.schoolName = "";

            if (element["schoolAddress"] != null)
                mp.schoolAddress = element["schoolAddress"].toString();
            else mp.schoolAddress = "";

            if (element["degree"] != null)
                mp.degree = element["degree"].toString();
            else mp.degree = "";

            if (element["degreeTypeID"] != null)
                mp.degreeTypeID = element["degreeTypeID"].toString();
            else mp.degreeTypeID = "";

            if (element["major"] != null)
                mp.major = element["major"].toString();
            else mp.major = "";


            if (element["schoolImage"] != null)
                mp.webSite = element["schoolImage"].toString();
            else mp.webSite = "";

            if (mp.webSite.indexOf('http') == -1) {
                mp.webSite = "http://" + mp.webSite;
            }

            if (element["schoolImage"] != null) {
                if (element["schoolImage"] != "")
                    mp.schoolImage = "https://www.google.com/s2/favicons?domain=" + element["schoolImage"].toString();
                else mp.schoolImage = "http://www.marcman.xyz/assets/images/members/default.png";
            }
            if (element["yearClass"] != null)
                mp.yearClass = element["yearClass"].toString();
            else mp.yearClass = "";

            if (element["schoolType"] != null)
                mp.schoolType = element["schoolType"].toString();
            else mp.schoolType = "";

            if (element["societies"] != null)
                mp.Societies = element["societies"].toString();
            else mp.Societies = "";

            if (element["sportLevelType"] != null)
                mp.sportLevelType = element["sportLevelType"].toString();
            else mp.sportLevelType = "";

            lst.push(mp);
        }
        return lst;
    }

    async getMemberEmploymentInfo(memberID: string) {
        let lst = new Array<MemberProfileEmploymentModel>();
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}GetMemberEmploymentInfo/${memberID}`;
        let responseData = await firstValueFrom(this.httpClient.get(this.requestQuery,
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

            let mp = new MemberProfileEmploymentModel();

            if (element["companyID"] != null)
                mp.companyID = element["companyID"].toString();
            else mp.companyID = "";

            if (element["companyName"] != null)
                mp.companyName = element["companyName"].toString();
            else mp.companyName = "";

            if (element["title"] != null)
                mp.title = element["title"].toString();
            else mp.title = "";

            let city = "";
            let state = "";
            let Address = "";

            if (element["city"] != null)
                city = element["city"].toString();

            if (element["state"] != null)
                state = element["state"].toString();

            if (city != "")
                Address = city;
            if (Address != "")
                Address = Address + ", " + state;
            else
                Address = state;

            mp.companyAddress = Address;

            if (element["companyImage"] != null) {
                if (element["companyImage"] != "")
                    mp.companyImage = "https://www.google.com/s2/favicons?domain=" + element["companyImage"].toString();
                else mp.companyImage = "http://www.marcman.xyz/assets/images/members/default.png";
            }

            if (element["website"] != null) {
                if (element["website"].toString().search("http") == -1)
                    mp.website = "http://" + element["website"].toString();
                else
                    mp.website = element["website"].toString();
            }
            else
                mp.website = "";

            let startMonth = ""; let startYear = ""; let endMonth = ""; let endYear = "";

            if (element["startMonth"] != null)
                startMonth = element["startMonth"].toString();
            if (element["startYear"] != null)
                startYear = element["startYear"].toString();
            if (element["endMonth"] != null)
                endMonth = element["endMonth"].toString();
            if (element["endYear"] != null)
                endYear = element["endYear"].toString();

            let workedDT = "";
            if (endYear == "Pres")
                workedDT = "Date: " + startMonth + "/" + startYear + " - Present";
            else
                workedDT = "Date: " + startMonth + "/" + startYear + " - " + endMonth + "/" + endYear;

            mp.datesWorked = workedDT;

            if (element["description"] != null)
                mp.Description = element["description"].toString();
            else mp.Description = "";

            if (element["jobDesc"] != null)
                mp.JobDesc = element["jobDesc"].toString();
            else mp.JobDesc = "";

            if (element["email"] != null)
                mp.Email = element["email"].toString();
            else mp.Email = "";

            if (element["industry"] != null)
                mp.Industry = element["industry"].toString();
            else mp.Industry = "";

            if (element["ipoYear"] != null)
                mp.IPOYear = element["ipoYear"].toString();
            else mp.IPOYear = "";

            if (element["sector"] != null)
                mp.Sector = element["sector"].toString();
            else mp.Sector = "";

            if (element["summaryQuote"] != null)
                mp.summaryQuote = element["summaryQuote"].toString();
            else mp.summaryQuote = "";

            if (element["symbol"] != null)
                mp.Symbol = element["symbol"].toString();
            else mp.Symbol = "";

            if (element["empInfoID"] != null)
                mp.EmpInfoID = element["empInfoID"].toString();
            else mp.EmpInfoID = "";

            if (element["city"] != null)
                mp.City = element["city"].toString();
            else mp.City = "";

            if (element["state"] != null)
                mp.State = element["state"].toString();
            else mp.State = "";

            if (element["startMonth"] != null)
                mp.StartMonth = element["startMonth"].toString();
            else mp.StartMonth = "";

            if (element["startYear"] != null)
                mp.StartYear = element["startYear"].toString();
            else mp.StartYear = "";

            if (element["endMonth"] != null)
                mp.EndMonth = element["endMonth"].toString();
            else mp.EndMonth = "";

            if (element["endYear"] != null)
                mp.EndYear = element["endYear"].toString();
            else mp.EndYear = "";

            if (element["currentlyWorkedHere"] != null)
                mp.CurrentlyWorkedHere = element["currentlyWorkedHere"].toString();
            else mp.CurrentlyWorkedHere = "";

            lst.push(mp);
        }
        return lst;
    }

    async getAboutMeInfo(memberID: string) {

        let lst = new Array<MemberProfileAboutModel>();
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}GetMemberPersonalInfo/${memberID}`;
        let responseData = await firstValueFrom(this.httpClient.get(this.requestQuery,
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

            let mp = new MemberProfileAboutModel();

            if (element.aboutMe != null)
                mp.aboutMe = element.aboutMe.toString();
            else mp.aboutMe = "";

            if (element.activities != null)
                mp.activities = element.activities.toString();
            else mp.activities = "";

            if (element.interests != null)
                mp.interests = element.interests.toString();
            else mp.interests = "";

            if (element.specialSkills != null)
                mp.specialSkills = element.specialSkills.toString();
            else mp.specialSkills = "";

            lst.push(mp);
        }
        return lst;
    }

    async SaveMemberGeneralInfo(memberID: string, body: MemberProfileBasicInfoModel) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}SaveMemberGeneralInfo/${memberID}`;
        let requestData = JSON.stringify(body);
        await firstValueFrom( this.httpClient.post(this.requestQuery, requestData,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async SaveMemberContactInfo(memberID: string, body: MemberProfileContactInfoModel) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}SaveMemberContactInfo/${memberID}`;
        let requestData = JSON.stringify(body);
        await firstValueFrom(this.httpClient.post(this.requestQuery, requestData,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async SaveMemberAboutInfo(memberID: string, aboutMe: string, activities: string, hobbies: string, specialSkills: string) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}SaveMemberPersonalInfo?memberID=${memberID}&aboutMe=${aboutMe}&activities=${activities}&interests=${hobbies}&specialSkills=${specialSkills}`;
        await firstValueFrom(this.httpClient.post(this.requestQuery, null,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async AddNewSchool(memberId: string, body: MemberProfileEducationModel) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}AddMemberSchool/${memberId}`;
        let requestData = JSON.stringify(body);
        await firstValueFrom( this.httpClient.post(this.requestQuery, requestData,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async UpdateSchool(memberId: string, body: MemberProfileEducationModel) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}UpdateMemberSchool/${memberId}`;
        let requestData = JSON.stringify(body);
        await firstValueFrom(this.httpClient.put(this.requestQuery, requestData,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async RemoveSchool(memberId: string, instId: string, instType: string) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}RemoveMemberSchool?memberID=${memberId}&instID=${instId}&instType=${instType}`;
        await firstValueFrom(this.httpClient.delete(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async AddWorkExperience(memberId: string, body: MemberProfileEmploymentModel) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}AddWorkExperience/${memberId}`;
        let requestData = JSON.stringify(body);
        await firstValueFrom(this.httpClient.post(this.requestQuery, requestData,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async UpdateWorkExperience(memberId: string, body: MemberProfileEmploymentModel) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}UpdateMemberWorkExperience/${memberId}`;
        let requestData = JSON.stringify(body);
        await firstValueFrom(this.httpClient.put(this.requestQuery, requestData,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async RemoveWorkExperience(memberId: string, empInfoId: string, compId: string) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}RemoveMemberWorkExperience?empInfoID=${empInfoId}&memberID=${memberId}&compID=${compId}`;
        await firstValueFrom(this.httpClient.delete(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }
        ));
    }

    async UploadProfilePhoto(memberId: string, file: File) {
        const fd = new FormData();
        fd.append('image', file);
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}UploadProfilePhoto/${memberId}`;
        await lastValueFrom(this.httpClient.post(this.requestQuery, fd,
            {
                headers: {
                    // 'Content-Type': 'text/plain; charset=utf-8',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }

        ));
    }

    async IsFriendByContactID(memberID: string, contactID: string) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}IsFriendByContactID/${memberID}/${contactID}`;
        let response = await lastValueFrom(this.httpClient.get(this.requestQuery,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }, responseType: 'text'
            }
        ));
        return response.toString();
    }
    
    async getVideoPlayList(memberID: string) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}GetVideoPlayList/${memberID}`;
        let response = lastValueFrom(this.httpClient.get<Array<YoutubePlayListModel>>(this.requestQuery
            ,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }));
        return response;
    } /*
  async getVideoPlayList(memberID: string) {
    this.requestQuery = `${this.MEMBERS_SERVICE_URI}GetVideoPlayList/${memberID}`;
    let response = await this.httpClient.get<Array<YoutubePlayListModel>>(this.requestQuery
      ,
      {
        headers: {
          'Content-Type': 'application/json',
          'authorization': 'Bearer ' + localStorage.getItem("access_token")
        }
      }).toPromise();
    return response;
  }*/


    async getVideosList(playerListID: string) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}GetVideosList/${playerListID}`;
        let response = await lastValueFrom(this.httpClient.get<Array<YoutubeVideosListModel>>(this.requestQuery
            ,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }));
        return response;
    }

    async getChannelId(memberID: string) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}GetYoutubeChannel/${memberID}`;
        let response = await firstValueFrom(this.httpClient.get(this.requestQuery
            ,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                },
                responseType: 'text'
            }));
        if (response == null)
            return "";
        else
            return response.toString();
    }

    async getInstagramURL(memberID: string) {
      this.requestQuery = `${this.MEMBERS_SERVICE_URI}GetInstagramURL/${memberID}`;
      let response = await firstValueFrom(this.httpClient.get(this.requestQuery
        ,
        {
          headers: {
            'Content-Type': 'application/json',
            'authorization': 'Bearer ' + localStorage.getItem("access_token")
          },
          responseType: 'text'
        }));
      if (response == null)
        return "";
      else
        return response.toString();
    }


    async saveChannelID(memberID: string, channelID: string) {

        let postBody: YoutubeDataModel = {
            memberID: memberID,
            channelID: channelID
        }
        let requestData = JSON.stringify(postBody);
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}SetYoutubeChannel`;
        await firstValueFrom(this.httpClient.put(this.requestQuery, requestData
            ,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }));
    }

    async saveInstagramURL(memberID: string, instagramURL: string) {
      let postBody = {
        memberID: memberID,
        instagramURL: instagramURL
      }
      let requestData = JSON.stringify(postBody);
      this.requestQuery = `${this.MEMBERS_SERVICE_URI}SetInstagramURL`;
     
      await firstValueFrom(this.httpClient.put(this.requestQuery, requestData
        ,
        {
          headers: {
            'Content-Type': 'application/json',
            'authorization': 'Bearer ' + localStorage.getItem("access_token")
          }
        }));
    }

    async getInstagramAccessToken(code: string) {
        this.requestQuery = `${this.MEMBERS_SERVICE_URI}GetInstagramAccessToken?code=${code}`;
        let response = await firstValueFrom(this.httpClient.get<Array<InstagramUserModel>>(this.requestQuery
            ,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'authorization': 'Bearer ' + localStorage.getItem("access_token")
                }
            }));
        return response;
    }

    // get Instagram user details and token
   instagramGetUserDetails(code) {
    try {
      const body = new HttpParams()
        .set('client_id', '315611252948079')
        .set('client_secret', 'f65cdcabd35a5eaebf2fc266003aba81')
        .set('grant_type', 'authorization_code')
        .set('redirect_uri', 'https://sportprofiles.net/sp/members/show-profile')
        .set('code', code)
        .set('auth', 'no-auth');
      return this.httpClient.post('https://api.instagram.com/oauth/access_token', body.toString(), {
          headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
        })
        .pipe(
          tap(res => {
            // ----------->>>>>>   you get the token here   <<<<<---------------
            console.log(res)
          }),
          catchError(this.handleError<any>('instagram_auth'))
        );
     } catch (err) {
         console.log(err);
      return err;
     }
   }
  
  // Handle error
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);
  
      // Let the app keep running by returning an empty result.
      return of(result );
    };
  }


    private truncate(value: string, maxLength: number) {
        if ((value == null) || (value == undefined) || (value.length == 0)) return value;
        return (value.length <= maxLength ? value : value.substring(0, maxLength))
    }

    private extractData(res: Response) {
        let body = res
        return body || [];
    }
}

export class PostModel {
    memberID: string;
    postID: string;
    postMsg: string;
}

export class YoutubeDataModel {
    memberID: string;
    channelID: string;
}

export class IgRequestModel {
    client_id: string;
    client_secret: string;
    grant_type: string;
    redirect_uri:string;
    code:string;
}


