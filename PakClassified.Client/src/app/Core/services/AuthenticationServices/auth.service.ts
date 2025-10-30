import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BehaviorSubject, tap } from 'rxjs';
import { apiBaseUrl } from '../../../environments/environment.dev';
import { User } from '../../Model/User/User.model';
// import { apiBaseUrl } from '../../environments/environment.dev';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = apiBaseUrl+'Auth';
  private currentUserSubject = new BehaviorSubject<any>(this.getUserFromStorage());
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient, private router: Router, private snackBar: MatSnackBar) {}

  login(email: string, password: string) {
    return this.http.post<{token: string, payload: any}>(`${this.baseUrl}/Login`, { email, password })
      .pipe(
        tap(res => {
          localStorage.setItem('jwt_token', res.token);
          localStorage.setItem('user', JSON.stringify(res.payload));
          this.currentUserSubject.next(res.payload);
        })
      );
  }

  logout() {
    localStorage.removeItem('jwt_token');
    localStorage.removeItem('user');
    this.currentUserSubject.next(null);
    this.snackBar.open('Logged out successfully!', '', { duration: 3000, panelClass: ['success-snackbar'] });
    this.router.navigate(['/login']);
  }

  getToken() {
    return localStorage.getItem('jwt_token');
  }

  forgotPassword(email: string) {
    return this.http.post(`${this.baseUrl}/forgotpassword`, { email });
}


verifySecurityAnswer(data: { email: string; securityAnswer: string }) {
    return this.http.post(`${this.baseUrl}/verifysecurityanswer`, data);
}

resetPassword(data: { email: string; newPassword: string }) {
    return this.http.post(`${this.baseUrl}/resetpassword`, data);
}
getSecurityQuestion(email: string) {
    return this.http.post(`${this.baseUrl}/forgotpassword`, { email });
}

getCurrentUser() {
  return JSON.parse(localStorage.getItem('currentUser') || '{}');
}

saveCurrentUser(user: any) {
  localStorage.setItem('currentUser', JSON.stringify(user));
}

setUserInStorage(user: User): void {

    localStorage.setItem('currentUser', JSON.stringify(user));
    localStorage.setItem('user', JSON.stringify(user)); 
    this.currentUserSubject.next(user);
}
  getUserFromStorage() {
    const userStr = localStorage.getItem('user') || localStorage.getItem('currentUser');
    return userStr ? JSON.parse(userStr) : null;
}

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  isAdmin(): boolean {
  const user = this.getUserFromStorage();
  return user && user.role?.name === 'Admin';
}
}
