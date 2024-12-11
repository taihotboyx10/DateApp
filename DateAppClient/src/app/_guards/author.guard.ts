import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../../../services/Account.service';

export const authorGuard: CanActivateFn = (route, state) => {
  const accService = inject(AccountService);

  if(accService.currentUser()){
    return true;
  }else{
    alert('You shall not pass!');
    return false;
  }
};
