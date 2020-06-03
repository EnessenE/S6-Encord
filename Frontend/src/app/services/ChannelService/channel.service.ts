import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticationService } from '../AuthService/auth.service';
import { Observable } from 'rxjs';
import { Guild } from 'src/app/models/guild';
import { environment } from 'src/environments/environment';
import { Channel } from 'src/app/models/channel';
import { TextChannel } from 'src/app/models/text-channel';

@Injectable({
  providedIn: 'root'
})
export class ChannelService {
  constructor(private http: HttpClient,
    private authService: AuthenticationService) { }

  public getAllChannelsOnGuild(id: string): Observable<Channel[]> {
    return this.http.get<Channel[]>(environment.api_base + `/channel/guild/` + id, { headers: this.authService.getHeaders() });
  }

  public getTextChannel(id: string): Observable<TextChannel> {
    return this.http.get<TextChannel>(environment.api_base + '/channel/text/' + id, { headers: this.authService.getHeaders() });
  }

  public createChannel(data: Channel): Observable<Channel> {
    return this.http.post<Channel>(environment.api_base + `/channel/`, data, { headers: this.authService.getHeaders() });
  }

  public getAllChannelTypes(): Observable<string[]> {
    return this.http.get<string[]>(environment.api_base + `/channel/types`, { headers: this.authService.getHeaders() });
  }
  public deleteChannel(channel: Channel): Observable<boolean> {
    return this.http.request<boolean>('delete', environment.api_base + `/channel/`,  { headers: this.authService.getHeaders(), body: channel});
  }
  
}
