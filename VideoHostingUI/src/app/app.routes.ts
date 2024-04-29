import { Routes } from '@angular/router';
import { LoginComponent } from './Components/auth/login/login.component';
import { RegistrationComponent } from './Components/auth/registration/registration.component';
import { MyProfileComponent } from './Components/user/my-profile/my-profile.component';
import { AuthGuard } from './guards/auth.guard';
import { SubscriptionsComponent } from './Components/video/subscriptions/subscriptions.component';
import { VideosPageComponent } from './Components/video/videos-page/videos-page.component';
import { UserProfileComponent } from './Components/user/user-profile/user-profile.component';
import { UserChangeProfileComponent } from './Components/user/user-change-profile/user-change-profile.component';
import { VideoDetailsComponent } from './Components/video/video-details/video-details.component';

export const routes: Routes = [
    {path: "login", component: LoginComponent },
    {path: "register", component: RegistrationComponent },
    {path: "videos", component: VideosPageComponent, canActivate: [AuthGuard] },
    {path: "subscriptions", component: SubscriptionsComponent, canActivate: [AuthGuard] },
    {path: "myprofile", component: MyProfileComponent, canActivate: [AuthGuard] },
    {path: "user/:userId", component: UserProfileComponent, canActivate: [AuthGuard] },
    {path: "user/info/:userId", component: UserChangeProfileComponent, canActivate: [AuthGuard] },
    {path: "video/:videoId", component: VideoDetailsComponent, canActivate: [AuthGuard] },
    {path: "**", redirectTo: "myprofile" }
];
