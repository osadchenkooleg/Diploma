import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from './Services/auth.service';
import { MaterialModule } from './material/material.module';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    MaterialModule,
    RouterModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'VideoHostingUI';

  constructor(
    private as: AuthService
  ) {}

  public get isLoggedIn() : boolean{
    return this.as.isAuthenticated();
  }

  // login(username:string, password:string){
  //   this.as.login(username, password)
  //     .subscribe({
  //       complete: () => {
  //         this.router.navigate(['']);
  //       }
  //       , error: () => {
  //         alert('Wrong login or email')
  //       }
  //     })
  // }
}
