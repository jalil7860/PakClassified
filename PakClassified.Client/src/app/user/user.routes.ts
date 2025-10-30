import { Routes } from '@angular/router';
import { UserLayoutComponent } from './user-layout/user-layout.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { AuthGuard } from '../Core/services/AuthenticationServices/auth.guard';


export const userRoutes: Routes = [
  {
    path: 'user',
    component: UserLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: UserDashboardComponent },
      { path: 'edit', component: UserEditComponent }
    ]
  }
];