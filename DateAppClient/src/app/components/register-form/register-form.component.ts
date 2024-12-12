import { CommonModule } from '@angular/common';
import { Component, inject, output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, FormsModule, MaxLengthValidator, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../../../../services/Account.service';
import { RegisterUserDTO } from '../../models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-form',
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})
export class RegisterFormComponent {
  registerForm = new FormGroup({
    UserName: new FormControl('', [Validators.required, Validators.maxLength(20)]),
    Pwd: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]),
    ConfirmPwd: new FormControl('', [Validators.required]),
    DoB: new FormControl('', [Validators.required]),
    Gender: new FormControl('male'),
    Introductions: new FormControl('', [Validators.maxLength(15)]),
    Interest: new FormControl('', [Validators.maxLength(50)]),
    LookingFor: new FormControl('', [Validators.maxLength(50)]),
    City: new FormControl('', [Validators.maxLength(15)]),
    Country: new FormControl('', [Validators.maxLength(15)]),
  },
  {validators: CustomValidators.mustMatch('Pwd', 'ConfirmPwd')}
  );
  
  userNameConflictMsg: string = '';

  route = inject(Router);
  accountService = inject(AccountService);
  
  cancelRegister = output<boolean>(); 
  
  onSubmit(){
    // const regisObj: RegisterUserDTO = this.registerForm.value;
    this.accountService.Register(this.registerForm.value).subscribe({
      next: (user) => {
        console.log(user);
        alert("Register successfully");
        this.route.navigateByUrl("/lists")
      },
      error: (error) => {
        if(error.status === 409){
          this.userNameConflictMsg = 'User with the same username already exists.';
        }
        console.log(error);
      },
    })
  }

  onCancel(){
    this.registerForm.reset();
    // this.cancelRegister.emit(false);
  }
}

export class CustomValidators {
  static mustMatch(controlName: string, matchingControlName: string): ValidatorFn {
    return (formGroup: AbstractControl): ValidationErrors | null => {
      const control = formGroup.get(controlName);
      const matchingControl = formGroup.get(matchingControlName);

      if (!control || !matchingControl) {
        return null; // Bỏ qua nếu các trường chưa được khởi tạo
      }

      // Nếu `matchingControl` đã có lỗi khác không phải từ `mustMatch`, bỏ qua
      if (matchingControl.errors && !matchingControl.errors['mustMatch']) {
        return null;
      }

      // Đặt lỗi nếu giá trị không khớp
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null); // Xóa lỗi nếu giá trị khớp
      }

      return null;
    };
  }
}
