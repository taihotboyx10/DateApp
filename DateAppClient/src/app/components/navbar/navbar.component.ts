import { Component, inject, Pipe } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms'
import { AccountService } from '../../../../services/Account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [FormsModule, ReactiveFormsModule, BsDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe],
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

  route = inject(Router);

  onLogin(){
    this.accountService.Login(this.loginForm.value).subscribe({
      next: response => {
        // console.log(response);
        this.route.navigateByUrl('member');
        console.log(this.accountService.currentUser());
      } ,
      error: error => {
        console.log(error);
      }
    })
  }

  onLogout(){
    this.accountService.Logout();
    this.route.navigateByUrl('/');
  }

}
