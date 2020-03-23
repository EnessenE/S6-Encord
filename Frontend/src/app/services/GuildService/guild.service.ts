import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticationService } from '../AuthService/auth.service';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Guild } from 'src/app/models/guild';

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
    return this.http.get<Guild[]>(environment.api_base + `/guild/`, { headers: this.getHeaders()});
  }
}