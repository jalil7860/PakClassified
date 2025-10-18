import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { NotificationService } from '../../Core/Common/Notification/notification.service';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { FormControl, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../Core/services/AuthenticationServices/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [RouterLink, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
    notificationService = inject(NotificationService);
    loadingService = inject(LoadingService);
    authService = inject(AuthService);
    router = inject(Router)


    loginForm = new FormGroup({
      email: new FormControl ('', [Validators.required, Validators.email]),
      password: new FormControl ('', [Validators.required, Validators.minLength(8)])
    });

    LoginSubmit(){
      this.loadingService.show();
      if(this.loginForm.invalid){
        console.warn("Form is invalid: ", this.loginForm.value)

        this.loadingService.hide();
        this.notificationService.showError('Invalid Credentials!')
        return;
      }
      const {email, password} = this.loginForm.value;
      console.log("Calling login APi With: ", email, password);

      this.loadingService.show();

      this.authService.login(email!, password!).subscribe({
        next: (res: any) => {
          console.log("Login Response: ", res);
          this.loadingService.hide()
          this.router.navigate(['user/dashboard'])
          this.notificationService.showSuccess("User Login Successfully!")
        },error: (err: any) => {
          this.loadingService.hide();
          this.notificationService.showError("Backend Invalid Credentials")
        }
      })
    }

    get email_login(){
      return this.loginForm.get('email');
    }
    get password_login(){
      return this.loginForm.get('password')
    };
}
