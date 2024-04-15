import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { MaterialModule } from '../../../material/material.module';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AuthService, USERID } from '../../../Services/auth.service';
import { HOSTING_API_URL } from '../../../app-injection-tokens';

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
export class MyProfileComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private as: AuthService,
    @Inject(HOSTING_API_URL) private apiUrl:string
  ) { }

  ngOnInit() {
    var userId = localStorage.getItem(USERID);
    this.router.navigate([`/user/${userId}`], {relativeTo: this.route});
  }

  logout(){
    this.as.logout();
  }
}
