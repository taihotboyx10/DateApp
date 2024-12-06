import { JsonPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Console, error } from 'console';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, JsonPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http = inject(HttpClient)
  user: any;

  ngOnInit(): void {
    this.http.get("https://localhost:5097/api/user/2").subscribe({
      next: response => this.user = response,
      // error: response => console.log(response.message),
      error: response => console.log(response),
      complete: () => alert('Request has complete!'),
    })
  }
}
