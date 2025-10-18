import { Routes } from '@angular/router';
import { HomeComponent } from './Shared/home/home.component';
import { AboutUsComponent } from './Shared/about-us/about-us.component';
import { ErrorPageComponent } from './Shared/error-page/error-page.component';
import { AdvertisementCategoryComponent } from './Shared/advertisement-category/advertisement-category.component';
import { CarDetailsComponent } from './Shared/car-details/car-details.component';
import { ContactUsComponent } from './Shared/contact-us/contact-us.component';
import { PostAdvertisementComponent } from './Shared/post-advertisement/post-advertisement.component';
import { LoginComponent } from './Shared/login/login.component';
import { SignupComponent } from './Shared/signup/signup.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { UserDashboardComponent } from './Shared/user-dashboard/user-dashboard.component';
import { AdminGuard } from './Core/services/AuthenticationServices/admin.guard';
import { AuthGuard } from './Core/services/AuthenticationServices/auth.guard';
import { UnauthorizedPageComponent } from './auth/unauthorized-page/unauthorized-page.component';
import { LoginGuard } from './Core/services/AuthenticationServices/login.guard';
import { SubcategoryDetailComponent } from './Shared/sub-category-details/sub-category-details.component';
import { ForgotPasswordComponent } from './Shared/forgot-password/forgot-password.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'about',
    component: AboutUsComponent
  },
  {
    path: 'Category/:name/:id',
    component: AdvertisementCategoryComponent
  },
  {
    path: 'Category/:categoryName/:categoryId/sub/:subCategoryName/:subCategoryId',
    component: SubcategoryDetailComponent
  },
  {
    path: 'carDetail/:name/:id',
    component: CarDetailsComponent
  },
  {
    path: 'post-ad',
    component: PostAdvertisementComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [LoginGuard]
  },
  {
    path: 'signup',
    component: SignupComponent,
    canActivate: [LoginGuard]
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordComponent
  },
  {
    path: 'contact',
    component: ContactUsComponent
  },
  { 
  path: 'admin', 
  canActivate: [AdminGuard],
  children: [
    { path: 'dashboard', component: AdminDashboardComponent },
  ]
},
  {
    path: 'unauthorized',
    component: UnauthorizedPageComponent
  },
  {
    path: 'user/dashboard',
    component: UserDashboardComponent,
    canActivate: [AuthGuard]
  },
  {
    path: '**',
    component: ErrorPageComponent
  }
];
