import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../../Core/services/UserServices/User.service';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { NotificationService } from '../../Core/Common/Notification/notification.service';
import { RoleService } from '../../Core/services/UserServices/Role.Service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { User } from '../../Core/Model/User/User.model';
import { Role } from '../../Core/Model/User/Role.model';
import { AuthService } from '../../Core/services/AuthenticationServices/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signup',
  imports: [RouterLink, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent implements OnInit{
  userService = inject(UserService);
  loadingService = inject(LoadingService);
  notificationService = inject(NotificationService);
  roleService = inject(RoleService);
  authService = inject(AuthService);
  router = inject(Router);
  
  users: User[] = [];
  roles: Role[] = [];

  SignUpForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    securityQuestion: new FormControl('', [Validators.required]),
    securityAnswer: new FormControl('', [Validators.required]),
    roleId: new FormControl('', [Validators.required]),
    birthDate: new FormControl('', [Validators.required]),
    contactNumber: new FormControl('', [Validators.required]),
    image: new FormControl('', [Validators.required]),
  });
  get name_signup(){
    return this.SignUpForm.get('name');
  }
  get email_signup(){
    return this.SignUpForm.get('email');
  }
  get password_signup(){
    return this.SignUpForm.get('password');
  }
  get securityQuestion_signup(){
    return this.SignUpForm.get('securityQuestion');
  }
  get securityAnswer_signup(){
    return this.SignUpForm.get('securityAnswer');
  }
  get roleId_signup(){
    return this.SignUpForm.get('roleId');
  }
  get birthDate_signup(){
    return this.SignUpForm.get('birthDate');
  }
  get contactNumber_signup(){
    return this.SignUpForm.get('contactNumber');
  }
  get image_signup(){
    return this.SignUpForm.get('image');
  }
  onFileSelected(event: any){
    const file = event.target.files[0];
    if(file){
      const reader = new FileReader();
      reader.onload = ()=>{
        const base64String = reader.result as string;
        this.SignUpForm.get('image')?.setValue(base64String);
      };
      reader.readAsDataURL(file);
    }
  }
  ngOnInit(): void {
    this.loadingService.show();
    this.roleService.getAll().subscribe({
      next: (data) => {
        console.log("Role", data);
        this.roles = data.map((ur :any) => ({
          id: ur.id,
          name: ur.name,
          rank: ur.rank
        }))
        this.loadingService.hide()
        
      },error :(err) => {
        this.loadingService.hide();
        console.log(err);
        
      }
    });
  };
  signupsubmit(){
    this.loadingService.show();
    console.log("Form Value: ", this.SignUpForm.getRawValue());

    if(!this.SignUpForm.valid){
      this.loadingService.hide();
      this.notificationService.showError("Please fill all required fields Correctly.");
      return;
    }
    if(this.SignUpForm.valid){
      const rawData = this.SignUpForm.getRawValue();
      const selectedRoleId = Number(rawData.roleId);

      const selectedRole = this.roles.find(role => role.id === selectedRoleId);
      
      const user = {
        id: 0,
        name: rawData.name || '',
        email: rawData.email || '',
        password: rawData.password || '',
        securityQuestion: rawData.securityQuestion || '',
        securityAnswer: rawData.securityAnswer || '',
        roleId: selectedRoleId,
        role: selectedRole,
        birthDate: rawData.birthDate? new Date(rawData.birthDate).toISOString() : new Date().toISOString(),
        contactNumber: rawData.contactNumber || '',
        image: rawData.image || ''
      };
      console.log("User object with both roleId and role: ", user);
      
      this.userService.create(user).subscribe({
        next: (data) => {
          console.log("Saved: ", data);
          this.router.navigate(['login']);
          this.loadingService.hide();
          this.notificationService.showSuccess("User Register Successfully! Please sign in to continue");
          this.SignUpForm.reset();
        },error: (err) => {
          this.loadingService.hide();
          console.log("Sign Up Error: ", err);
          
        }
      })
    }
    
  }
}