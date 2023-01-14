import { Injectable } from '@angular/core';
import {
  HttpRequest, HttpErrorResponse,
  HttpHandler,
  HttpEvent, HttpClient,
  HttpInterceptor
} from '@angular/common/http';

import { AuthService } from '../services/auth.service';
import { Observable, mergeMap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SessionMgtService } from '../services/session-mgt.service';
import { Router } from '@angular/router';
import { UserModel } from '../models/user.model';
import { environment } from '../../environments/environment';
import { catchError,  } from 'rxjs/operators';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private router: Router, public auth: AuthService, public session: SessionMgtService, public httpClient: HttpClient) { }

  ACCOUNT_SERVICE_URI: string = environment.webServiceURL + "account/";

  helper = new JwtHelperService();
  res: UserModel;

  headers = new Headers({
    'Content-Type': 'application/json',
    'authorization': 'Bearer ' + localStorage.getItem("access_token")
  });

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    console.log("intercepted request...");

    //clone the request to add the new header 
    const authReq = request.clone({ headers: request.headers.set('authorization', 'Bearer ' + localStorage.getItem("access_token")) });

    console.log("Sending request with new header now ...");

    //send the newly created request

    return next.handle(authReq)
      .pipe(

      catchError(err => {
        //onError
        console.log(err);
        if (err instanceof HttpErrorResponse) {
          console.log(err.status);
          console.log(err.statusText);
          if (err.status === 401) {
            let url = `${this.ACCOUNT_SERVICE_URI}refreshLogin?accessToken=` + localStorage.getItem("access_token");

            return this.httpClient.post(url, null)
              .pipe(mergeMap(

                (data: any) => {
                  //if reload successful update tokens
                  if (data != null) {
                    //update token
                    localStorage.setItem("access_token", data.accessToken);
                    //clone our field request ansd try to resend it
                    request = request.clone({ headers: request.headers.set('authorization', 'Bearer ' + localStorage.getItem("access_token")) });

                    return next.handle(request);
                  }
                  else {
                    //something terrible happened - logout from accout 
                    this.router.navigate(['/errors'], { queryParams: { errType: "BadToken" } });
                    return (data);
                  }
                }
              ));
          }
          else {
            //other errors 
            console.log(err.status);
          }
        }
      })) as any; 
  } 
}
