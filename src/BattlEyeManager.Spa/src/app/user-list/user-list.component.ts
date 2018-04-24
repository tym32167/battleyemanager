import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { IUser } from './user';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  constructor(private userService: UserService) {  }

  errorMessage: string;
  users: IUser[] = [];

  ngOnInit() {
    this.userService.getUsers()
      .subscribe(users => {
        this.users = users;
      },
      error => this.errorMessage = <any>error);
  }
}
