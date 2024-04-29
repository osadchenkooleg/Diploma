import { UUID } from 'crypto';
import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';
import { FileRouteService } from '../../../Services/file-route.service';
import { VideoService } from '../../../Services/video.service';
import { MaterialModule } from '../../../material/material.module';
import { Video } from '../../../Models/VideoModels/Video';
import { Observable, Subject, tap } from 'rxjs';
import { UserItemComponent } from "../../user/user-item/user-item.component";
import { Commentary } from '../../../Models/CommentaryModels/Commentary';
import { CommentaryService } from '../../../Services/commentary.service';
import { CommentListComponent } from "../../comments/comment-list/comment-list.component";

@Component({
    selector: 'app-video-details',
    standalone: true,
    templateUrl: './video-details.component.html',
    styleUrl: './video-details.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        MaterialModule,
        RouterModule,
        UserItemComponent,
        CommentListComponent
    ]
})
export class VideoDetailsComponent { 
  constructor(
    private route: ActivatedRoute,
    private vs: VideoService,
    private cs: CommentaryService,
    private as:AuthService,
    private frs:FileRouteService
  ) { }

  videoPhotoRoute:string|null = null;
  videoRoute:string|null = null;

  isAdmin:boolean = false;
  isMyProfile:boolean = false;

  video$: Observable<Video> | undefined;
  videoId: UUID | undefined;
  videoCommentaries: Commentary[] = []; 

  isLiked:boolean = false;
  isDisliked:boolean = false;

  likes: number = 0;
  dislikes: number = 0;

  ngOnInit() {
    this.setVideo();

    this.isAdmin = this.as.isAdmin();
  }

  setVideo(){
    this.route.paramMap.subscribe(params => {
      let videoId = <UUID>params.get('videoId')!;
      this.video$ = this.vs.getVideoById(videoId)
        .pipe( 
          tap(v => {   
            if(v.id === this.as.getUserId()){
              this.isMyProfile = true;
            }
            if(v.photoPath != null && v.photoPath !== undefined && v.photoPath !== ""){
              this.videoPhotoRoute = this.frs.getVideoImageRoute(v.photoPath);
              console.log('this.videoPhotoRoute: ' + this.videoPhotoRoute);
            }   
            if(v.videoPath != null && v.videoPath !== undefined && v.videoPath !== ""){
              this.videoRoute = this.frs.getVideoRouteRoute(v.videoPath);
              console.log('this.videoRoute: ' + this.videoRoute);
            } 
            
            this.cs.getCommentariesByVideoId(v.id).subscribe( result =>{
              this.videoCommentaries = result;
            });

            this.videoId = v.id;
            this.isLiked = v.liked;
            this.isDisliked = v.disliked;
            console.log('isLiked: ' + this.isLiked);
            console.log('isDisliked: ' + this.isDisliked);
            this.likes = v.likes;
            this.dislikes = v.dislikes;
          })
        );
    })
  }

  likeVideo(){
    if(this.isDisliked){
      this.dislikes--;
      this.isDisliked = false;
    }
    this.likes++;
    this.isLiked = true;
    this.vs.PutLike(this.videoId!).subscribe();
  }

  dislikeVideo(){
    if(this.isLiked){
      this.likes--;
      this.isLiked = false;
    }
    this.dislikes++;
    this.isDisliked = true;
    this.vs.PutDislike(this.videoId!).subscribe();
  }
}
