import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/services/AuthService/auth.service';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errorText: string;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
    private accountService: AuthenticationService,
    private router: Router) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(environment.settings.minimalpasswordlength)]],
      email: ['', Validators.required]
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
  get f() { return this.registerForm.controls; }

  RouteToDash() {
    this.router.navigate(['./dashboard']);
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      this.submitted = false;
      this.errorText = "Please fill all fields"
      return;
    }

    this.accountService.register(this.registerForm.value).subscribe(
      data => {
        this.submitted = false;
        this.accountService.setToken(data.token);
        this.RouteToDash();
      },
      error => {
        if (error.status == 401) {
          this.errorText = "Registration failed. Wrong credentials."
        }
        else {
          console.error(error);
          this.errorText = "Registration currently isn't available."
        }
        this.submitted = false;
      });
  }
}
