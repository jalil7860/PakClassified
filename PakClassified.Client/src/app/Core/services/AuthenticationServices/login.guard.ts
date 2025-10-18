import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service'; // Apni auth service import karo

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {
  
  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    // Check if user is already logged in
    const isLoggedIn = this.authService.isLoggedIn();
    
    if (isLoggedIn) {
      // User already logged in hai, dashboard pe redirect karo
      const userRole = this.authService.getUserFromStorage().role.name;
      
      if (userRole === 'Admin') {
        this.router.navigate(['/admin/dashboard']);
      } else { 
        this.router.navigate(['/user/dashboard']);
      }
      
      return false; // Login page pe jane nahi denge
    }
    
    // User logged in nahi hai, login page pe ja sakta hai
    return true;
  }
}