import { Component, inject, OnInit } from '@angular/core';
import { LoadingService } from '../Core/Common/Loading/loading.service';
import { NotificationService } from '../Core/Common/Notification/notification.service';
import { AdvertisementService } from '../Core/services/PakClassified/advertisement.service';
import { AdvertisementCategoryService } from '../Core/services/PakClassified/AdvertisementCategory.service';
import { AdvertisementSubCategoryService } from '../Core/services/PakClassified/AdvertismentSubCategory.service';
import { AdvertisementCategory } from '../Core/Model/Advertisment/AdvertisementCategory.model';
import { AdvertisementSubCategory } from '../Core/Model/Advertisment/AdvertisementSubCategory.model';
import { AdvertisementModel } from '../Core/Model/Advertisment/Advertisement.model';

@Component({
  selector: 'app-adv-sub-category-cmp',
  imports: [],
  templateUrl: './adv-sub-category-cmp.component.html',
  styleUrl: './adv-sub-category-cmp.component.css'
})
export class AdvSubCategoryCMPComponent implements OnInit{
 loadingServie = inject(LoadingService);
 notificationService = inject(NotificationService);
 subCateService = inject(AdvertisementSubCategoryService)
 advService = inject(AdvertisementService);
 advCateService = inject(AdvertisementCategoryService);
 advSubCate = inject(AdvertisementSubCategoryService);

 AllAdv: AdvertisementModel[] = [];
 SubCategories: AdvertisementSubCategory[] = [];
 ngOnInit(): void {
   this.loadingServie.show();
   this.subCateService.getAll().subscribe({
    next:(data) => {
      this.SubCategories = data;
      console.log("Fetched: ", data);
      this.loadingServie.hide();
      this.notificationService.showNotification("Success","Fetched Successfully.")
    },error: (err) => {
      console.log("Did'nt Fetched data");
      this.loadingServie.hide();
      this.notificationService.showError("Error","Error While Fetching.")
    }
   });
   this.loadingServie.show();
   this.notificationService.showNotification("Fetching", "Fetching your data.")
   this.advService.getAll().subscribe({
    next: (data) => {
      this.AllAdv = data
    }
   })
   
 }
};