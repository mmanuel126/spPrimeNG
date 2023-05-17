import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDialogModule } from '@angular/material/dialog';
import { YouTubePlayerModule } from '@angular/youtube-player';
import { NgxPaginationModule } from 'ngx-pagination';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { CommonModule, DatePipe, PathLocationStrategy } from '@angular/common';

import { PerfectScrollbarModule, PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

import { GlobalErrorHandler } from './services/global-error.handler';

import { LoggerModule, NgxLoggerLevel, NGXLogger, NGXLoggerMapperService } from 'ngx-logger';
import { NgxPageScrollCoreModule } from 'ngx-page-scroll-core';

//* main or app component */
import { AppComponent } from './app.component';
import { } from './nav-components';

/* custom nav components */
import { NavbarComponent } from './nav-components/navbar.component'
import { FooterComponent } from './nav-components/footer.component';
import { NoLoginFooterComponent } from './nav-components/nologin-footer.component';
import { SiteAdsComponent } from './nav-components/ads/site-ads.component';
import { SponsoredAdsComponent } from './nav-components/ads/sponsored-ads.component';
import { GoogleAdsComponent } from './nav-components/ads/google-ads.component';

/* account components */
import { LoginComponent } from './account/login.component';
import { ForgotPasswordComponent } from './account/forgotpwd.component';
import { ChangepwdComponent } from './account/changepwd.component';
import { RegisterComponent } from './account/register.component';
import { CompleteRegisterComponent } from './account/complete-register.component';
import { ConfirmRegisterComponent } from './account/confirm-register.component';
import { ResetpwdComponent } from './account/resetpwd.component';
import { ResetpwdConfirmComponent } from './account/resetpwd-confirm.component';

/* home components */
import { HomeComponent } from './home/home.component';

/* member show and edit profile */
import { ShowProfileComponent } from './members/show-profile/show-profile.component';
import { EditProfileComponent } from './members/edit-profile/edit-profile.component';
import { UserInfoComponent } from "./members/edit-profile/user-info.component";
import { ContactInfoComponent } from "./members/edit-profile/contact-info.component";
import { EducationInfoComponent } from "./members/edit-profile/education-info.component";
import { EmploymentInfoComponent } from "./members/edit-profile/employment-info.component";
import { AboutInfoComponent } from "./members/edit-profile/about-info.component";

/* connections */
import { MyConnectionsComponent } from './connections/my-connections.component';
import { RequestsComponent } from './connections/requests.component';
import { FindConnectionsComponent } from './connections/find-connections.component';
import { PeopleIFollowComponent } from './connections/people-i-follow.component';
import { PeopleFollowingMeComponent } from './connections/people-following-me.component';
import { SearchResultsComponent } from './connections/search-results.component';

/* messages */
import { ViewMessagesComponent } from './messages/view-messages/view-messages.component';
import { ViewNotificationsComponent } from './messages/view-notifications/view-notifications.component';

/* account settings */
import { AccountSettingComponent } from "./settings/account-setting.component";
import { AccountSettingNameComponent } from "./settings/account-setting-name.component";
import { AccountSettingEmailComponent } from "./settings/account-setting-email.component";
import { AccountSettingPasswordComponent } from "./settings/account-setting-password.component";
import { AccountSettingSecQuetionsComponent } from "./settings/account-setting-secQuestions.component";
import { AccountSettingNotificationsComponent } from "./settings/account-setting-notifications.component";
import { AccountSettingDeactivateComponent } from "./settings/account-setting-deactivate.component";
import { AccountSettingChangePhotoComponent } from "./settings/account-setting-change-photo.component";
import { PrivacySettingComponent } from "./settings/privacy-setting.component";
import { PrivacySettingProfileComponent } from "./settings/privacy-setting-profile.component";
import { PrivacySettingSearchComponent } from "./settings/privacy-setting-search.component";
import { ReactivateComponent } from "./account/reactivate.component";

/* error component */
import { ErrorComponent } from './errors/error.component';


/* services */
import { SessionMgtService } from './services/session-mgt.service';
import { AdsService, IAdsService } from './services/ads.service';
import { MembersService } from './services/data/members.service';
import { CommonService, ICommonService } from './services/common.service';
import { AuthService, IAuthService } from './services/auth.service';
import { MessagesService } from './services/data/messages.service';
import { OrganizationsService, IOrganizationsService } from './services/data/organizations.service';
import { SettingsService, ISettingsService } from './services/data/settings.service';
import { ConnectionsService } from './services/data/connections.service';
import { NetworksService, INetworksService } from './services/data/networks.service';
import { EventsService, IEventsService } from './services/data/events.service';
import { StateService } from './services/state.service';
import { ErrorLogService } from './services/error-log.service';
import { AuthGuardService } from './services/auth-guard-service';

// directives
import { BootstrapTabDirective } from './directives/bootstrap-tab.directive';
import { SelectRequiredValidatorDirective } from './directives/select-required-validator.directive';
import { EqualValidatorDirective } from './directives/confirm-pwd-validator.directive';

//interceptors
import { TokenInterceptor } from './services/token.interceptor';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [
    AppComponent,

    /* custom nav components */
    NavbarComponent, FooterComponent, NoLoginFooterComponent,
    SiteAdsComponent, SponsoredAdsComponent, GoogleAdsComponent,

    /* account */
    LoginComponent,
    RegisterComponent,
    RegisterComponent, ForgotPasswordComponent, ChangepwdComponent,
    CompleteRegisterComponent, ConfirmRegisterComponent, ResetpwdComponent, ResetpwdConfirmComponent,

    /* home */
    HomeComponent,

    /* members */
    ShowProfileComponent,
    EditProfileComponent, UserInfoComponent,
    ContactInfoComponent, EducationInfoComponent, EmploymentInfoComponent, AboutInfoComponent, 

    /* connections */
    MyConnectionsComponent, RequestsComponent, FindConnectionsComponent,
    PeopleFollowingMeComponent, PeopleIFollowComponent, SearchResultsComponent,

    /* messages */
    ViewMessagesComponent, ViewNotificationsComponent,

    /* account settings */
    AccountSettingComponent,
    AccountSettingNameComponent,
    AccountSettingEmailComponent,
    AccountSettingPasswordComponent,
    AccountSettingSecQuetionsComponent,
    AccountSettingNotificationsComponent,
    AccountSettingDeactivateComponent,
    AccountSettingChangePhotoComponent,
    PrivacySettingComponent,
    PrivacySettingProfileComponent,
    PrivacySettingSearchComponent,
    ReactivateComponent,

    /* errors */
    ErrorComponent,

    /* directives */
    SelectRequiredValidatorDirective, EqualValidatorDirective, BootstrapTabDirective,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    HttpClientModule,
    FormsModule, ReactiveFormsModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatSelectModule,
    MatTabsModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    CarouselModule,
    PaginationModule,
    NgxPaginationModule,
    YouTubePlayerModule,
    PerfectScrollbarModule,
    NgxPageScrollCoreModule.forRoot({ duration: 1600 }),

    LoggerModule.forRoot({
      serverLoggingUrl: 'http://localhost:5005/services/common/logs',
      level: NgxLoggerLevel.ERROR,
      serverLogLevel: NgxLoggerLevel.ERROR,
      disableConsoleLogging: false
    }),

    RouterModule.forRoot([

      //account route
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'register', component: RegisterComponent },
      { path: 'complete-register', component: CompleteRegisterComponent },
      { path: 'forgotpwd', component: ForgotPasswordComponent },
      { path: 'confirm-register', component: ConfirmRegisterComponent },
      { path: 'resetpwd', component: ResetpwdComponent },
      { path: 'changepwd', component: ChangepwdComponent },
      { path: 'resetpwd-confirm', component: ResetpwdConfirmComponent },
      { path: 'reactivate', component: ReactivateComponent },

      //home route
      { path: 'home', component: HomeComponent, canActivate: [AuthGuardService] },

      //members route
      { path: 'members/show-profile', component: ShowProfileComponent },
      { path: 'members/edit-profile', component: EditProfileComponent },

      //connections routes
      { path: 'connections/my-connections', component: MyConnectionsComponent },
      { path: 'connections/requests', component: RequestsComponent },
      { path: 'connections/find-connections', component: FindConnectionsComponent },
      { path: 'connections/people-following-me', component: PeopleFollowingMeComponent },
      { path: 'connections/people-i-follow', component: PeopleIFollowComponent },
      { path: 'connections/search-results', component: SearchResultsComponent },

      //messages route
      { path: 'messages/view-messages', component: ViewMessagesComponent },
      { path: 'messages/view-notifications', component: ViewNotificationsComponent },

      //settings route
      { path: 'settings/account-setting', component: AccountSettingComponent, canActivate: [AuthGuardService] },
      { path: 'settings/privacy-setting', component: PrivacySettingComponent, canActivate: [AuthGuardService] },

      //errors
      { path: 'errors', component: ErrorComponent },
    ])
  ],
  providers: [
    SessionMgtService, AdsService, MembersService, CommonService,
    ConnectionsService, NetworksService, EventsService, CommonService,
    MessagesService, AuthService, StateService, AdsService,
    ErrorLogService, AuthGuardService,
    { provide: IAuthService, useClass: AuthService },
    { provide: IAdsService, useClass: AdsService },
    { provide: ICommonService, useClass: CommonService },
    { provide: ISettingsService, useClass: SettingsService },
    { provide: IOrganizationsService, useClass: OrganizationsService },
    { provide: INetworksService, useClass: NetworksService },
    { provide: IEventsService, useClass: EventsService },
    NGXLogger, NGXLoggerMapperService, 
    DatePipe, Title,

    // register global error handler
    { provide: ErrorHandler, useClass: GlobalErrorHandler },
    //{ provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },

    {
      provide:
        PERFECT_SCROLLBAR_CONFIG,
      useValue:
        DEFAULT_PERFECT_SCROLLBAR_CONFIG,
      useClass:
        PathLocationStrategy
    },
  ],
  bootstrap: [AppComponent],
  entryComponents: [EditProfileComponent],
})
export class AppModule { }
