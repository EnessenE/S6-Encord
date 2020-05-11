import { Injectable } from '@angular/core';
import { AuthenticationService } from '../AuthService/auth.service';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Guild } from 'src/app/models/guild';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GuildService {

  constructor(private http: HttpClient,
    private authService: AuthenticationService) { }

  getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': "bearer " + this.authService.getToken()
    })
  }

  public getAllGuilds(): Observable<Guild[]> {
    return this.http.get<Guild[]>(environment.api_base + `/guild/`, { headers: this.getHeaders() });
  }

  public getGuild(id: string): Observable<Guild> {
    return this.http.get<Guild>(environment.api_base + '/guild/' + id, { headers: this.getHeaders() });
  }

  public createGuild(guild: Guild): Observable<Guild> {
    return this.http.post<Guild>(environment.api_base + `/guild/`, guild, { headers: this.getHeaders() });
  }

  public deleteGuild(guild: Guild): Observable<boolean> {
    return this.http.request<boolean>('delete', environment.api_base + `/guild/`,  { headers: this.getHeaders(), body: guild});
  }
}
