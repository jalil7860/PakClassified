import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { AdvertisementService } from '../../Core/services/PakClassified/advertisement.service';
import { AdvertisementModel } from '../../Core/Model/Advertisment/Advertisement.model';
import { CityArea } from '../../Core/Model/location/cityArea.model';
import { Province } from '../../Core/Model/location/province.model';
import { Country } from '../../Core/Model/location/country.model';
import { City } from '../../Core/Model/location/city.model';
import { CityAreaService } from '../../Core/services/LoactionServices/city-area.service';
import { CityService } from '../../Core/services/LoactionServices/city.service';
import { ProvinceService } from '../../Core/services/LoactionServices/province.service';
import { CountryService } from '../../Core/services/LoactionServices/country.service';
import { CurrencyPipe, DatePipe, DecimalPipe } from '@angular/common';
import { UserService } from '../../Core/services/UserServices/User.service';
import { User } from '../../Core/Model/User/User.model';

@Component({
  selector: 'app-car-details',
  imports: [CurrencyPipe, DatePipe, DecimalPipe],
  templateUrl: './car-details.component.html',
  styleUrl: './car-details.component.css'
})
export class CarDetailsComponent implements OnInit{
  postdetail!: AdvertisementModel | null;
  CityArea : CityArea[] = [];
  User : User[] = [];
  province : Province[] = [];
  country : Country[] = [];
  city: City[] = [];

  constructor(
   private route: ActivatedRoute,
    private advService: AdvertisementService,
    private cityAreaService: CityAreaService,
    private userService: UserService,
    private cityService: CityService,
    private provinceService: ProvinceService,
    private countryService: CountryService,
    private loadingService: LoadingService
  ){}
  formatPrice(price: number | undefined): string {
  if (!price) return '0';
  return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
  ngOnInit(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
     this.loadingService.show();

  // Pehle CityArea aur User dono fetch karo
    this.cityAreaService.getAll().subscribe({
    next: (ca: CityArea[]) => {
      this.CityArea = ca;
      
      // Ab User data fetch karo
      this.userService.getAll().subscribe({
        next: (users: User[]) => {
          this.User = users;
          
          // Jab dono data aa jaye tab post detail load karo
          const id = this.route.snapshot.paramMap.get('id');
          if (id) {
            this.loadPostDetail(+id);
          }
          this.loadingService.hide();
        },
        error: err => {
          console.error('User load error:', err);
          this.loadingService.hide();
        }
      });
    },
    error: err => {
      console.error('CityArea load error:', err);
      this.loadingService.hide();
    }
  });
}

   private loadPostDetail(id: number): void {
    this.advService.getById(id).subscribe({
      next: (ad: AdvertisementModel) => {
        const cityArea = this.CityArea.find(c => c.id === ad.cityAreaId) || null;
        const postedByUser = this.User.find(u => u.id === ad.postedById) || null;
        

        this.postdetail = {
                id: ad.id,
                name: ad.name,
                description: ad.description,
                features: ad.features,
                price: ad.price,
                cityArea: cityArea,
                postedById: ad.postedById,
                postedBy: postedByUser,
                status: ad.status,
                subCategory: ad.subCategory,
                type: ad.type,
                createdDate: ad.createdDate,
                startsOn : ad.startsOn,
                endsOn : ad.endsOn,
                image: ad.image

        } as AdvertisementModel;
         // Component mein yeh function add karo
      },
      error: err => {
        console.error('Post Details load error:', err); 
      }
    });
  }
  getFeaturesList(): string[] {
  if (!this.postdetail?.features) return [];
  return this.postdetail.features.split(',').map(feature => feature.trim());
} 
getWhatsAppLink(): string {
    const phoneNumber = this.postdetail?.postedBy?.contactNumber;
    if (!phoneNumber) return '#';
    
    // Remove any spaces, dashes, or special characters
    const cleanNumber = phoneNumber.replace(/[\s\-\(\)]/g, '');
    
    // Add country code if not present (Pakistan = 92)
    const fullNumber = cleanNumber.startsWith('+') ? cleanNumber : `+92${cleanNumber.replace(/^0/, '')}`;
    
    // Create WhatsApp message
    const message = encodeURIComponent(`Hi ${this.postdetail?.postedBy?.name  }, I'm interested in your ${this.postdetail?.name} listed for ${this.formatPrice(this.postdetail?.price)} PKR`);
    
    return `https://wa.me/${fullNumber.replace(/\+/g, '')}?text=${message}`;
  }
}
