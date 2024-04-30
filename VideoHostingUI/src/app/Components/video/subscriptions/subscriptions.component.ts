import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { Video } from '../../../Models/VideoModels/Video';
import { AuthService } from '../../../Services/auth.service';
import { FileRouteService } from '../../../Services/file-route.service';
import { UserService } from '../../../Services/user.service';
import { VideoService } from '../../../Services/video.service';
import { HOSTING_API_URL } from '../../../app-injection-tokens';
import { MaterialModule } from '../../../material/material.module';
import { VideoListComponent } from '../video-list/video-list.component';

@Component({
  selector: 'app-subscriptions',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    VideoListComponent
  ],
  templateUrl: './subscriptions.component.html',
  styleUrl: './subscriptions.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SubscriptionsComponent implements OnInit { 
  constructor(
    private route: ActivatedRoute,
    @Inject(HOSTING_API_URL) private apiUrl:string,
    private up: UserService,
    private vs: VideoService,
    private auths:AuthService,
    private frs:FileRouteService
  ) { }

  videos$: Observable<Video[]> | undefined; 

  ngOnInit() {
    this.setVideos();
  }

  setVideos(){
    this.videos$ =this.vs.GetVideosSubscribers();
  }
}
