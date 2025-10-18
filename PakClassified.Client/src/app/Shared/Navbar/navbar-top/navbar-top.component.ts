import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../Core/services/AuthenticationServices/auth.service';

@Component({
  selector: 'app-navbar-top',
  imports: [RouterLink],
  templateUrl: './navbar-top.component.html',
  styleUrl: './navbar-top.component.css'
})
export class NavbarTopComponent {
 authservice = inject(AuthService);
}
