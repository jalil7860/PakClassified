import { Component, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../Core/services/AuthenticationServices/auth.service';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
    authservice = inject(AuthService)
    isNavbarCollapsed = true;

    toggleNavbar() {
      this.isNavbarCollapsed = !this.isNavbarCollapsed;
    }

    closeNavbar() {
      this.isNavbarCollapsed = true;
    }
}