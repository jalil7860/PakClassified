import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AdvertisementCategoryService } from '../../Core/services/PakClassified/AdvertisementCategory.service';
import { AdvertisementCategory } from '../../Core/Model/Advertisment/AdvertisementCategory.model';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { AdvertisementSubCategoryService } from '../../Core/services/PakClassified/AdvertismentSubCategory.service';
import { AdvertisementSubCategory } from '../../Core/Model/Advertisment/AdvertisementSubCategory.model';
import { forkJoin } from 'rxjs';




@Component({
  selector: 'app-categories',
  imports: [RouterLink],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})
export class CategoriesComponent implements OnInit {
  loadingservice = inject(LoadingService)

  subcategoryservice = inject(AdvertisementSubCategoryService)

  categoryservice = inject(AdvertisementCategoryService);
  subcategory : AdvertisementSubCategory[] = [];

  SubcategoryCounts : {[key : number]: AdvertisementSubCategory[]} = {};

  Category: AdvertisementCategory[] = [];
  
  // ngOnInit(): void {
  //   this.loadingservice.show();
  //   this.categoryservice.getAll().subscribe({
  //     next:(data:any[]) => {
  //       console.log("Category Service: ", data);
  //       this.loadingservice.hide();
  //       this.Category = data;
  //     },
  //     error:(err) => {
  //       this.loadingservice.hide();
  //       console.log("error: ", err)
  //     }
  //   });
  //    this.subcategoryservice.getAll().subscribe({
  //     next: (data) => {
  //       console.log("Advertisement Sub Categories", data)
  //       this.subcategory = data;
 
  //       this.SubcategoryCounts = this.subcategory.reduce((acc, sub) => {
  //         if (!acc[sub.CategoryId]) acc[sub.CategoryId] = [];
  //         acc[sub.CategoryId].push(sub);
  //         return acc;
  //       }, {} as { [key: number]: AdvertisementSubCategory[] });
 
  //       this.loadingservice.hide();
  //     },
  //     error: (err) => {
  //       console.log('Error Fetching Advertisement Sub Category', err);
  //       this.loadingservice.hide();
  //     }
  //   });

  // }
  ngOnInit(): void {
  this.loadingservice.show();

  forkJoin({
    categories: this.categoryservice.getAll(),
    subcategories: this.subcategoryservice.getAll()
  }).subscribe({
    next: ({ categories, subcategories }) => {
      console.log("Categories: ", categories);
      console.log("Subcategories: ", subcategories);

      this.Category = categories;
      this.subcategory = subcategories;

      // Map counts per category
      this.SubcategoryCounts = this.subcategory.reduce((acc, sub) => {
        if (!acc[sub.categoryId]) acc[sub.categoryId] = [];
        acc[sub.categoryId].push(sub);
        return acc;
      }, {} as { [key: number]: AdvertisementSubCategory[] });

      this.loadingservice.hide();
      console.log("Count",this.SubcategoryCounts);
      
    },
    error: (err) => {
      console.log("Error loading data: ", err);
      this.loadingservice.hide();
    }
  });
}
}
