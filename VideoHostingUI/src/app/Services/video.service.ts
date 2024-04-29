import { UUID } from 'crypto';
import { VideoApplyModel } from './../Models/VideoModels/VideoApplyModel';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { HOSTING_API_URL } from '../app-injection-tokens';
import { Video } from '../Models/VideoModels/Video';

@Injectable({
  providedIn: 'root'
})
export class VideoService {
  hostingUrl: string;
  constructor(
      private http: HttpClient,
      @Inject(HOSTING_API_URL) private apiUrl:string
    ) {
      this.hostingUrl = apiUrl + 'api/Video/';
  }

  addVideo(videoApplyModel: VideoApplyModel, video: File, videoImage: File) {
    const formData = new FormData(); 
    formData.append("file", video, video.name);
    formData.append("file", videoImage, videoImage.name);
    return this.http.post<Video>(this.hostingUrl + `video?userId=${videoApplyModel.userId}&name=${videoApplyModel.name}&description=${videoApplyModel.description}`, formData );
  }

  deleteVideo(id: UUID) {
    return this.http.delete(this.hostingUrl + `video/${id}`);
  }

  getVideoById(id: UUID) {
    return this.http.get<Video>(this.hostingUrl + `video/${id}`);
  }

  GetVideosOfUser(userId: string) {
    return this.http.get<Video[]>(this.hostingUrl + `video/user/${userId}`);
  }

  GetVideosSubscribers() {
    return this.http.get<Video[]>(this.hostingUrl + `video`);
  }

  GetLikedVideos(userId: string){
    return this.http.get<Video[]>(this.hostingUrl + `video/liked/${userId}`);
  }
  
  GetDislikedVideos(userId: string) {
    return this.http.get<Video[]>(this.hostingUrl + `video/disliked/${userId}`);
  }

  GetVideosName(videoName: string){
    return this.http.get<Video[]>(this.hostingUrl + `video/search?name=${videoName}`);
  }

  PutLike(videoId: UUID) {
    return this.http.put(this.hostingUrl + `like/${videoId}`, undefined);
  }

  PutDislike(videoId: UUID){
    return this.http.put(this.hostingUrl + `dislike/${videoId}`, undefined);
  }
}
