import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { IAuthService } from '../services/auth.service';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-forgotpwd',
  templateUrl: './forgotpwd.component.html',
  styleUrls: ['./forgotpwd.component.css'],
})
export class ForgotPasswordComponent implements OnInit {

  public webSiteDomain = environment.webSiteDomain;
  public appLogoText = environment.appLogoText;
  public companyName = environment.companyName;

  public show: boolean = false;

  constructor(private router: Router, private authSvrc: IAuthService) { }

  formData: FormData = {
    email: "",
  }

  ngOnInit() {
    // TO DO document why this method 'ngOnInit' is empty
  
  }

  async forgotPassword() {
    let e = this.formData.email;
    this.show = true;
    await this.authSvrc.resetPassword(e);
    this.router.navigate(['/resetpwd'], { queryParams: { email: e } });
  }
}

export class FormData {
  email: string;
}

