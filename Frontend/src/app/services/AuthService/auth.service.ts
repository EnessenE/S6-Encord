import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Account } from '../../models/account';
import { jwt_decode } from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  public login(account): Observable<Account> {
    return this.http.post<Account>(environment.api_base + `/account/login`, account);
  }

  getTokenExpirationDate(token: string): Date {
    const decoded = jwt_decode(token);

    if (decoded.exp === undefined) return null;

    const date = new Date(0); 
    date.setUTCSeconds(decoded.exp);
    return date;
  }

  isTokenExpired(token?: string): boolean {
    return false;
    if(!token) token = this.getToken();
    if(!token) return true;

    const date = this.getTokenExpirationDate(token);
    if(date === undefined) return false;
    return !(date.valueOf() > new Date().valueOf());
  }

  public setToken(token: string){
    localStorage.setItem("Token", token);
  }

  public clearToken(){
    localStorage.removeItem("Token");
  }

  public getToken(){
    return localStorage.getItem("Token");
  }

  public tokenExistsAndValid(): boolean{
    var token = this.getToken();
    if (token && !this.isTokenExpired(token)){
      return true;
    }
    return false;
  }
}
