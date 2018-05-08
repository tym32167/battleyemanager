import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { IUser, User } from './user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  public user: IUser;
  public submitted = false;

  constructor(private router: Router) {
    this.user = new User();
  }

  ngOnInit() {
  }

  submit(form: NgForm) {
    this.submitted = true;
    if (form.valid) {
      console.log(this.user);
      this.router.navigateByUrl('/users');
    } else {

     }
  }
}
