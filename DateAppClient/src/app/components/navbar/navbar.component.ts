import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms'
import { AccountService } from '../../../../services/Account.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-navbar',
  imports: [FormsModule, ReactiveFormsModule, BsDropdownModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  accountService = inject(AccountService)
  // Khởi tạo FormGroup
  loginForm = new FormGroup({
    UserName: new FormControl(),
    Pwd: new FormControl(),
  });

  onLogin(){
    this.accountService.Login(this.loginForm.value).subscribe({
      next: response => {
        console.log(response);
      } ,
      error: error => {
        console.log(error);
      }
    })
  }

  onLogout(){
    this.accountService.Logout();
  }

}
