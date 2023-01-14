import { Component, OnInit } from '@angular/core';
import { SessionMgtService } from '../services/session-mgt.service';
import { ConnectionsService } from '../services/data/connections.service';
import { SearchResultModel } from '../models/contacts/contact-model';
import { Subject, Observable } from 'rxjs';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { environment } from '../../environments/environment';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css']
})
export class SearchResultsComponent implements OnInit {

  public memberId: string;
  public contactCnt: number = 0;
  public contactInfoList: SearchResultModel[];
  public spinner: boolean = false;
  public contactId = "";

  showErrMsg: boolean = false;
  errMsg: string;

  modHand: NgbModalRef;

  searchModel = new SearchModel();
  autoCompleteModel = new AutoCompleteModel();

  public connections: Observable<any[]>;
  //private searchContacts = new Subject<string>();
  public contactName = '';
  public flag: boolean = true;
  public memberImagesUrlPath: string;

  constructor(private route: ActivatedRoute, private session: SessionMgtService, public contactSvc: ConnectionsService, public ngbMod: NgbModal) {
    this.memberImagesUrlPath = environment.memberImagesUrlPath;
  }

  ngOnInit(): void {
    this.memberId = this.session.getSessionVal('userID');
    this.route.queryParams.subscribe((params) => {
      this.searchModel.key = params.searchText;
    });
    this.getSearchResults();
  }

  async getSearchResults() {

    this.contactInfoList = await this.contactSvc.getSearchResults(this.memberId, this.searchModel.key);
    this.spinner = true;
    if (this.contactInfoList != null) {
      this.contactCnt = this.contactInfoList.length;
    }
    this.spinner = false;
  }
}

export class SearchModel {
  key: string;
}

export class AutoCompleteModel {
  name: string;
  id: string;
}
