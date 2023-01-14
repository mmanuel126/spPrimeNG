import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { SessionMgtService } from '../services/session-mgt.service';

@Component({
  selector: 'privacy-setting',
  templateUrl: './privacy-setting.component.html',
  styleUrls: ['./privacy-setting.component.css']
})
export class PrivacySettingComponent implements OnInit {

  constructor(public session: SessionMgtService, private route: ActivatedRoute) { }

  ngOnInit(): void {}

}
