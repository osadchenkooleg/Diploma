import type { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { AuthService } from '../Services/auth.service';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const as = inject(AuthService);
  const router = inject(Router);
  
  var token = as.getToken();
  
  if (token) {
    const cloned = req.clone({
      setHeaders: {
        authorization: `bearer ${token}`,
      },
    });
    return next(cloned);
  } else {
    return next(req);
  }
};
