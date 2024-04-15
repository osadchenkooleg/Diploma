import { Routes } from '@angular/router';
import { LoginComponent } from './Components/auth/login/login.component';
import { RegistrationComponent } from './Components/auth/registration/registration.component';
import { MyProfileComponent } from './Components/user/my-profile/my-profile.component';
import { AuthGuard } from './guards/auth.guard';
import { SubscriptionsComponent } from './Components/video/subscriptions/subscriptions.component';
import { VideosPageComponent } from './Components/video/videos-page/videos-page.component';

export const routes: Routes = [
    {path: "login", component: LoginComponent },
    {path: "register", component: RegistrationComponent },
    {path: "videos", component: VideosPageComponent },
    {path: "subscriptions", component: SubscriptionsComponent },
    {path: "myprofile", component: MyProfileComponent, canActivate: [AuthGuard] },
    {path: "**", redirectTo: "myprofile" }
];
