import { filter } from 'rxjs/operators';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../environments/environment';

@Component({
  selector: 'confirm-register',
  templateUrl: './confirm-register.component.html',
})

export class ConfirmRegisterComponent {

  email: string;
  public appName = environment.appName;

  public constructor(private route: ActivatedRoute) { }

  ngOnInit() {

    this.route.queryParams.pipe(
      filter(params => params.email))
      .subscribe(params => {
        this.email = params.email;
      });
  }
}  
