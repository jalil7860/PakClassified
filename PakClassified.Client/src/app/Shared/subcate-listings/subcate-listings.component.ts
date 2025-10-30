import { Component, inject, OnInit } from '@angular/core';
import { AdvertisementService } from '../../Core/services/PakClassified/advertisement.service';
import { ActivatedRoute, Router, RouterLink, RouterOutlet } from '@angular/router';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { CommonModule } from '@angular/common';
import { AdvertisementModel } from '../../Core/Model/Advertisment/Advertisement.model';
import { CityArea } from '../../Core/Model/location/cityArea.model';
import { CityAreaService } from '../../Core/services/LoactionServices/city-area.service';

@Component({
  selector: 'app-subcate-listings',
  imports: [RouterLink, CommonModule],
  templateUrl: './subcate-listings.component.html',
  styleUrl: './subcate-listings.component.css'
})
export class SubcateListingsComponent implements OnInit{
  advertisementService = inject(AdvertisementService)
  cityAreaService = inject(CityAreaService)
  loadingService = inject(LoadingService)
  route = inject(ActivatedRoute)

  Listings: AdvertisementModel[] = []
  cityAreas : CityArea[] = []
  SubCategoryId: any = '';
  subCategoryName: any = ''
  totalListings: any
  categoryId: any
  categoryName: any = '';

  formatPrice(price: number | undefined): string {
    if (!price) return '0';
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  }

  ngOnInit(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
    
    this.loadingService.show();
    
    this.route.queryParams.subscribe(params => {
      this.SubCategoryId = params['subCategory'];
      this.subCategoryName = params['subCategoryName'];
      this.categoryId = params['categoryId'];
      this.categoryName = params['category']; // Fixed parameter name
      
      console.log('Query Params:', {
        subCategory: this.SubCategoryId,
        subCategoryName: this.subCategoryName,
        category: this.categoryId,
        categoryName: this.categoryName
      });

      if(this.SubCategoryId) {
        this.loadListings();
      } else {
        this.loadingService.hide();
        console.error('No SubCategory ID found in query params');
      }
    });
  }

  loadListings() {
    this.loadingService.show();
    this.advertisementService.getAll().subscribe({
      next: (data: AdvertisementModel[]) => {
        // Double equals use karo type coercion ke liye
        
        this.Listings = data.filter(a => a.subCategoryId == this.SubCategoryId);
        console.log(this.Listings);
        this.totalListings = this.Listings.length;
        this.loadingService.hide();
      },
      error: (err) => {
        console.log("Error loading listings:", err);
        this.loadingService.hide();
      }
    });
  }
}