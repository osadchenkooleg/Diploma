import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { User } from '../../../Models/UserModels/User';
import { Video } from '../../../Models/VideoModels/Video';
import { Observable, Subject, tap } from 'rxjs';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { UserService } from '../../../Services/user.service';
import { VideoService } from '../../../Services/video.service';
import { AuthService } from '../../../Services/auth.service';
import { HOSTING_API_URL } from '../../../app-injection-tokens';
import { IMAGES_ROUTE } from '../../../constants/wwwroot-constants';
import { MaterialModule } from '../../../material/material.module';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule
  ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UserProfileComponent implements OnInit {
  profilePhoto:string|null = null;
  isAdmin:boolean = false;
  isMyProfile:boolean = false;

  isEditing:boolean = false;

  user$: Observable<User> | undefined;
  userVideos: Video[] = []; 
  userLikedVideos: Video[] = []; 
  userDislikedVideos: Video[] = []; 

  onItemUpdated = new Subject<any>();
  
  constructor(
    private route: ActivatedRoute,
    @Inject(HOSTING_API_URL) private apiUrl:string,
    private up: UserService,
    private vs: VideoService,
    private auths:AuthService
  ) { }

  ngOnInit() {
    this.setUser();
    
    this.isAdmin = this.auths.isAdmin()
  }
  setUser(){
    let userId = "";
    this.route.paramMap.subscribe(params => {
      userId = params.get('userId')!;
      this.user$ = this.up.getUser(userId)
      .pipe( 
        tap(u => {
          console.log(u);
          if(u.id === this.auths.getUserId()){
            this.isMyProfile = true;
          }
          if(u.photoPath !== null && u.photoPath !== ""){
            this.profilePhoto = this.apiUrl + `${IMAGES_ROUTE}/` + u.photoPath;
          }          
        })
      );
      this.vs.GetVideosOfUser(userId).subscribe( result =>{
        this.userVideos = result;
      });
      this.vs.GetLikedVideos(userId).subscribe( result =>{
        this.userLikedVideos = result;
      });
      this.vs.GetDislikedVideos(userId).subscribe( result =>{
        this.userDislikedVideos = result;
      });
    })
  }
  
}
