import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { NotificationService } from '../../Core/Common/Notification/notification.service';
import { AdvertisementSubCategoryService } from '../../Core/services/PakClassified/AdvertismentSubCategory.service';
import { AdvertisementCategoryService } from '../../Core/services/PakClassified/AdvertisementCategory.service';
import { AdvertisementService } from '../../Core/services/PakClassified/advertisement.service';
import { AdvertisementSubCategory } from '../../Core/Model/Advertisment/AdvertisementSubCategory.model';
import { AdvertisementCategory } from '../../Core/Model/Advertisment/AdvertisementCategory.model';
import { AdvertisementModel } from '../../Core/Model/Advertisment/Advertisement.model';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-subcategory-detail',
  imports: [CommonModule, RouterLink],
  templateUrl: './sub-category-details.component.html',
  styleUrl: './sub-category-details.component.css'
})
export class SubcategoryDetailComponent implements OnInit {
  loadingService = inject(LoadingService);
  notificationService = inject(NotificationService);
  subCategoryService = inject(AdvertisementSubCategoryService);
  categoryService = inject(AdvertisementCategoryService);
  route = inject(ActivatedRoute)
  advService = inject(AdvertisementService);

  subCateDetail!: AdvertisementSubCategory | null;
  subCategory: AdvertisementSubCategory [] = [];
  Category: AdvertisementCategory [] = [];
  Advertisement: AdvertisementModel [] =[]
  advCount = 0;
  categoryName = ''
  categoryId = 0
  
  ngOnInit(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
    this.loadingService.show();
    const SubName = this.route.snapshot.paramMap.get('subCategoryName');
    const id: any = this.route.snapshot.paramMap.get('subCategoryId');
    console.log("SubCategoryId: ", id);
    

    this.categoryService.getAll().subscribe({
      next: (cate: any) => {
        console.log("Category: ", cate);
        this.Category = cate;

        this.advService.getAll().subscribe({
          next: (adv: any) => {
            console.log("Advertisement: ", adv);
            this.Advertisement = adv
            
            const id = this.route.snapshot.paramMap.get('subCategoryId');
            console.log("Subcategory Id: ", id);
            if (id) {
            this.loadSubCategoryDetails(+id);
          }
          this.loadingService.hide();
          },error: (err) => {
            console.log("Error Loading ADV: ", err);
            this.loadingService.hide();
          }
        });
        
      },error: (err) => {
        console.log("Error While Loadin SUbcategory: ", err);
        this.loadingService.hide();
      }
    });
    // this.loadSubCategoryDetails(+id)

    // this.subCategoryService.getById(id).subscribe({
    //   next: (SubCate: any) => {
    //     console.log(`SubCategory With ID: ${id}`, SubCate);
    //     this.subCategory = SubCate;
    //     this.loadingService.hide();
    //   },error : (err) => {
    //     console.log("Error: ", err);
    //   }
    // })
  }

  private loadSubCategoryDetails(id: number): void{
    this.subCategoryService.getById(id).subscribe({
      next: (subCate: AdvertisementSubCategory) => {
        const category: any = this.Category.find( c => c.id === subCate.categoryId)
        console.log("Catgeory Id: ", category?.name);
        this.advCount = this.Advertisement.filter(a => a.subCategoryId === subCate.id).length
        console.log("Advertisement Count: ", this.advCount);
        
        this.subCateDetail = {
          id: subCate.id,
          name: subCate.name,
          description: subCate.description,
          image: subCate.image,
          category: category,
          categoryId: category?.id
        } as AdvertisementSubCategory
      },error: (err) => {
        console.log("Error while fetching subCategory Details: ", err);
        
      }
    })
  }   
  
}