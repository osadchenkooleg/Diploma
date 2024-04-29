import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';
import { FileRouteService } from '../../../Services/file-route.service';
import { UserService } from '../../../Services/user.service';
import { VideoService } from '../../../Services/video.service';
import { HOSTING_API_URL } from '../../../app-injection-tokens';
import { MaterialModule } from '../../../material/material.module';
import { Video } from '../../../Models/VideoModels/Video';
import { VideoListComponent } from "../video-list/video-list.component";
import { Observable } from 'rxjs';

@Component({
    selector: 'app-videos-page',
    standalone: true,
    templateUrl: './videos-page.component.html',
    styleUrl: './videos-page.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        MaterialModule,
        RouterModule,
        VideoListComponent
    ]
})
export class VideosPageComponent  implements OnInit { 
  constructor(
    private route: ActivatedRoute,
    @Inject(HOSTING_API_URL) private apiUrl:string,
    private up: UserService,
    private vs: VideoService,
    private auths:AuthService,
    private frs:FileRouteService
  ) { }

  videoName : string = "";
  videos$: Observable<Video[]> | undefined; 

  ngOnInit() {
    this.setVideos();
  }

  setVideos(){
    this.videos$ =this.vs.GetVideosName(this.videoName);
  }
}
