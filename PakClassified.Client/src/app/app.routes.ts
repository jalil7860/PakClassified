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
import { UserDashboardComponent } from './user/user-dashboard/user-dashboard.component';
import { AdminGuard } from './Core/services/AuthenticationServices/admin.guard';
import { AuthGuard } from './Core/services/AuthenticationServices/auth.guard';
import { UnauthorizedPageComponent } from './auth/unauthorized-page/unauthorized-page.component';
import { LoginGuard } from './Core/services/AuthenticationServices/login.guard';
import { SubcategoryDetailComponent } from './Shared/sub-category-details/sub-category-details.component';
import { ForgotPasswordComponent } from './Shared/forgot-password/forgot-password.component';
import { PrivacyPolicyComponent } from './pages/privacy-policy/privacy-policy.component';
import { TermServiceComponent } from './pages/term-service/term-service.component';
import { CookiePolicyComponent } from './pages/cookie-policy/cookie-policy.component';
import { SubcateListingsComponent } from './Shared/subcate-listings/subcate-listings.component';


import { UserLayoutComponent } from './user/user-layout/user-layout.component';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { AdEditComponent } from './user/ad-edit/ad-edit.component';

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
    path:'listings',
    component: SubcateListingsComponent
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
    path: 'privacy-policy',
    component: PrivacyPolicyComponent
  },
  {
    path: 'terms-and-conditions',
    component: TermServiceComponent
  },
  {
    path: 'cookie-policy',
    component: CookiePolicyComponent
  },

  { 
    path: 'admin',
    canActivate: [AdminGuard],
    children: [
      { path: 'dashboard', component: AdminDashboardComponent },
    ]
  },

  {
    path: 'user',
    component: UserLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: UserDashboardComponent },
      { path: 'edit', component: UserEditComponent },
      { path: 'edit-ad/:id', component: AdEditComponent }
    ]
  },
  {
    path: 'unauthorized',
    component: UnauthorizedPageComponent
  },
  {
    path: '**',
    component: ErrorPageComponent
  }
];