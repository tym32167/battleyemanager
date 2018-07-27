import { Component, OnInit } from '@angular/core';
import { User } from './user';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  constructor(private userService: UserService,
          private router: Router ) {  }

  errorMessage: string;
  users: User[] = [];

  ngOnInit() {
    this.refreshUsers();
  }

  private refreshUsers() {
    this.userService.getAll()
      .subscribe(users => {
        this.users = users;
      },
        error => this.errorMessage = <any>error);
  }

  deleteUser(user: User) {
    if (confirm('Do you want to delele user ' + user.userName + '?')) {
      this.userService.delete(user.id).subscribe(result => this.refreshUsers());
    }
  }
}
