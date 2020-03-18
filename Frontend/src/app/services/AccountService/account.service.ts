import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticationService } from '../AuthService/auth.service';
import { Account } from 'src/app/models/account';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient,
    private authService: AuthenticationService) { }

  getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': "bearer " + this.authService.getToken()
    })
  }

  public getUser(): Observable<Account> {
    return this.http.get<Account>(environment.api_base + `/account/`, { headers: this.getHeaders()});
  }
}
