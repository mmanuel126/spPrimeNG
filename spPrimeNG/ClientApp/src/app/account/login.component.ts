import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Login } from '../models/login.model';
import { MembersService } from '../services/data/members.service';
import { IAuthService } from '../services/auth.service'
import { SessionMgtService } from '../services/session-mgt.service';
import { Router } from "@angular/router";
import { throwError } from 'rxjs';
import { MessagesService } from '../services/data/messages.service';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  @ViewChild('closebutton') closebutton;
  public webSiteDomain = environment.webSiteDomain;
  public currentYear = new Date().getFullYear().toString();
  public appLogoText = environment.appLogoText;
  public companyName = environment.companyName;
  result: string = "";
  public isLoading = false;
  userID: string;

  login: Login = {
    email: "",
    password: "",
  }

  public constructor(public members: MembersService,
    public authSvc: IAuthService, public session: SessionMgtService, private router: Router, public msgSvc: MessagesService) {
  }

  ngOnInit() {
    if (this.session.getSessionVal('isUserLogin') == "true") {
      this.router.navigate(['/home']);
    }
  }

  @ViewChild("videoPlayer", { static: false }) videoplayer: ElementRef;
  isPlay: boolean = false;
  toggleVideo() {
    this.videoplayer.nativeElement.play();
  }

  async loginUser() {
    //throwError(null);
    this.isLoading = true;
    let response = await this.authSvc.login(this.login);
    if (response.memberID != "") {

      if (response.currentStatus == "2") //active
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
        this.router.navigate(['/home']);
      }
      else if (response.currentStatus == "3") //deactivated
      {
        this.result = "deactivated";
        this.isLoading = false;
      }
    }
    else {
      this.session.setSessionVar('isUserLogin', 'false');
      this.result = "notfound";
      this.isLoading = false;
    }
  }

  closePopup() {
    this.videoplayer.nativeElement.pause();
    this.closebutton.nativeElement.click();
  }

  showModalBox: boolean = false;

  showPreviewPopup() {
    if (this.videoplayer === undefined) {
      //do nothing. video is set to autostart and loop
    }
    else {
      this.videoplayer.nativeElement.currentTime = 0; //reset video to start at beggining
      this.videoplayer.nativeElement.play(); //then play it.
    }
    this.showModalBox = true;
    return false;
  }
}






