import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { map } from 'rxjs';
import { API_URLS } from '../src/Constant/Constant';
import { User } from '../src/app/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  
  private readonly _http: HttpClient;
  constructor(http : HttpClient) 
  {
    this._http = http;
  }

  currentUser = signal<User | null>(null);

  Login(user: any){
    return this._http.post<User>(API_URLS.BASE_URL + API_URLS.USER.LOGIN, user).pipe(
      map(user => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    )
  }

  Register(model: any){
    return this._http.post<User>(API_URLS.BASE_URL + API_URLS.USER.REGISTER, model).pipe(
      map(user => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;
      })
    )
  }

  Logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
