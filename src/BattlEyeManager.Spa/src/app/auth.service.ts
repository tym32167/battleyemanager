
import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export const TOKEN_NAME = 'jwt_token';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable()
export class AuthService {

  private url = 'api/account';
  private headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  logout() {
    localStorage.removeItem(TOKEN_NAME);
  }

  getToken(): string {
    return localStorage.getItem(TOKEN_NAME);
  }

  setToken(token: string): void {
    localStorage.setItem(TOKEN_NAME, token);
  }

  isTokenExpired(token?: string): boolean {
    if (!token) { token = this.getToken(); }
    if (!token) { return true; }
    return false;
  }

  login(username: string, password: string) {
    return this.http
      .post<LoginResponse>(`${this.url}/auth`, JSON.stringify({ username, password }), httpOptions)
      .toPromise()
      .then(res => this.setSession(res));
  }

  private setSession(authResult: LoginResponse) {
    this.setToken(authResult.access_token);
  }
}

interface LoginResponse {
  access_token: string;
  username: string;
}
