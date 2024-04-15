import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { MaterialModule } from '../../../material/material.module';
import { User } from '../../../Models/UserModels/User';
import { Observable, tap } from 'rxjs';
import { AuthService } from '../../../Services/auth.service';
import { HOSTING_API_URL } from '../../../app-injection-tokens';
import { UserService } from '../../../Services/user.service';
import { IMAGES_ROUTE } from '../../../constants/wwwroot-constants';

@Component({
  selector: 'app-user-navbar-view',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule
  ],
  templateUrl: './user-navbar-view.component.html',
  styleUrl: './user-navbar-view.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UserNavbarViewComponent implements OnInit {
  profilePhoto:string|null = null;
  isAuthor:boolean = false;
  user$: Observable<User> | undefined; 
  constructor(
    private route: ActivatedRoute,
    @Inject(HOSTING_API_URL) private apiUrl:string,
    private as: AuthService,
    private up: UserService
  ) { }
  
  public get isLoggedIn() : boolean{
    return this.as.isAuthenticated();
  }

  ngOnInit() {
    this.setUser();
    console.log(this.user$);
  }
  
  setUser(){
    this.route.paramMap.subscribe(params => {
      this.user$ = this.up.getMyProfile()
      .pipe(
        tap(u =>{
          console.log(u);
          if(u.photoPath !== null || u.photoPath !== undefined){
            this.profilePhoto = this.apiUrl + `${IMAGES_ROUTE}/` + u.photoPath;
            console.log(this.profilePhoto);
          }
        })
      );
    })
  }
  logout(){
    this.as.logout();
  }
}
