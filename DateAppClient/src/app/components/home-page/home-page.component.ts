import { Component, OnInit } from '@angular/core';
import { RegisterFormComponent } from "../register-form/register-form.component";

@Component({
  selector: 'app-home-page',
  imports: [RegisterFormComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit{
  isRegister = false;

  ngOnInit(): void {
  }

  onRegister(){
    this.isRegister = !this.isRegister;
  }
  cancelRegisMode(pram: boolean){
    this.isRegister = pram;
  }
}
