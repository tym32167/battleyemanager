import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { User } from '../user/user';

@Injectable()
export class UserService {

  private userServiceUrl = '/api/user';

  constructor(private _http: HttpClient) { }

  getAll(): Observable<User[]> {
    return this._http.get<User[]>(this.userServiceUrl)
      // .do(data => console.log('getAll: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  get(userid): Observable<User> {
    return this._http.get<User>(this.userServiceUrl + '/' + userid)
      // .do(data => console.log('get: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  update(user: User): Observable<User> {
    const url = this.userServiceUrl + '/' + user.id;
    return this._http.post<User>(url, user)
      // .do(data => console.log('update: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  add(user: User): Observable<User> {
    return this._http.put<User>(this.userServiceUrl, user)
      // .do(data => console.log('update: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  delete(userid: string): Observable<User> {
    const url = this.userServiceUrl + '/' + userid;
    return this._http.delete(url)
      // .do(data => console.log('update: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }


  private handleError(err: HttpErrorResponse) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage = '';
    if (err.error instanceof Error) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return Observable.throw(errorMessage);
  }
}
