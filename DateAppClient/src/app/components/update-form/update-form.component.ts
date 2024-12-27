import { Component, inject, OnInit} from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../services/Account.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-form',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './update-form.component.html',
  styleUrl: './update-form.component.css'
})
export class UpdateFormComponent implements OnInit{
  updateUserForm = new FormGroup({
    UserName: new FormControl('', [Validators.required, Validators.maxLength(20)]),
    DoB: new FormControl('', [Validators.required]),
    Gender: new FormControl('male'),
    Introductions: new FormControl('', [Validators.maxLength(15)]),
    Interest: new FormControl('', [Validators.maxLength(50)]),
    LookingFor: new FormControl('', [Validators.maxLength(50)]),
    City: new FormControl('', [Validators.maxLength(15)]),
    Country: new FormControl('', [Validators.maxLength(15)]),
  });
  
  updateUserId: number|null = null;
  userNameConflictMsg: string = '';
  
  route = inject(Router);
  activatedRoute = inject(ActivatedRoute);
  
  accountService = inject(AccountService);
  
  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.updateUserId = Number(params['id']);
      console.log('User ID:', this.updateUserId);
    });

    this.onLoadCurrentUser();
  }

  onLoadCurrentUser(){
    this.accountService.GetUserById(Number(this.updateUserId)).subscribe({
      next: (user) => {
        console.log(this.updateUserId);
        console.log(user);
        this.updateUserForm.patchValue({
        UserName: user.userName,
        DoB: user.doB,
        Gender: user.gender,
        Introductions: user.introductions,
        Interest: user.interest,
        LookingFor: user.lookingFor,
        City: user.city,
        Country: user.country,
        })
      }
    })
  }

  onUpdate(){
    // const regisObj: RegisterUserDTO = this.registerForm.value;
    this.accountService.UpdateUser(Number(this.updateUserId), this.updateUserForm.value).subscribe({
      next: (user) => {
        console.log(user);
        alert("Update successfully");
        this.route.navigateByUrl("/lists")
      },
      error: (error) => {
        if(error.status === 409){
          this.userNameConflictMsg = error.error.errors;
        }
        console.log(error);
      },
    })
  }

  onCancel(){
    this.updateUserForm.reset();
    // this.cancelRegister.emit(false);
  }
}
