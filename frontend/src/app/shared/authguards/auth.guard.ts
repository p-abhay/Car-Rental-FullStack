import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UserAuthService } from '../services/user-auth.service/user-auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private userauthService: UserAuthService,
    private router: Router
  ) {}

  canActivate(): boolean {
    if (this.userauthService.isAuthenticated()) {
      return true;
    } else {
      alert('You are not logged in');
      this.router.navigate(['login']);
      return false;
    }
  }
}
