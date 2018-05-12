import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavigationComponent } from './navigation/navigation.component';
import { HomeComponent } from './home/home.component';
import { ErrorComponent } from './error/error.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AuthService } from './services/auth.service';
import { UserService } from './services/user.service';

import { AuthGuard } from './services/auth.guard';
import { HttpModule } from '@angular/http';

import { AuthInterceptor } from './services/auth-interceptor.service';
import { TestComponent } from './test/test.component';
import { AuthErrorHandler } from './services/auth-error-handler.service';
import { UserListComponent } from './user/user-list.component';
import { UserEditComponent } from './user/user-edit.component';
import { CONFIG } from './CONFIG';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    HomeComponent,
    ErrorComponent,
    LoginComponent,
    TestComponent,
    UserListComponent,
    UserEditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpModule
  ],
  providers: [
    AuthService,
    AuthGuard,
    UserService,
    CONFIG,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: ErrorHandler, useClass: AuthErrorHandler }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
