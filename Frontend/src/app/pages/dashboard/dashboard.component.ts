import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/AuthService/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(
    private authService: AuthenticationService,
    private router: Router) { }

  ngOnInit(): void {
    if (this.authService.tokenExistsAndValid()){
      console.log("Found a token");
    }
    else{
      console.log("No token found");
      this.routeToLogin();
    }
  }

  private routeToLogin(){
    this.router.navigate(['./login']);
  }
}
