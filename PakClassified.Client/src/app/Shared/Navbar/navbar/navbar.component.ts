import { Component, inject, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../Core/services/AuthenticationServices/auth.service';
import { AdvertisementService } from '../../../Core/services/PakClassified/advertisement.service';
import { AdvertisementCategoryService } from '../../../Core/services/PakClassified/AdvertisementCategory.service';
import { LoadingService } from '../../../Core/Common/Loading/loading.service';
import { AdvertisementCategory } from '../../../Core/Model/Advertisment/AdvertisementCategory.model';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit{
    categoryService = inject(AdvertisementCategoryService);
    loadingService = inject(LoadingService)
    authservice = inject(AuthService)
    isNavbarCollapsed = true;

    Categories: AdvertisementCategory [] = []
    ngOnInit(): void {
      this.loadingService.show();
      this.categoryService.getAll().subscribe({
        next: (data: AdvertisementCategory[]) => {
          this.Categories = data;
          this.loadingService.hide();
        },error : (err) =>{
          console.log("ERROR WHILE LOADING: ", err);
          this.loadingService.hide();
        }
      })
    }


    toggleNavbar() {
      this.isNavbarCollapsed = !this.isNavbarCollapsed;
    }

    closeNavbar() {
      this.isNavbarCollapsed = true;
    }
}