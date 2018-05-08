import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { IUser, User } from './user';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  public user: IUser;
  public submitted = false;
  errorMessage: string;

  constructor(private router: Router,
    private _route: ActivatedRoute,
  private userService: UserService) {
  }

  ngOnInit() {
    const userid = this._route.snapshot.paramMap.get('id');
    if (userid) {
      this.user = new User();
      this.userService.getUser(userid)
      .subscribe(u => this.user = u, error => this.errorMessage = <any>error);
    } else {
      this.user = new User();
    }
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
