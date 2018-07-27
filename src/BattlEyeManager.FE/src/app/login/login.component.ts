import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../services/auth.service';
import { User } from '../user/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  public submitted = false;
  public user: User;

  constructor(
    private authService: AuthService,
    private router: Router) {
      this.user = new User();
    }

  submit(form: NgForm) {
    this.submitted = true;
    const self = this;
    if (form.valid) {
      this.authService.login(this.user.email, this.user.password)
        .then(() => self.router.navigateByUrl('/'));
    }
  }
}
