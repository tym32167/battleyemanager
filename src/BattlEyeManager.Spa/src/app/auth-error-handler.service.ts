import { ErrorHandler, Injectable, Injector, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable()
export class AuthErrorHandler implements ErrorHandler {

  constructor(private injector: Injector, private authService: AuthService, private ngZone: NgZone) { }

  handleError(error) {
    const router = this.injector.get(Router);
    if (error.status === 401) {
      this.authService.logout();

      this.ngZone.run(() => {
        router.navigate(['/login']);
      });

      return;
    }

    throw error;
  }
}
