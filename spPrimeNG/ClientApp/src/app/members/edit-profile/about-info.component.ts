import { Component, OnInit } from '@angular/core';
import { MemberProfileAboutInfoModel } from '../../models/members/profile-member.model';
import { ActivatedRoute, Router } from '@angular/router';
import { MembersService } from '../../services/data/members.service';
import { SessionMgtService } from '../../services/session-mgt.service';
import { MemberProfileAboutModel } from 'src/app/models/members/profile-about.model';

@Component({
    selector: 'about-info',
    templateUrl: './about-info.component.html',
    styleUrls: ['./about-info.component.css']
})
export class AboutInfoComponent implements OnInit {

    public show: boolean = false;
    public showErrMsg: boolean = false;
    public isSaving = false;

    aboutInfo: MemberProfileAboutInfoModel = {
        aboutMe: "",
        activities: "",
        hobbies: "",
        specialSkills: "",
    }

    public constructor(public session: SessionMgtService, private route: ActivatedRoute, private router: Router,
        private membersSvc: MembersService) {
    }

    memberID: string;
    aboutInfoModel: MemberProfileAboutModel[];

    ngOnInit() {
        this.memberID = this.session.getSessionVal('userID');
        this.getAboutInfo(this.memberID);
    }

    async getAboutInfo(memberId: string) {
      this.aboutInfoModel = await this.membersSvc.getAboutMeInfo(memberId);
        if (this.aboutInfoModel.length != 0) {
            this.aboutInfo.aboutMe = this.aboutInfoModel[0].aboutMe;
            this.aboutInfo.activities = this.aboutInfoModel[0].activities;
            this.aboutInfo.hobbies = this.aboutInfoModel[0].interests;
            this.aboutInfo.specialSkills = this.aboutInfoModel[0].specialSkills;
        }
    }

    async saveAboutInfo() {
        this.isSaving = true;
        await this.membersSvc.SaveMemberAboutInfo(this.memberID, this.aboutInfo.aboutMe, this.aboutInfo.activities,
            this.aboutInfo.hobbies, this.aboutInfo.specialSkills);
        this.isSaving = false;
    }
}
