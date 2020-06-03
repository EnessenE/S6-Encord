import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Account } from 'src/app/models/account';
import { AuthenticationService } from '../AuthService/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient,
    private authService: AuthenticationService) { }

  public getUser(): Observable<Account> {
    return this.http.get<Account>(environment.api_base + `/account/`, { headers: this.authService.getHeaders() });
  }
}
