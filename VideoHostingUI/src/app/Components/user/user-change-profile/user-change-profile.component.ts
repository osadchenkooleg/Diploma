import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { Observable, map, tap } from 'rxjs';
import { User } from '../../../Models/UserModels/User';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { HOSTING_API_URL } from '../../../app-injection-tokens';
import { UserService } from '../../../Services/user.service';
import { AuthService } from '../../../Services/auth.service';
import { UserUpdateModel } from '../../../Models/UserModels/UserUpdateModel';
import { MaterialModule } from '../../../material/material.module';
import { IMAGES_ROUTE } from '../../../constants/wwwroot-constants';
import { FileRouteService } from '../../../Services/file-route.service';

@Component({
  selector: 'app-user-change-profile',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule
  ],
  templateUrl: './user-change-profile.component.html',
  styleUrl: './user-change-profile.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UserChangeProfileComponent implements OnInit {
  user$: Observable<User> | undefined;
  profilePhoto:string|null = null;
  isAuthor:boolean = false;
  email = new FormControl('', [Validators.required, Validators.email]);
  userId:string = "";

  selectedFiles: File | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    @Inject(HOSTING_API_URL) private apiUrl:string,
    private us: UserService,
    private as:AuthService,
    private frs:FileRouteService
  ) { }

  ngOnInit() {
    this.setUser();
  }

  setUser(){
    this.route.paramMap.subscribe(params => {
      this.userId = params.get('userId')!;
      this.user$ = this.us.getUser(this.userId)
      .pipe( 
        tap(u => {
          if(u.id === this.as.getUserId()){
            this.isAuthor = true;
          }   
          
          if(u.photoPath !== null && u.photoPath !== undefined && u.photoPath !== ""){
            this.profilePhoto = this.frs.getImageRoute(u.photoPath);
          } 
        })
      );
    })
  }
  getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'You must enter a value';
    }

    return this.email.hasError('email') ? 'Not a valid email' : '';
  }
  updateMyProfile( 
    name:string,
    surname: string,
    faculty: string,
    group: string,
    sex: boolean
  ) {
    this.us.updateUser(new UserUpdateModel(name, surname, faculty, group, sex))
      .subscribe({
        complete: () => this.router.navigate(["user",this.userId]),
        error: err => alert('Error: ' + err)
      });
  }

  onFileSelected(event : any){
    this.selectedFiles = <File>event.target.files.item(0);
    this.onAddPhoto();
  }

  onAddPhoto(){
    if(this.selectedFiles !== null){
      this.us.addPhoto(this.selectedFiles)
      .subscribe( complete => {
        this.router.navigate(['user',this.userId]);
        },
        error => alert('Error: ' + error)
      );
      
    }
      
  }
}
