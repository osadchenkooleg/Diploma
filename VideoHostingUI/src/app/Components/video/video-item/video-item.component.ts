import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { Video } from '../../../Models/VideoModels/Video';
import { AuthService } from '../../../Services/auth.service';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../../../material/material.module';
import { UserItemComponent } from "../../user/user-item/user-item.component";
import { FileRouteService } from '../../../Services/file-route.service';
import { HOSTING_API_URL } from '../../../app-injection-tokens';

@Component({
    selector: 'app-video-item',
    standalone: true,
    templateUrl: './video-item.component.html',
    styleUrl: './video-item.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        MaterialModule,
        RouterModule,
        UserItemComponent
    ]
})
export class VideoItemComponent { 
  @Input() video: Video | undefined;

  videoName:string|null = null;
  videoDescription:string|null = null;

  isAuthor:boolean = false;
  isAdmin:boolean = false;

  videoPhotoPath:string|null = null;

  public get isLoggedIn() : boolean{
    return this.as.isAuthenticated();
  }
  
  @Output() videoToDelete = new EventEmitter<Video>();  

  constructor(
    private as: AuthService,
    private frs:FileRouteService,
    @Inject(HOSTING_API_URL) private apiUrl:string,
  ) {}

  ngOnInit() {
    this.isAuthor = this.getAuthor();
    this.isAdmin = this.as.isAdmin();
    this.setOriginalValues();
  }

  navigateToVideoDetails() : string{
    return location.origin + `video/${this.video!.id}`;
  }


  deleteVideo(){
    this.videoToDelete.emit(this.video);
  }
  
  getAuthor():boolean{
    if(this.video?.userId === this.as.getUserId())
      return true;
    return false;
  }
  setOriginalValues(){
    this.videoName = this.video?.name!;
    this.videoDescription = this.video?.description!;

    if(this.video?.photoPath !== null && this.video?.photoPath !== undefined && this.video?.photoPath !== ""){
      this.videoPhotoPath = this.frs.getVideoImageRoute(this.video?.photoPath);
    } 
  }
}
