import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MaterialModule } from '../../../material/material.module';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';

@Component({
  selector: 'app-my-profile',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule
  ],
  templateUrl: './my-profile.component.html',
  styleUrl: './my-profile.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MyProfileComponent {
  constructor(
    private as: AuthService,
    private router: Router,
    // private up: UserService
  ) { }
  logout(){
    this.as.logout();
  }
}
