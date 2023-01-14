import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MembersService } from '../../services/data/members.service';
import { IOrganizationsService } from '../../services/data/organizations.service';
import { SessionMgtService } from '../../services/session-mgt.service';
import { MemberProfileEmploymentModel } from 'src/app/models/members/profile-employment.model';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { CompanyBySectorIndustryModel } from 'src/app/models/organization/company-by-sec-industry.model';

@Component({
    selector: 'employment-info',
    templateUrl: './employment-info.component.html',
    styleUrls: ['./employment-info.component.css']
})
export class EmploymentInfoComponent implements OnInit {

    public show: boolean = false;
    public showErrMsg: boolean = false;
    public isSaving = false;
    public noEmployments: boolean = false;
    public years: any;

    title: string;
    sector: string;
    companyID: string;
    companyName: string;
    empInfoId: string;

    businessSectorsList = new Array<string>();
    industriesList = new Array<string>();
    companiesList = new Array<CompanyBySectorIndustryModel>();

    modHand: NgbModalRef;

    empInfoEdit: MemberProfileEmploymentModel = {
        EmpInfoID: "",
        City: "",
        State: "select",
        StartMonth: "select",
        StartYear: "select",
        EndMonth: "select",
        EndYear: "select",
        CurrentlyWorkedHere: "",
        companyID: "",
        companyName: "",
        companyImage: "",
        companyAddress: "",
        title: "",
        datesWorked: "",
        Description: "",
        Email: "",
        Industry: "",
        IPOYear: "",
        JobDesc: "",
        Sector: "",
        summaryQuote: "",
        Symbol: "",
        website: "",
    }

    empInfo = new MemberProfileEmploymentModel();

    public constructor(public session: SessionMgtService, private route: ActivatedRoute, private router: Router,
        private membersSvc: MembersService, private orgSvc: IOrganizationsService,
        public modalService: NgbModal) {
    }

    memberID: string;
    employmentInfoModel: MemberProfileEmploymentModel[];

    ngOnInit() {
        this.memberID = this.session.getSessionVal('userID');
        this.getBusinessSectors();
        this.empInfo.Sector = "select";
        this.empInfo.Industry = "select";
        this.empInfo.companyID = "select";
        this.empInfo.State = "select"
        this.empInfo.StartMonth = "select"
        this.empInfo.StartYear = "select";
        this.empInfo.EndMonth = "select";
        this.empInfo.EndYear = "select";
        this.years = this.getEmploymentYears();
        this.getEmploymentInfo();
    }

    async getBusinessSectors() {
        this.businessSectorsList = await this.orgSvc.getCompanySectors();
    }

    async getEmploymentInfo() {
        this.employmentInfoModel = await this.membersSvc.getMemberEmploymentInfo(this.memberID.toString());
        if (this.employmentInfoModel.length == 0) {
            this.noEmployments = true;
        }
        else {
            this.noEmployments = false;
        }
    }

    jumpToAddWorkExperience(modal) {
        this.modHand = this.modalService.open(modal);
        return false;
    }

    jumpToEditExperience(editModal, companyId: string, empInfoId: string, compName: string, title: string, desc: string, city: string, state: string,
        startMonth: string, startYear: string, endMonth: string, endYear: string) {
        this.companyName = compName;
        this.empInfoEdit.companyID = companyId;
        this.empInfoEdit.EmpInfoID = empInfoId;
        this.empInfoEdit.title = title;
        this.empInfoEdit.JobDesc = desc;
        this.empInfoEdit.City = city;
        this.empInfoEdit.State = state;
        this.empInfoEdit.StartMonth = startMonth;
        this.empInfoEdit.StartYear = startYear;
        this.empInfoEdit.EndMonth = endMonth;
        this.empInfoEdit.EndYear = endYear;
        this.modHand = this.modalService.open(editModal);
        return false;
    }

    jumpToRemoveExperience(removeModal, compId: string, empInfoId: string, name: string) {
        this.companyName = name;
        this.companyID = compId;
        this.empInfoId = empInfoId;

        this.modHand = this.modalService.open(removeModal);
        return false;
    }

  onSectorChange(ev:any) {
        let sector = (ev.target as HTMLInputElement).value;
        this.getIndustries(sector);
        this.empInfo.Industry = "select";
        this.empInfo.companyID = "select";
        this.sector = sector;
    }

  onIndustryChange(sector: string, ev: any) {
        let industry = (ev.target as HTMLInputElement).value;
        this.getCompanies(sector, industry);
        this.empInfo.companyID = "select";
    }

    async getIndustries(sec: string) {
        this.industriesList = await this.orgSvc.getIndustries(sec);
    }

    async getCompanies(sec: string, industry: string) {
        this.companiesList = await this.orgSvc.getCompanies(sec, industry);
    }

    async doAddWorkExperience(modal) {

        this.isSaving = true;
        await this.membersSvc.AddWorkExperience(this.memberID, this.empInfo);
        this.getEmploymentInfo();
        this.isSaving = false;
        this.modalService.dismissAll(modal);
    }

    async doUpdateWorkExperience(form) {

        this.isSaving = true;
        await this.membersSvc.UpdateWorkExperience(this.memberID, this.empInfoEdit);
        this.getEmploymentInfo();
        this.isSaving = false;
        this.modalService.dismissAll(form);
    }

    async doRemoveWorkExperience(form) {

        this.isSaving = true;
        await this.membersSvc.RemoveWorkExperience(this.memberID, this.empInfoId, this.companyID);
        this.getEmploymentInfo();
        this.isSaving = false;
        this.modalService.dismissAll(form);
    }

    onCompanyChange(event: Event) {
        this.companyName = event.target['options']
        [event.target['options'].selectedIndex].text;
        this.empInfo.companyName = this.companyName;
    }

    goToLink(url: string) {
        window.open(url + '/', '_blank');
        return false;
    }

  public getEmploymentYears() {
    let baseYear = 1900;
    let years = [];
    for (let i = baseYear; i <= 2040; i++) {
      years.push(i);
    }
    return years
  }
}
