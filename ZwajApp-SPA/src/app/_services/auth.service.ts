import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from "@auth0/angular-jwt";


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  jwtHelper = new JwtHelperService();

  baseUrl = 'http://localhost:5000/Auth/';

constructor(private http: HttpClient) { }

login(model: any) {
  return this.http.post(this.baseUrl + 'login', model).pipe(
    map((response: any) => {
      const user = response;
      if (user) { 
        localStorage.setItem('token', user.token);
    }
    }))
}

Register(model: any)
{
  return this.http.post(this.baseUrl + 'Register', model);
}

loggedIn() {
    
  try{const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);}
  catch{
    return false
  }
   }


}
