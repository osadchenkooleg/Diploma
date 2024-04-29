import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { User } from '../../../Models/UserModels/User';
import { HOSTING_API_URL } from '../../../app-injection-tokens';
import { MaterialModule } from '../../../material/material.module';
import { UserService } from '../../../Services/user.service';
import { FileRouteService } from '../../../Services/file-route.service';
import { Observable, tap } from 'rxjs';

@Component({
  selector: 'app-user-item',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
  ],
  templateUrl: './user-item.component.html',
  styleUrl: './user-item.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UserItemComponent implements OnInit {
  profilePhoto:string|null = null;

  @Input() userId:string | null = null;

  user$: Observable<User> | undefined; 

  constructor(
    @Inject(HOSTING_API_URL) private apiUrl:string,
    private route: ActivatedRoute,
    private router: Router,
    private up: UserService,
    private frs:FileRouteService
  ) { }

  ngOnInit() {
    this.user$ = this.up.getUser(this.userId!)
    .pipe(
      tap(u =>{
        if(u.photoPath !== null && u.photoPath !== undefined && u.photoPath !== ""){
          this.profilePhoto = this.frs.getImageRoute(u.photoPath);
        } 
      })
    );

  }
  navigateToProfile() : string{
    return location.origin+`/user/${this.userId}`;
  }
}
