import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';
import { MaterialModule } from '../../../material/material.module';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent { 
  constructor(
    private as: AuthService,
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  public error : string | undefined;

  ngOnInit() {
  }
  public get isLoggedIn() : boolean{
    return this.as.isAuthenticated();
  }
  login(username:string, password:string){
    this.error = undefined;
    this.as.login(username, password)
      .subscribe({
        complete: () => {
          this.router.navigate(['']);
        }
        , error: err => {
          alert('Wrong login or email');
          this.error = err.error.message;
        }
      })
  }
}
