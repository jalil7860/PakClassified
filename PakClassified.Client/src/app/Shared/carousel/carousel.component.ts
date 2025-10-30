import { Component, inject } from '@angular/core';
import { AuthService } from '../../Core/services/AuthenticationServices/auth.service';
import { RouterLink } from '@angular/router';
// import { RouterLink } from "../../../../node_modules/@angular/router/router_module.d-Bx9ArA6K";

@Component({
  selector: 'app-carousel',
  imports: [RouterLink],
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.css'
})
export class CarouselComponent {
  authService = inject(AuthService);
  search(event: Event){
    event.preventDefault();
  }
}
