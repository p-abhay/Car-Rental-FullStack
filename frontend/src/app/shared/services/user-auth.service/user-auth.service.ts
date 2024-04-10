import { Injectable } from '@angular/core';
import { User } from 'src/app/shared/models/user.model';
import { environment } from 'src/environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Credentials } from '../../models/credentials';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class UserAuthService {
  private baseApiUrl: string = environment.baseApiURL;
  private tokenKey: string = 'jwt-token';
  private payLoad: any;

  constructor(private http: HttpClient) {}

  //signup and receive JWT token
  signup(user: User): Observable<{ jwt: string }> {
    return this.http.post<{ jwt: string }>(
      this.baseApiUrl + '/api/users/signup',
      user
    );
  }

  //login and receive JWT token
  login(credentials: Credentials): Observable<{ jwt: string }> {
    return this.http.post<{ jwt: string }>(
      this.baseApiUrl + '/api/users/login',
      credentials
    );
  }

  //Get user info by userID
  getUserById(userId: string): Observable<User> {
    return this.http.get<User>(`${this.baseApiUrl}/api/users/${userId}`);
  }

  //Save JWT token to LocalStorage
  saveToken(jwt: string) {
    localStorage.setItem(this.tokenKey, jwt);
  }

  //Get JWT token from LocalStorage
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  //Check for user authentication based on JWT token
  isAuthenticated(): boolean {
    const token = this.getToken();
    //console.log('inside auth service' + token);
    if (token == undefined) return false;
    return !!token;
  }

  //logout and remove token
  // logout() {
  //   localStorage.removeItem(this.tokenKey);
  // }
  logout() {
    return new Observable((observer) => {
      try {
        localStorage.removeItem(this.tokenKey);
        this.payLoad = null;
        observer.next(); // Emit a value to indicate the operation is complete
        observer.complete(); // Complete the Observable
      } catch (error) {
        observer.error(error); // Emit an error if something goes wrong
      }
    });
  }

  //decode JWT token and get the payload
  decodeJWT() {
    //const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    //console.log('toke in decodeJWT(): ' + token);
    //this.payLoad = JSON.stringify(jwtHelper.decodeToken(token));
    this.payLoad = jwt_decode(token);
    //console.log('paylod : ' + this.payLoad);
  }
  //get FullName from token
  getFullName(): string {
    //console.log('inside fullname : ' + this.payLoad + ' ' + this.payLoad.Name);
    return this.payLoad.Name;
  }

  //check admin role
  isAdmin(): boolean {
    if (this.payLoad) {
      return JSON.parse(this.payLoad.IsAdmin)!!;
    }
    return false;
  }

  //get user id
  getUserId(): string {
    return this.payLoad.Id;
  }
}
