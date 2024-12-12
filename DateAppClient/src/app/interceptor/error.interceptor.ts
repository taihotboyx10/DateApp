import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const route = inject(Router);


  return next(req).pipe(
    catchError(error => {
      if(error){
        switch (error.status) {
          case 400:
            if(error.error.errors){
              const modalStateErr = [];
              for(const key in error.error.errors){
                if(error.error.errors[key]){
                  modalStateErr.push(error.error.errors[key]);
                }
              }
              throw modalStateErr.flat();
            }else{
              alert(error.status + ': ' +error.error);
            }
            break;
          case 401:
            alert(error.status + ": Unauthorize");
            break;
          case 404:
            route.navigateByUrl("/not-found");
            break;
          case 500:
            const navigateEXtras: NavigationExtras = {state: {error: error.error}};
            route.navigateByUrl("/server-error", navigateEXtras);
            break;
          // Successful
          case 200:
            break;
          // Conflict
          case 409:
            break;
          // No content
          case 204:
            break;
          default:
            alert("Something went wrong");
            break;
        }
      }
      throw error;
    })
  );
};
