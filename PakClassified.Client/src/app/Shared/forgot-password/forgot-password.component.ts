import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../Core/services/AuthenticationServices/auth.service';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { NotificationService } from '../../Core/Common/Notification/notification.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  imports: [FormsModule, CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private loadingService = inject(LoadingService);
  private notificationService = inject(NotificationService);

  currentStep = 1;
  userEmail = '';
  securityQuestion = '';

  // Step 1: Email Form
  emailForm: FormGroup = this.fb.group({
    email: ['', [Validators.required, Validators.email]]
  });

  // Step 2: Security Answer Form
  securityForm: FormGroup = this.fb.group({
    securityAnswer: ['', [Validators.required]]
  });

  // Step 3: Reset Password Form
  resetForm: FormGroup = this.fb.group({
    newPassword: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', [Validators.required]]
  }, { validators: this.passwordMatchValidator });

  // Custom validator for password match
  passwordMatchValidator(g: FormGroup) {
    return g.get('newPassword')?.value === g.get('confirmPassword')?.value
      ? null : { mismatch: true };
  }

  // Step 1: Email Submit
  onEmailSubmit() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
    if (this.emailForm.valid) {
      this.loadingService.show();
      this.userEmail = this.emailForm.value.email;

      this.authService.forgotPassword(this.userEmail).subscribe({
        next: (response: any) => {
          this.loadingService.hide();
          this.securityQuestion = response.securityQuestion;
          this.currentStep = 2;
        },
        error: (err: any) => {
          console.log(err);
          
          this.loadingService.hide();
          this.notificationService.showError('User not found with this email');
        }
      });
    }
  }

  // Step 2: Security Answer Submit
  onSecurityAnswerSubmit() {
    if (this.securityForm.valid) {
      this.loadingService.show();
      
      const data = {
        email: this.userEmail,
        securityAnswer: this.securityForm.value.securityAnswer
      };

      this.authService.verifySecurityAnswer(data).subscribe({
        next: (response: any) => {
          this.loadingService.hide();
          if (response.isVerified) {
            this.currentStep = 3;
          } else {
            this.notificationService.showError('Incorrect security answer');
          }
        },
        error: (err) => {
          this.loadingService.hide();
          this.notificationService.showError('Verification failed');
        }
      });
    }
  }

  // Step 3: Reset Password
  onResetPassword() {
    if (this.resetForm.valid) {
      this.loadingService.show();
      
      const data = {
        email: this.userEmail,
        newPassword: this.resetForm.value.newPassword
      };

      this.authService.resetPassword(data).subscribe({
        next: (response: any) => {
          this.loadingService.hide();
          this.currentStep = 4;
          this.notificationService.showSuccess('Password reset successfully');
        },
        error: (err) => {
          this.loadingService.hide();
          this.notificationService.showError('Password reset failed');
        }
      });
    }
  }
}