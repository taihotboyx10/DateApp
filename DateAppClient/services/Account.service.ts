import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { map, Observable } from 'rxjs';
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

  // Register(model: any){
  //   return this._http.post(API_URLS.BASE_URL + API_URLS.USER.REGISTER, model).pipe(
  //     map(user => {
  //       if(user){
  //         // localStorage.setItem('user', JSON.stringify(user));
  //         // this.currentUser.set(user);
  //       }
  //       return user;
  //     })
  //   )
  // }

  Register(regisObj: any): Observable<any>{
    return this._http.post<any>(API_URLS.BASE_URL + API_URLS.USER.REGISTER, regisObj);
  }

  Logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }

  GetAllUser(): Observable<any>{
    return this._http.get<any>(API_URLS.BASE_URL + API_URLS.USER.GET_ALL_USER);
  }

  DeleteUser(id: number){
    return this._http.delete(API_URLS.BASE_URL + API_URLS.USER.DELETE_USER(id));
  }

  UpdateUser(id: number, updateUser: any): Observable<any>{
    return this._http.put<any>(API_URLS.BASE_URL + API_URLS.USER.UPDATE_USER(id), updateUser);
  }

  GetUserById(id: number): Observable<any>{
    return this._http.get<any>(API_URLS.BASE_URL + API_URLS.USER.GET_USER_BY_ID(id));
  }
  
}
