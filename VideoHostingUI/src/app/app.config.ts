import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { MaterialModule } from './material/material.module';
import { environment } from '../environments/environment';
import { HOSTING_API_URL } from './app-injection-tokens';
import { JWT_OPTIONS, JwtHelperService } from '@auth0/angular-jwt';
import { ACCESS_TOKEN_KEY } from './Services/auth.service';
import { BrowserStorageService } from './Services/browse-storage.service';

export function tokenGetter(){
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes), 
    provideClientHydration(), 
    provideAnimationsAsync(),
    provideHttpClient(withFetch()),
    {
      provide: HOSTING_API_URL,
      useValue: environment.hostingApi
    },
    { provide: JWT_OPTIONS, useValue: {
      config:{
        tokenGetter,
        allowedDomains : environment.tokenWhiteListedDomains
      }
    }},
    JwtHelperService,
    BrowserStorageService
  ]
};
