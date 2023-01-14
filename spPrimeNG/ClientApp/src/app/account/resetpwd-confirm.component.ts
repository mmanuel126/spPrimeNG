import { Component } from '@angular/core';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-resetpwd-confirm',
  templateUrl: './resetpwd-confirm.component.html',
  styleUrls: ['./resetpwd-confirm.component.css'],
})

export class ResetpwdConfirmComponent {
  public webSiteDomain = environment.webSiteDomain;
  public appLogoText = environment.appLogoText;
  public constructor() { /* to do document why this constructor is empty */ }

}
