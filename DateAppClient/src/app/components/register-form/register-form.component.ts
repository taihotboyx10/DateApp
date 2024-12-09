import { CommonModule } from '@angular/common';
import { Component, inject, output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../../../../services/Account.service';
import { error } from 'console';

@Component({
  selector: 'app-register-form',
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})
export class RegisterFormComponent {
  registerForm = new FormGroup({
    UserName: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(12)]),
    Pwd: new FormControl('', [Validators.required]),
  });
  accountService = inject(AccountService);

  cancelRegister = output<boolean>(); 

  onSubmit(){
    this.accountService.Register(this.registerForm.value).subscribe({
      next: (user) => {
        console.log(user);
      },
      error: (error) => {
        console.log(error);
      },
      // complete: () => console.log("complete!")
    })
  }

  onCancel(){
    // this.registerForm.reset();
    this.cancelRegister.emit(false);
  }
}
