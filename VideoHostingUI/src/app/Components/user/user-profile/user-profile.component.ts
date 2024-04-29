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
import { FileRouteService } from '../../../Services/file-route.service';
import { VideoListComponent } from "../../video/video-list/video-list.component";

@Component({
    selector: 'app-user-profile',
    standalone: true,
    templateUrl: './user-profile.component.html',
    styleUrl: './user-profile.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        MaterialModule,
        RouterModule,
        VideoListComponent
    ]
})
export class UserProfileComponent implements OnInit {
  profilePhoto:string|null = null;
  isAdmin:boolean = false;
  isMyProfile:boolean = false;
  isFolowing:boolean = false;

  isEditing:boolean = false;
  userId: string = "";
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
    private auths:AuthService,
    private frs:FileRouteService
  ) { }

  ngOnInit() {
    this.setUser();
    
    this.isAdmin = this.auths.isAdmin()
  }
  setUser(){
    this.route.paramMap.subscribe(params => {
      this.userId = params.get('userId')!;
      this.user$ = this.up.getUser(this.userId)
      .pipe( 
        tap(u => {
          if(u.id === this.auths.getUserId()){
            this.isMyProfile = true;
          }
          if(u.photoPath !== null && u.photoPath !== undefined && u.photoPath !== ""){
            this.profilePhoto = this.frs.getImageRoute(u.photoPath);
          }   
          this.isFolowing = u.doSubscribed;       
        })
      );
      this.vs.GetVideosOfUser(this.userId).subscribe( result =>{
        this.userVideos = result;
      });
      this.vs.GetLikedVideos(this.userId).subscribe( result =>{
        this.userLikedVideos = result;
      });
      this.vs.GetDislikedVideos(this.userId).subscribe( result =>{
        this.userDislikedVideos = result;
      });
    })
  }
  
  getUserVideos(){
    this.vs.GetVideosOfUser(this.userId).subscribe( result =>{
      this.userVideos = result;
    });
  }

  subscribe(){
    this.up.subscribeUser(this.userId);
    this.isFolowing = !this.isFolowing
  }
}
