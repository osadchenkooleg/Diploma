import { Inject, Injectable } from '@angular/core';
import { HOSTING_API_URL } from '../app-injection-tokens';
import { IMAGES_ROUTE, VIDEOS_ROUTE, VIDEO_IMAGES_ROUTE } from '../constants/wwwroot-constants';

@Injectable({
  providedIn: 'root'
})
export class FileRouteService {

  constructor(
    @Inject(HOSTING_API_URL) private apiUrl:string,
  ) { }

  getImageRoute(fileId: string) {
    return this.apiUrl + `${IMAGES_ROUTE}/` + fileId;
  }

  getVideoImageRoute(fileId: string) {
    return this.apiUrl + `${VIDEO_IMAGES_ROUTE}/` + fileId;
  }

  getVideoRouteRoute(fileId: string) {
    return this.apiUrl + `${VIDEOS_ROUTE}/` + fileId;
  }
}
