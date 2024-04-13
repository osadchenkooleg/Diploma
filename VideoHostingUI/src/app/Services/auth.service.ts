import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { JwtHelperService, JWT_OPTIONS  } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { HOSTING_API_URL } from '../app-injection-tokens';
import { UserApplyModel } from '../Models/AuthModels/UserApplyModel';
import { Token } from '../Models/AuthModels/Token';
import { DOCUMENT } from '@angular/common';


export const ACCESS_TOKEN_KEY = 'hosting_api_token';
export const USERID = 'userid';
export const IS_ADMIN = 'is_admin';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private http: HttpClient,
    @Inject(HOSTING_API_URL) private apiUrl:string,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) {
   }
  register(user:UserApplyModel ) {
    return this.http.post<UserApplyModel>(`${this.apiUrl}api/account/register`,{
      ...user
    });
  }
  login(email:string, password:string): Observable<Token>{
    return this.http.post<Token>(`${this.apiUrl}api/account/login`,{
      email, password
    }).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.token);
        
        localStorage.setItem(USERID, this.decodeToken().nameid);

        var role = this.decodeToken().role;
        console.log(role);
        if(role !== null && role.includes("Admin"))
          localStorage.setItem(IS_ADMIN, "true");
        else
        localStorage.setItem(IS_ADMIN, "false");
      })
    )
  }

  isAuthenticated(): boolean{    
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);   
    if(token === null)
      token = ""; 
    return !this.jwtHelper.isTokenExpired(token);
  }
  decodeToken(){
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);   
    if(token === null)
      token = ""; 
    return this.jwtHelper.decodeToken(token);
  }
  logout() : void{
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    localStorage.removeItem(USERID);
    localStorage.removeItem(IS_ADMIN);
    this.router.navigate(['login']);
  }
  isAdmin():boolean{
    if(localStorage.getItem(IS_ADMIN) === "true")  {
      return true;
    }   
    return false;
  }
  getUserId():string|null{
    return localStorage.getItem(USERID);
  }
}
