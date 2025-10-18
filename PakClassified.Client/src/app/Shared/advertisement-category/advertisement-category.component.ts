import { Component, inject, OnInit } from '@angular/core';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { AdvertisementService } from '../../Core/services/PakClassified/advertisement.service';
import { AdvertisementSubCategoryService } from '../../Core/services/PakClassified/AdvertismentSubCategory.service';
import { AdvertisementSubCategory } from '../../Core/Model/Advertisment/AdvertisementSubCategory.model';
import { AdvertisementCategoryService } from '../../Core/services/PakClassified/AdvertisementCategory.service';
import { AdvertisementCategory } from '../../Core/Model/Advertisment/AdvertisementCategory.model';
import { RouterLink, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-advertisement-category',
  imports: [RouterLink],
  templateUrl: './advertisement-category.component.html',
  styleUrl: './advertisement-category.component.css',
})
export class AdvertisementCategoryComponent implements OnInit{
  LoadingService = inject(LoadingService);
  route = inject(ActivatedRoute);
  categoryService = inject(AdvertisementCategoryService);
  advertisementSubCategoryService = inject(AdvertisementSubCategoryService);
  category: AdvertisementCategory[] = [];
  SubCategory: AdvertisementSubCategory[] = [];

  ngOnInit(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
    this.LoadingService.show();
    this.categoryService.getAll().subscribe({
      next: (ca: AdvertisementCategory[]) => {
        this.category = ca;

        this.route.paramMap.subscribe((params) => {
          const id = params.get('id');
          console.log('Got category from route:', id);

          if (id) {
            this.loadSubCateDetail(+id);
          }

          this.LoadingService.hide();
        });
      },
      error: (err) => {
        this.LoadingService.hide();
        console.error('cate data load error:', err);
      },
    });
  }
  private loadSubCateDetail(categoryId: number): void {
    this.advertisementSubCategoryService.GetByCategoryId(categoryId).subscribe({
      next: (scList: AdvertisementSubCategory[]) => {
        this.SubCategory = scList;
        console.log('Loading subcategories for categoryId:', categoryId);

        this.SubCategory = scList.map((sc) => {
          const advertisementCategory = this.category.find(c => c.id === sc.categoryId);
          const advertisementSubCategory =
            this.SubCategory.find((c) => c.id === sc.categoryId) || null;
          return {
            id: sc.id,
            name: sc.name,
            description: sc.description,
            image: sc.image,
            category:advertisementCategory,
            // createdDate: sc.createdDate,
            categoryId: sc.categoryId,
            // categoryName: sc.categoryName,
          } as AdvertisementSubCategory;
        });
      },
      error: (err) => {
        console.error('subcategories list fetching error:', err);
      },
    });
  }
}
