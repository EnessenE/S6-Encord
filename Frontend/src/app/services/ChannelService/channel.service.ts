import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticationService } from '../AuthService/auth.service';
import { Observable } from 'rxjs';
import { Guild } from 'src/app/models/guild';
import { environment } from 'src/environments/environment';
import { Channel } from 'src/app/models/channel';

@Injectable({
  providedIn: 'root'
})
export class ChannelService {
  constructor(private http: HttpClient,
    private authService: AuthenticationService) { }

  getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': "bearer " + this.authService.getToken()
    })
  }

  public getAllChannelsOnGuild(id: string): Observable<Channel[]> {
    return this.http.get<Channel[]>(environment.api_base + `/channel/guild/` + id, { headers: this.getHeaders() });
  }

  public getChannel(id: string): Observable<Channel> {
    return this.http.get<Channel>(environment.api_base + '/channel/' + id, { headers: this.getHeaders() });
  }

  public createChannel(data: Channel): Observable<Channel> {
    return this.http.post<Channel>(environment.api_base + `/channel/`, data, { headers: this.getHeaders() });
  }

  public getAllChannelTypes(): Observable<string[]> {
    return this.http.get<string[]>(environment.api_base + `/channel/types`, { headers: this.getHeaders() });
  }

  
}
