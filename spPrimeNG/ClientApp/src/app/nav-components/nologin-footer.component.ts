import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-nologin-footer',
  templateUrl: './nologin-footer.component.html',
  styleUrls: ['./nologin-footer.component.css']
})
export class NoLoginFooterComponent implements OnInit {

  public currentYear = new Date().getFullYear().toString();
  public appLogoText = environment.appLogoText;
  public companyName = environment.companyName;

  constructor() {/* nothing here */}

  ngOnInit() {/* no inits */}

}
