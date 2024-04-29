import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { VideoService } from '../../../Services/video.service';
import { Video } from '../../../Models/VideoModels/Video';
import { Subject, map, tap } from 'rxjs';
import { VideoApplyModel } from '../../../Models/VideoModels/VideoApplyModel';
import { AuthService } from '../../../Services/auth.service';
import { UUID } from 'crypto';
import { MaterialModule } from '../../../material/material.module';
import { RouterModule } from '@angular/router';
import { VideoItemComponent } from "../video-item/video-item.component";
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-video-list',
    standalone: true,
    templateUrl: './video-list.component.html',
    styleUrl: './video-list.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        MaterialModule,
        RouterModule,
        VideoItemComponent,
        FormsModule
    ]
})
export class VideoListComponent {
  @Input() videos: Video[] = [];
  @Input() isAuthor:boolean = false;
  @Input() isAddable:boolean = false;

  @Output() onVideoAdd = new EventEmitter<boolean>();  
  
  isAddingVideo:boolean = false;

  videoName:string = "";
  videoDescription: string = "";

  selectedVideoFile: File | null = null;
  selectedImageFile: File | null = null;

  onItemDeleted = new Subject<any>();
  onItemAdded = new Subject<any>();
  
  constructor(
    private vs:VideoService,
    private auths:AuthService,
  ) {}
  
  postVideo(){
    if(this.videoName!== "" && this.videoDescription !== ""){
      this.vs.addVideo(
        new VideoApplyModel(this.auths.getUserId()!, this.videoName, this.videoDescription),
        this.selectedVideoFile!,
        this.selectedImageFile!
      )
        .pipe(
          map(data =>{
            this.videos?.push(data);
          }),
          tap(item => this.onItemAdded.next(item))
        ).subscribe(res =>{
          this.videoName = "";
          this.videoDescription = "";
          this.isAddingVideo = false;
          this.onVideoAdd.emit(true);
        });
    }
  }

  deleteVideo(video:Video){
    this.vs.deleteVideo(video.id!)
      .pipe(
        map(data => {
          this.removeById(this.videos, video.id!);
          console.log(this.videos.length);
        }),
        tap(items => this.onItemDeleted.next(items)),
      ).subscribe();
  }
  updateElement(fromItems : Video[], id: UUID, newElement:Video) {
    const index1  = fromItems.findIndex((element) => {
      return element.id === id;
    });
    if (index1 >= 0 ) {
      fromItems[index1] = newElement;
    }
    return fromItems;
  }
  removeById(fromItems : Video[], id: UUID) {
    const index1  = fromItems.findIndex((element) => {
      return element.id === id;
    });
    if (index1 >= 0 ) {
      fromItems.splice(index1,1);
    }
    return fromItems;
  }  

  onVideoSelected(event : any){
    this.selectedVideoFile = <File>event.target.files.item(0);
  }

  onImageSelected(event : any){
    this.selectedImageFile = <File>event.target.files.item(0);
  }
}
