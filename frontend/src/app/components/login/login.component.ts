import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Credentials } from 'src/app/shared/models/credentials';
import { UserAuthService } from 'src/app/shared/services/user-auth.service/user-auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  user: Credentials = {
    email: '',
    password: '',
  };
  passwordValue = {
    dirty: false,
    errors: {
      minlength: false,
    },
  };
  emailValue = {
    dirty: false,
    errors: {
      valid: false,
    },
  };

  constructor(
    private router: Router,
    private userauthService: UserAuthService
  ) {}

  onPasswordChange() {
    this.passwordValue.dirty = true;
    this.passwordValue.errors.minlength = this.user.password.length < 8;
  }
  login() {
    this.userauthService.login(this.user).subscribe({
      next: (res) => {
        this.userauthService.saveToken(res.jwt);
        console.log(this.userauthService.getToken());
        this.userauthService.decodeJWT();
        console.log('token decoded');
        this.router.navigate(['dashboard']);
      },
      error: (e) => {
        console.error('Login Error:', e);
        alert('Wrong login info!');
      },
    });
  }
}
