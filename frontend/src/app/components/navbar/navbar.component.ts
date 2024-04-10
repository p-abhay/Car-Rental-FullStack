import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserAuthService } from 'src/app/shared/services/user-auth.service/user-auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  name: string = '';
  constructor(
    public userAuthService: UserAuthService,
    private router: Router
  ) {}

  ngOnInit() {
    if (this.userAuthService.isAuthenticated())
      this.userAuthService.decodeJWT();
    //this.name = this.userAuthService.getFullName();
    //console.log(this.userAuthService.getToken());
  }

  logout() {
    this.userAuthService.logout().subscribe({
      next: () => {
        console.log('Logout Successfull');
        this.router.navigate(['']);
      },
    });
    //this.router.navigate(['']);
  }
}
