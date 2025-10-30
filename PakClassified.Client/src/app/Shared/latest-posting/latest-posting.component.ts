import { booleanAttribute, Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AdvertisementService } from '../../Core/services/PakClassified/advertisement.service';
import { AdvertisementModel } from '../../Core/Model/Advertisment/Advertisement.model';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { DatePipe } from '@angular/common';
import { CityAreaService } from '../../Core/services/LoactionServices/city-area.service';
import { CityArea } from '../../Core/Model/location/cityArea.model';
import { AuthService } from '../../Core/services/AuthenticationServices/auth.service';

@Component({
  selector: 'app-latest-posting',
  imports: [RouterLink, DatePipe],
  templateUrl: './latest-posting.component.html',
  styleUrl: './latest-posting.component.css',
})
export class LatestPostingComponent {
  advetisementService = inject(AdvertisementService);
  cityAreaService = inject(CityAreaService);
  LoadingService = inject(LoadingService);
  authService = inject(AuthService)
  CityAreas: CityArea[] = [];
  Advertisements: AdvertisementModel[] = [];

  ngOnInit(): void {
    this.LoadingService.show();

    this.cityAreaService.getAll().subscribe({
      next: (allCityAreas) => {
        this.CityAreas = allCityAreas;

        this.advetisementService.getAll().subscribe({
          next: (ad: any[]) => {
            this.Advertisements = ad.map(ad => {
              const cityAreaObj = this.CityAreas.find(ca => ca.id === ad.cityAreaId);
              return {
                ...ad,
                cityArea: cityAreaObj
              };
            }).sort((a, b) => b.id - a.id);
            this.LoadingService.hide();
          },
          error: (err) => {
            this.LoadingService.hide();
            console.log('Error: ', err);
          },
        });
      },
      error: (err) => {
        console.log('Error loading CityAreas: ', err);
      },
    });
  }
  formatPrice(price: number | undefined): string {
    if (!price) return '0';
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  }
  deletePost(id: number){
    let confirmation = confirm("Are you sure you want Delete?")
    if(confirmation){
      this.LoadingService.show();
      this.advetisementService.delete(id).subscribe({
      next: (data: any) => {
        this.Advertisements = data;
        this.LoadingService.hide();
      }
      })
    }else{
      console.log("You've cancel the deletion");
      
    }
  }
    getDaysAgo(createdDate: string): string {
    const created = new Date(createdDate);
    const now = new Date();
    const diffTime = Math.abs(now.getTime() - created.getTime());
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    
    if (diffDays === 1) return '1 day ago';
    if (diffDays < 7) return `${diffDays} days ago`;
    if (diffDays < 30) return `${Math.floor(diffDays / 7)} weeks ago`;
    return `${Math.floor(diffDays / 30)} months ago`;
  }
}
