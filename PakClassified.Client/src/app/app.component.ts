import { Component, inject } from '@angular/core';
import { NavbarTopComponent } from './Shared/Navbar/navbar-top/navbar-top.component';
import { NavbarComponent } from './Shared/Navbar/navbar/navbar.component';
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from './Shared/footer/footer.component';
import { PageLoaderComponent } from './layout/page-loader/page-loader.component';
import { LoadingService } from './Core/Common/Loading/loading.service';
// import { c } from "../../node_modules/@angular/cdk/a11y-module.d-DBHGyKoh";



@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarTopComponent, NavbarComponent, FooterComponent, PageLoaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'pakClassified';
  loadingservice = inject(LoadingService)

}
