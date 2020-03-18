import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(
    private accountService: AccountService,
    private router: Router) { }

  ngOnInit(): void {
    if (this.accountService.tokenExistsAndValid()){
      console.log("Found a token");
    }
    else{
      console.log("No token found");
      this.router.navigate(['./login']);
    }
  }

  Logout(){
    this.accountService.clearToken();
    this.router.navigate(['./login']);
  }

}
