import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { UserApplyModel } from '../../../Models/AuthModels/UserApplyModel';
import { FormControl, Validators } from '@angular/forms';
import { AuthService } from '../../../Services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MaterialModule } from '../../../material/material.module';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule
  ],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegistrationComponent {
  constructor(
    private as: AuthService,
    private route: ActivatedRoute,
    private router: Router,
  ) {}
  email = new FormControl('', [Validators.required, Validators.email]);
  ngOnInit() {
  }
  public get isLoggedIn() : boolean{
    return this.as.isAuthenticated();
  }
  login(username:string, password:string){
    this.as.login(username, password)
      .subscribe({
        complete: () => {
          this.router.navigate(['']);
        }
        , error: () => {
          alert('Wrong login or email')
        }
      })
  }
  register( 
    { email, password, passwordConfirm, phoneNumber, firstname, surname, faculty, group, sex }: 
    { 
      email: string; 
      password: string; 
      passwordConfirm: string; 
      phoneNumber: string; 
      firstname: string; 
      surname: string; 
      faculty: string; 
      group: string; 
      sex: boolean; 
  }): void {
    this.as.register(new UserApplyModel(email, password, passwordConfirm, phoneNumber, firstname, surname, faculty, group, sex))
      .subscribe({
        complete: () => {
          this.login(email, password)
        }
        , error: () => {
          alert('Error occured!')
        }
      })
  }
  getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'You must enter a value';
    }

    return this.email.hasError('email') ? 'Not a valid email' : '';
  }
}
