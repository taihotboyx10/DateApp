import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../../../services/Account.service';
import { AllUser } from '../../models/user';
import { CommonModule, DatePipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-lists',
  imports: [CommonModule, DatePipe],
  templateUrl: './lists.component.html',
  styleUrl: './lists.component.css'
})
export class ListsComponent implements OnInit {
  accService = inject(AccountService);
  users: AllUser[] = [];
  route = inject(Router);

  ngOnInit(): void {
    this.onLoadAllUser();
  }

  onLoadAllUser(){
    this.accService.GetAllUser().subscribe({
      next: (users) => {
        this.users = users;
        console.log(users);
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  onDeleteUser(int: number){
    this.accService.DeleteUser(int).subscribe({
      next: () => {
        alert("Delete successful");
        this.onLoadAllUser();
      },
      error: (error) => {console.log(error)},
    })
  }

  onUpdateMove(userId: number){
    this.route.navigate(['member/update'], { queryParams: { id: userId } });
  }
}
