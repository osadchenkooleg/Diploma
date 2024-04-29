import { CommentaryApplyModel } from './../Models/CommentaryModels/CommentaryApplyModel';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginUserModel } from '../Models/CredentialModels/UserEmailLoginModel';
import { HOSTING_API_URL } from '../app-injection-tokens';
import { UUID } from "crypto";
import { Commentary } from '../Models/CommentaryModels/Commentary';

@Injectable({
  providedIn: 'root'
})
export class CommentaryService {
  hostingUrl: string;
  constructor(
      private http: HttpClient,
      @Inject(HOSTING_API_URL) private apiUrl:string
    ) {
      this.hostingUrl = apiUrl + 'api/Commentary';
  }

  getCommentariesByVideoId(videoId: UUID) {
    return this.http.get<Commentary[]>(this.hostingUrl + `/${videoId}`);
  }

  createCommentary(commentaryApplyModel: CommentaryApplyModel) {
    return this.http.post<Commentary>(this.hostingUrl, commentaryApplyModel);
  }

  deleteCommentary(commentaryId: UUID) {
    return this.http.delete<Commentary>(this.hostingUrl + `/${commentaryId}`);
  }
}
