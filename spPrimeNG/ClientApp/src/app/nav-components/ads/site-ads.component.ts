import { Component, OnInit } from '@angular/core';
import { AdsService } from '../../services/ads.service';
import { AdsModel } from '../../models/ads.model';
import { SessionMgtService } from '../../services/session-mgt.service';
import { environment } from 'src/environments/environment';

@Component({
   selector: 'app-site-ads',
   templateUrl: './site-ads.component.html',
   styleUrls: ['./site-ads.component.scss']
})
export class SiteAdsComponent implements OnInit {
   public webSiteDomain = environment.webSiteDomain;
   userId:string;
   constructor(public adsSvc: AdsService, public session: SessionMgtService) { 
      this.userId = this.session.getSessionVal('userID');
   }
   adsList : Promise<AdsModel[]>; 

   ngOnInit() { 
      this.adsList = this.getAds("SiteGuide");
   }

   async getAds(type: string) {
      return  await this.adsSvc.getAds(type);
   }

}

