import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../Core/services/AuthenticationServices/auth.service';
import { AdvertisementService } from '../../Core/services/PakClassified/advertisement.service';
import { CityAreaService } from '../../Core/services/LoactionServices/city-area.service';
import { AdvertisementStatusService } from '../../Core/services/PakClassified/AdvertisementStatus.service'; // ✅ NEW SERVICE
import { AdvertisementModel } from '../../Core/Model/Advertisment/Advertisement.model';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { NotificationService } from '../../Core/Common/Notification/notification.service';

@Component({
  selector: 'app-user-dashboard',
  imports: [DatePipe, RouterLink],
  templateUrl: './user-dashboard.component.html',
  styleUrl: './user-dashboard.component.css'
})
export class UserDashboardComponent implements OnInit{
  loadingService = inject(LoadingService)
  authService = inject(AuthService);
  advertisementService = inject(AdvertisementService)
  notificationService = inject(NotificationService);
  cityAreaService = inject(CityAreaService)
  statusService = inject(AdvertisementStatusService)
  
  advertisements: AdvertisementModel[] = []
  cityAreas: any[] = []
  advertisementStatuses: any[] = []

  ngOnInit(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
    const userId = this.authService.getUserFromStorage()?.id;
    
    this.loadingService.show();
    
    this.cityAreaService.getAll().subscribe({
      next: (allCityAreas) => {
        this.cityAreas = allCityAreas;
        
        this.statusService.getAll().subscribe({
          next: (allStatuses) => {
            this.advertisementStatuses = allStatuses;
            
            this.advertisementService.getPostByUserId(userId).subscribe({
              next: (ads: AdvertisementModel[]) => {
                
                this.advertisements = ads.map(ad => {
                  const cityAreaObj = this.cityAreas.find(ca => ca.id === ad.cityAreaId);
                  
                  const statusObj = this.advertisementStatuses.find(s => s.id === ad.statusId);
                  
                  // Return updated advertisement with all objects
                  return {
                    ...ad,                    // Original data
                    cityArea: cityAreaObj,    // Manually attached cityArea
                    status: statusObj         // Manually attached status
                  };
                }).sort((a, b) => b.id - a.id);
                
                console.log("Final Advertisements with Status:", this.advertisements);
                this.loadingService.hide();
              },
              error: (err) => {
                console.log("Error loading ads: ", err);
                this.loadingService.hide();
              }
            });
          },
          error: (err) => {
            console.log("Error loading statuses: ", err);
            this.loadingService.hide();
          }
        });
      },
      error: (err) => {
        console.log("Error loading city areas: ", err);
        this.loadingService.hide();
      }
    });
  }

  formatPrice(price: number | undefined): string {
    if (!price) return '0';
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  }

  getStatusBadgeClass(statusName: string | undefined): string {
    switch(statusName?.toLowerCase()) {
      case 'active': return 'status-badge-active';
      case 'pending': return 'status-badge-pending'; 
      case 'sold': return 'status-badge-sold';
      case 'expired': return 'status-badge-expired';
      default: return 'status-badge-default';
    }
  }

deleteAdvertisement(adId: number) {
  if(confirm('Are you sure you want to delete this advertisement?')) {
    this.loadingService.show();
    this.advertisementService.delete(adId).subscribe({
      next: () => {
        this.advertisements = this.advertisements.filter(ad => ad.id !== adId);
        this.loadingService.hide();
        this.notificationService.showSuccess('Advertisement deleted successfully');
      },
      error: (err) => {
        console.log('Delete error:', err);
        this.loadingService.hide();
        this.notificationService.showError('Error deleting advertisement');
      }
    });
  }
}
}