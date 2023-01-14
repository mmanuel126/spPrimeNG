
import { Component, OnInit } from '@angular/core';
import { MembersService } from '../services/data/members.service';
import { IAuthService } from '../services/auth.service'
import { SessionMgtService } from '../services/session-mgt.service';
import { Router, ActivatedRoute } from "@angular/router";
import { throwError } from 'rxjs';
import { MessagesService } from '../services/data/messages.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { environment } from '../../environments/environment';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-complete-register',
  templateUrl: './complete-register.component.html',
  styleUrls: ['./complete-register.component.css'],
})
export class CompleteRegisterComponent implements OnInit {

  public webSiteDomain = environment.webSiteDomain;
  public show: boolean = false;
  public terms: boolean = false;

  public appLogoText = environment.appLogoText;
  public appName = environment.appName;
  public companyName = environment.companyName;

  result: string = "";
  public isLoading = false;
  userID: string;
  modHand: NgbModalRef;

  public constructor(public ngbMod: NgbModal, public members: MembersService,
    public authSvc: IAuthService, public session: SessionMgtService, private router: Router, public msgSvc: MessagesService
    , private route: ActivatedRoute) {
  }

  ngOnInit() {
    if (this.session.getSessionVal('isUserLogin') == "true") {
      this.router.navigate(['/home']);
    }
  }

  onCheckboxChange(e) {
    if (e.target.checked) {
      this.terms = true;
    } else {
      this.terms = false;
    }
  }

  async completeRegister() {

    throwError(null);
    this.isLoading = true;
    let code = "";
    let email = "";

    this.route.queryParams.pipe(
      filter(params => params.code))
      .subscribe(params => {
        code = params.code;
      });

    this.route.queryParams.pipe(
      filter(params => params.email))
      .subscribe(params => {
        email = params.email;
      });

    let response = await this.authSvc.validateNewRegisteredUser(email, code);

    if (response.memberID != null) {

      //change status from newly registered user to existing user
      this.authSvc.setMemberStatus(response.memberID, "2");

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

      this.session.setSessionVar('pwd', code);
      this.result = "found";
      this.router.navigate(['/home']);
    }
    else {
      this.session.setSessionVar('isUserLogin', 'false');
      this.result = "notfound";
      this.isLoading = false;
    }
  }

  async showOpenPopup(openModal) {
    this.modHand = this.ngbMod.open(openModal);
    return false;
  }
}

