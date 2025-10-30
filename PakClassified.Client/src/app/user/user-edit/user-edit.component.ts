import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../Core/services/AuthenticationServices/auth.service';
import { UserService } from '../../Core/services/UserServices/User.service';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { NotificationService } from '../../Core/Common/Notification/notification.service';
import { User } from '../../Core/Model/User/User.model';

@Component({
  selector: 'app-user-edit',
  imports: [ReactiveFormsModule],
  templateUrl: './user-edit.component.html',
  styleUrl: './user-edit.component.css'
})
export class UserEditComponent implements OnInit {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private userService = inject(UserService);
  private loadingService = inject(LoadingService);
  private notificationService = inject(NotificationService);

  userForm!: FormGroup;
  currentUser!: User;

  ngOnInit(): void {
    this.currentUser = this.authService.getUserFromStorage()!;
    this.initializeForm();
  }

  initializeForm(): void {
    const birthDate = this.currentUser.dateOfBirth
      ? new Date(this.currentUser.dateOfBirth).toISOString().split('T')[0]
      : '';

    this.userForm = this.fb.group({
      name: [this.currentUser.name, Validators.required],
      email: [this.currentUser.email, [Validators.required, Validators.email]],
      contactNumber: [this.currentUser.contactNumber || ''],
      dateOfBirth: [birthDate],
      image: [this.currentUser.image || '']
    });
  }

  onSubmit(): void {
    if (this.userForm.valid) {
      this.loadingService.show();

      const formData = {
        ...this.userForm.value,
        id: this.currentUser.id,
        roleId: this.currentUser.roleId,
        role: this.currentUser.role,
        dateOfBirth: this.userForm.value.dateOfBirth
          ? new Date(this.userForm.value.dateOfBirth)
          : new Date()
      };

      this.userService.update(this.currentUser.id, formData).subscribe({
        next: (updatedUser) => {
          this.loadingService.hide();
          this.notificationService.showSuccess('Profile updated successfully');

          updatedUser.role = updatedUser.role || this.currentUser.role;
          updatedUser.roleId = updatedUser.roleId || this.currentUser.roleId;

          //Update in local storage
          this.authService.setUserInStorage(updatedUser);
          this.currentUser = updatedUser;
        },
        error: (err) => {
          this.loadingService.hide();
          this.notificationService.showError('Error updating profile', 'Error Updating');
          console.error('Update error:', err);
        }
      });
    }
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      if (file.size > 1048576) {
        this.notificationService.showError('Image size should be less than 1MB', 'File Size Error');
        return;
      }

      const reader = new FileReader();
      reader.onload = () => {
        const base64String = reader.result as string;
        this.userForm.patchValue({ image: base64String });
      };
      reader.readAsDataURL(file);
    }
  }
}
