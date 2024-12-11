import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { API_URLS } from '../../../../Constant/Constant';
import { error } from 'console';

@Component({
  selector: 'app-errtest',
  imports: [],
  templateUrl: './errtest.component.html',
  styleUrl: './errtest.component.css'
})
export class ErrtestComponent {
  http = inject(HttpClient);
  on400Err(){
    this.http.get(API_URLS.BASE_URL + '/buggy/bad-request').subscribe({
      next: response => console.log(response),
      error: error => console.log(error),
    });
  }
  on401Err(){
    this.http.get(API_URLS.BASE_URL + '/buggy/auth').subscribe({
      next: response => console.log(response),
      error: error => console.log(error),
    });
  }
  on404Err(){
    this.http.get(API_URLS.BASE_URL + '/buggy/not-found').subscribe({
      next: response => console.log(response),
      error: error => console.log(error),
    });
  }
  on500Err(){
    this.http.get(API_URLS.BASE_URL + '/buggy/server-error').subscribe({
      next: response => console.log(response),
      error: error => console.log(error),
    });
  }
  on500ErrValid(){
    this.http.post(API_URLS.BASE_URL + '/account/register', {}).subscribe({
      next: response => console.log(response),
      error: error => console.log(error),
    });
  }
}
