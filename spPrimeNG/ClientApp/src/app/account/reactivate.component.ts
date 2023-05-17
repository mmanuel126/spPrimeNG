import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { IAuthService } from '../services/auth.service';
import { Login } from '../models/login.model';
import { environment } from '../../environments/environment'
import { MembersService } from '../services/data/members.service';
import { SessionMgtService } from '../services/session-mgt.service';
import { throwError } from 'rxjs';
import { MessagesService } from '../services/data/messages.service';


@Component({
  selector: 'app-reactivate',
  templateUrl: './reactivate.component.html',
  styleUrls: ['./reactivate.component.css'],
})
export class ReactivateComponent implements OnInit {

  public webSiteDomain = environment.webSiteDomain;
  public appLogoText = environment.appLogoText;
  public companyName = environment.companyName;

  result: string = "";
  public isLoading = false;
  userID: string;

  login: Login = {
    email: "",
    password: "",
  }

  public show: boolean = false;

  constructor(
    public members: MembersService, public authSvc: IAuthService, public session: SessionMgtService, private router: Router, public msgSvc: MessagesService) { }

  formData: FormData = {
    email: "",
  }

  ngOnInit() {
    if (this.session.getSessionVal('isUserLogin') == "true") {
      this.router.navigate(['/home']);
    }
  }

  async reactivateUser() {
    this.isLoading = true;
    let response = await this.authSvc.login(this.login);
    if (response.memberID != "") {

      if (response.currentStatus == "3") //deactivated
      {
        //reset active flag to activated
        await this.authSvc.setMemberStatus(response.memberID, "2");
      }

      if (response.currentStatus == "2" || response.currentStatus == "3") //active or not check this if you land on this page
      {
        this.session.setSessionVar('isUserLogin', 'true');
        this.session.setSessionVar('userID', response.memberID);
        this.userID = response.memberID;
        this.session.setSessionVar('userEmail', response.email);
        this.session.setSessionVar('userTitle', response.title);
        this.session.setSessionVar('userName', response.name);
        localStorage.setItem("access_token", response.accessToken);
        if (response.picturePath != "") {
          this.session.setSessionVar('userImage', response.picturePath);
        }
        else {
          this.session.setSessionVar('userImage', "default.png");
        }
        this.session.setSessionVar('pwd', this.login.password);
        this.result = "found";
        this.session.setSessionVar('reactivate', "yes");
        this.router.navigate(['/home']);
      }
    }
    else {
      this.session.setSessionVar('isUserLogin', 'false');
      this.result = "notfound";
      this.isLoading = false;
    }
  }
}

export class FormData {
  email: string;
}

