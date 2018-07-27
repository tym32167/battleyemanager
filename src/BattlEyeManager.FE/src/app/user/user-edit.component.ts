import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from './user';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  public user: User = new User();
  public submitted = false;
  errorMessage: string;
  public editMode = false;

  constructor(private router: Router,
    private _route: ActivatedRoute,
  private userService: UserService) {
  }

  ngOnInit() {
    const userid = this._route.snapshot.paramMap.get('id');
    if (userid) {
      this.editMode = true;
      console.log(userid);
      this.userService.get(userid)
      .subscribe(u => this.user = u, error => this.errorMessage = <any>error);
    }
  }

  submit(form: NgForm) {
    this.submitted = true;
    if (form.valid) {
      if (this.editMode) {
        this.userService.update(this.user).subscribe(u => this.router.navigateByUrl('/users'), error => this.errorMessage = <any>error);
      } else {
        this.userService.add(this.user).subscribe(u => this.router.navigateByUrl('/users'), error => this.errorMessage = <any>error);
      }
    } else {

     }
  }
}
