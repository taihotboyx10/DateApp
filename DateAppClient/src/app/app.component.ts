import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./components/navbar/navbar.component";
import { AccountService } from '../../services/Account.service';
import { HomePageComponent } from "./components/home-page/home-page.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http = inject(HttpClient)
  accountService = inject(AccountService)
  user: any;

  ngOnInit(): void {
    // this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser(){
    if (typeof window === 'undefined') return;
    const loginUser = localStorage.getItem('user');
    if(!loginUser) return;
    if(loginUser){
      this.accountService.currentUser.set(JSON.parse(loginUser));
    }
  }

  // getUsers(){
  //   this.http.get(API_URLS.BASE_URL + API_URLS.USER.GET_ALL_USER).subscribe({
  //     next: response => { console.log(response) },
  //     error: error => { console.log(error) },
  //     complete: () => console.log("complete!"),
  //   }
  //   )
  // }
}
