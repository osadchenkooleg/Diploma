import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { MaterialModule } from '../../../material/material.module';
import { User } from '../../../Models/UserModels/User';
import { Observable, tap } from 'rxjs';
import { AuthService } from '../../../Services/auth.service';
import { HOSTING_API_URL } from '../../../app-injection-tokens';

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
    // private up: UserService
  ) { }
  
  public get isLoggedIn() : boolean{
    return this.as.isAuthenticated();
  }

  ngOnInit() {
    // this.setUser();
  }
  
  // setUser(){
  //   this.route.paramMap.subscribe(params => {
  //     this.user$ = this.up.getMyUser()
  //     .pipe(
  //       tap(u =>{
  //         if(u.photos.length > 0){
  //           this.profilePhoto = this.apiUrl + u.photopath;
  //         }
  //       })
  //     );
  //   })
  // }
  logout(){
    this.as.logout();
  }
}
