import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {
  }

  intercept(req: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {
    const idToken = this.authService.getToken();

    req.headers.set('X-Requested-With', 'XMLHttpRequest');

    if (idToken) {
      const cloned = req.clone({
        headers: req.headers.set('Authorization',
          'Bearer ' + idToken).set('X-Requested-With', 'XMLHttpRequest')
      });

      return next.handle(cloned);
    } else {
      return next.handle(req);
    }
  }
}
