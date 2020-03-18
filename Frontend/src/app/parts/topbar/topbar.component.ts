import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/AccountService/account.service';
import { AuthenticationService } from 'src/app/services/AuthService/auth.service';
import { Router } from '@angular/router';
import { Account } from 'src/app/models/account';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.css']
})
export class TopbarComponent implements OnInit {
  account: Account;

  constructor(private accountService: AccountService,
    private authService: AuthenticationService,
    private router: Router) { }

  ngOnInit(): void {
    this.accountService.getUser().subscribe(
      data => {
        console.log(data);
        this.account = data;
      },
      error => {
        if (error.status == 401) {
          this.routeToLogin();
        }
        else {
          console.error(error);
        }
      });
  }

  private routeToLogin() {
    this.router.navigate(['./login']);
  }

  Logout() {
    this.authService.clearToken();
    this.router.navigate(['./login']);
  }
}
