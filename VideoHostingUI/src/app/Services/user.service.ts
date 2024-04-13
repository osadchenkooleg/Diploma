import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { HOSTING_API_URL } from '../app-injection-tokens';
import { LoginUserModel } from '../Models/CredentialModels/UserEmailLoginModel';
import { Observable } from 'rxjs';
import { UserUpdateModel } from '../Models/UserModels/UserUpdateModel';
import { User } from '../Models/UserModels/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  hostingUrl: string;
  constructor(
      private http: HttpClient,
      @Inject(HOSTING_API_URL) private apiUrl:string
    ) {
      this.hostingUrl = apiUrl + 'api/User/';
  }

  isUserExists(email: string) : Observable<boolean> {
    var model = new LoginUserModel(email);
    return this.http.post<boolean>(this.hostingUrl + `exist`, model);
  }

  addPhoto(file: File) {
    const formData = new FormData(); 
    formData.append("file", file, file.name);

    this.http.post(this.hostingUrl + `addPhoto`, formData);
  }

  subscribeUser(userId: string) {
    this.http.put(this.hostingUrl + `subscribe/${userId}`, null);
  }

  updateUser(userUpdateModel: UserUpdateModel) {
    this.http.put(this.hostingUrl + `updateUser`, userUpdateModel);
  }

  getUser(id: string) {
    return this.http.get<User>(this.hostingUrl + `profileUser/${id}`);
  }

  getMyProfile() {
    return this.http.get<User>(this.hostingUrl + `profileUser/my`);
  }

  getSubscribers(id: string) {
    return this.http.get<User[]>(this.hostingUrl + `subscribers`);
  }

  getSubscriptions() {
    return this.http.get<User[]>(this.hostingUrl + `subscriptions`);
  }
  
  SearchUserByName(userName: string) {
    return this.http.get<User[]>(this.hostingUrl + `findByName/${userName}`);
  }
}
