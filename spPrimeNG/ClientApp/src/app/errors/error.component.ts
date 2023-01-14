import { Component, Injector } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { SessionMgtService } from '../services/session-mgt.service';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent {

  errorType: string;
  typeOfErrorMsg: string = "";

  constructor(private inj: Injector, private route: ActivatedRoute, public session: SessionMgtService, private router: Router) { }

  ngOnInit() {

    this.route.queryParams.subscribe((params) => {
      this.errorType = params.errType;
    });
    const router = this.inj.get(Router);
    if (this.errorType == "http") {
      this.typeOfErrorMsg = "Please check your internet connection or WIFI settings and then try again.";
      router.navigate(['/errors'], { queryParams: { errType: this.errorType } });
    }
    else if (this.errorType == "BadToken") {
      this.typeOfErrorMsg = "It looks like your session may have timed out. <a href (click)='logOut()'>Please click here to login again./>";
      router.navigate(['/errors'], { queryParams: { errType: this.errorType } });
    }
     else {  //general or type or any others.
       this.typeOfErrorMsg = "We are going to fix it up and and have things back to normal soon. Please try again a bit later.";
       router.navigate(['/errors'], { queryParams: { errType: this.errorType } });
     }
  }

  logout() {
    localStorage.removeItem('access_token');
    this.session.setSessionVar('isUserLogin', null);
    this.session.setSessionVar('userID', null);
    this.session.setSessionVar('userEmail', null);
    this.session.setSessionVar('userImage', null);
    this.session.setSessionVar('pwd', null);
    this.router.navigate(['/']);
  }

}
