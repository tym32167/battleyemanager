import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { IUser, User } from './user';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  public user: IUser;
  public submitted = false;

  constructor() {
    this.user = new User();
  }

  ngOnInit() {
  }

  submit(form: NgForm) {
    this.submitted = true;
    if (form.valid) {
      console.log(this.user);
    } else {

     }
  }
}
