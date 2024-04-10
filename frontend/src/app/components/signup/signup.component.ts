import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/models/user.model';
import { UserAuthService } from 'src/app/shared/services/user-auth.service/user-auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent implements OnInit {
  user: User = {
    id: '00000000-0000-0000-0000-000000000000',
    fullName: '',
    email: '',
    phoneNumber: '',
    password: '',
    isAdmin: false,
  };
  confirmPassword: string = '';
  constructor(
    private userauthService: UserAuthService,
    private router: Router
  ) {}
  ngOnInit(): void {}
  signup() {
    if (!/^[a-zA-Z ]+$/.test(this.user.fullName)) {
      alert('Full Name should contain characters only');
      return;
    }

    if (
      !/^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(
        this.user.email
      )
    ) {
      alert('Email must be a valid email address.');
      return;
    }

    if (!/^\d+$/.test(this.user.phoneNumber)) {
      alert('Phone number should contain digits only');
      return;
    }

    if (
      this.user.phoneNumber.length < 10 ||
      this.user.phoneNumber.length > 10
    ) {
      alert('Phone number should be of 10 digits');
      return;
    }

    if (this.user.password.length < 8) {
      alert('Password must be at least 8 characters long.');
      return;
    }
    const hasUpperCase = /[A-Z]/.test(this.user.password);
    const hasLowerCase = /[a-z]/.test(this.user.password);
    const hasDigit = /\d/.test(this.user.password);
    const hasSpecialChar = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(
      this.user.password
    );
    if (!hasUpperCase || !hasLowerCase || !hasDigit || !hasSpecialChar) {
      alert(
        'Password should contain atleast 1 uppercase character ,1 smaller case character,1 digit and 1 special symbol'
      );
      return;
    }
    if (this.user.password === this.confirmPassword) {
    } else {
      console.log(this.user.password);
      console.log(this.confirmPassword);
      alert('Password should be same.');
      return;
    }

    if (
      !this.user.email ||
      !this.user.password ||
      !this.user.fullName ||
      !this.user.phoneNumber ||
      !this.confirmPassword
    ) {
      alert('All fields are required.');
      return;
    }

    this.userauthService.signup(this.user).subscribe({
      next: (res) => {
        this.userauthService.saveToken(res.jwt);
        console.log(this.userauthService.getToken());
        this.userauthService.decodeJWT();
        console.log('token decoded');
      },
      error: (e) => {
        console.error('Login Error:', e);
      },
    });
    this.router.navigate(['dashboard']);
  }
}
