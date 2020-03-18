import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errorText: string;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(environment.settings.minimalpasswordlength)]]
    });

    if (this.accountService.tokenExistsAndValid()) {
      console.log("Found a token");
      this.RouteToDash();
    }
    else {
      console.log("No token found");
    }
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  RouteToDash() {
    this.router.navigate(['./dashboard']);
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      this.submitted = false;
      this.errorText = "Please fill all fields"
      return;
    }

    this.accountService.login(this.loginForm.value).subscribe(
      data => {
        console.log(data);
        this.submitted = false;
        this.accountService.setToken(data.token);
        this.RouteToDash();
      },
      error => {
        if (error.status == 401) {
          this.errorText = "Login failed. Wrong credentials."
        }
        else {
          console.error(error);
          this.errorText = "Login currently isn't available."
        }
        this.submitted = false;
      });
  }

}
