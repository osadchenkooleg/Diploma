import { Routes } from '@angular/router';
import { LoginComponent } from './Components/auth/login/login.component';
import { RegistrationComponent } from './Components/auth/registration/registration.component';
import { MyProfileComponent } from './Components/user/my-profile/my-profile.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
    {path: "login", component: LoginComponent },
    {path: "register", component: RegistrationComponent },
    {path: "myprofile", component: MyProfileComponent, canActivate: [AuthGuard] },
    {path: "**", redirectTo: "myprofile" }
];
