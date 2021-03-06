import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { AuthenticationService } from './services/AuthService/auth.service';
import { LoadingComponent } from './parts/loading/loading.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { HeaderComponent } from './parts/header/header.component';
import { GuildsComponent } from './parts/guilds/guilds.component';
import { UserlistComponent } from './parts/userlist/userlist.component';
import { GuildviewComponent } from './parts/guildview/guildview.component';
import { ChannelsComponent } from './parts/channels/channels.component';
import { ChatviewComponent } from './parts/chatview/chatview.component';
import { TopbarComponent } from './parts/topbar/topbar.component';
import { RegisterComponent } from './pages/register/register.component';
import { CreateguildComponent } from './parts/createguild/createguild.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CreatechannelComponent } from './parts/createchannel/createchannel.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { GuildsettingsComponent } from './parts/guildsettings/guildsettings.component';
import { ChannelsettingsComponent } from './parts/channelsettings/channelsettings.component';
import { NgxBootstrapAlertNotificationModule } from "@benevideschissanga/ngx-bootstrap-alert-notification"
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LoadingComponent,
    DashboardComponent,
    HeaderComponent,
    GuildsComponent,
    UserlistComponent,
    GuildviewComponent,
    ChannelsComponent,
    ChatviewComponent,
    TopbarComponent,
    RegisterComponent,
    CreateguildComponent,
    CreatechannelComponent,
    GuildsettingsComponent,
    ChannelsettingsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    MatDialogModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
    NgxBootstrapAlertNotificationModule.forRoot()
  ],
  providers: [
    AuthenticationService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
