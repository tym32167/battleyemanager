import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from '../app/home/home.component';
import { ErrorComponent } from '../app/error/error.component';
import { LoginComponent } from '../app/login/login.component';

import { TestComponent } from '../app/test/test.component';
import { AuthGuard } from './auth.guard';

import { UserListComponent } from '../app/user/user-list.component';
import { UserEditComponent } from './user/user-edit.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'test', component: TestComponent, canActivate: [AuthGuard]},

  { path: 'users', component: UserListComponent, canActivate: [AuthGuard] },
  { path: 'users/edit', component: UserEditComponent, canActivate: [AuthGuard] },


  { path: '', component: HomeComponent, canActivate: [AuthGuard]},
  { path: '**', component: ErrorComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
