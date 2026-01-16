import { Component, inject, OnInit } from '@angular/core';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { NotificationService } from '../../Core/Common/Notification/notification.service';
import { AdvertisementCategoryService } from '../../Core/services/PakClassified/AdvertisementCategory.service';
import { AdvertisementSubCategoryService } from '../../Core/services/PakClassified/AdvertismentSubCategory.service';
import { AdvertisementTypeService } from '../../Core/services/PakClassified/AdvertisementType.service';
import { AdvertisementStatusService } from '../../Core/services/PakClassified/AdvertisementStatus.service';
import { CityAreaService } from '../../Core/services/LoactionServices/city-area.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../Core/services/AuthenticationServices/auth.service';
import { AdvertisementCategory } from '../../Core/Model/Advertisment/AdvertisementCategory.model';
import { AdvertisementSubCategory } from '../../Core/Model/Advertisment/AdvertisementSubCategory.model';
import { Router } from '@angular/router';
import { AdvertisementService } from '../../Core/services/PakClassified/advertisement.service';

@Component({
  selector: 'app-admin-dashboard',
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css'
})
export class AdminDashboardComponent implements OnInit{
  loadingservice = inject(LoadingService);
  notificationService = inject(NotificationService);
  advCategoryService = inject(AdvertisementCategoryService);
  advSubCategoryService = inject(AdvertisementSubCategoryService);
  advService = inject(AdvertisementService);
  authService = inject(AuthService);
  router = inject(Router);

  Category : AdvertisementCategory[] = [];
  SubCategory: AdvertisementSubCategory[] = [];

  totalCategories: number = 0;
  totalSubCategories: number = 0;
  activeListings: number = 0;

  CategoryForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    image: new FormControl('', [Validators.required])
  });
  get name_category(){
    return this.CategoryForm.get('name')
  }
  get description_category(){
    return this.CategoryForm.get('description')
  }get image_category(){
    return this.CategoryForm.get('image')
  }

  SubCategoryForm = new FormGroup({
    name: new FormControl('',[Validators.required]),
    description: new FormControl('',[Validators.required]),
    image: new FormControl('',[Validators.required]),
    categoryId: new FormControl('', [Validators.required])
  })
  get name_subcategory(){
    return this.SubCategoryForm.get('name')
  }
  get description_subcategory(){
    return this.SubCategoryForm.get('description')
  }
  get image_subcategory(){
    return this.SubCategoryForm.get('image')
  }
  get categoryId_subcategory(){
    return this.SubCategoryForm.get('categoryId')
  }

  onFileSelected(event: any, formType: string){
    const file = event.target.files[0];
    if(file){
      const reader = new FileReader();
      reader.onload = ()=>{
        const base64String = reader.result as string;
        if(formType === 'category'){
          this.CategoryForm.get('image')?.setValue(base64String);
        }else{
          this.SubCategoryForm.get('image')?.setValue(base64String)
        }
      };
      reader.readAsDataURL(file);
    }
  }

  ngOnInit(): void {
    this.loadingservice.show();
    this.advCategoryService.getAll().subscribe({
      next: (data) => {
        console.log("Categories: ", data);
        this.Category = data.map((ur: any) => ({
          id: ur.id,
          name: ur.name,
          description: ur.description,
          image:ur.image
        }))
        this.totalCategories = data.length;
        this.loadingservice.hide();
      },error: (err) => {
        console.log("error: ", err);
        this.loadingservice.hide();
      }
    });
    this.advService.getAll().subscribe({
      next: (data) => {
        this.activeListings = data.length;
      },error: (err) => {
          console.log("Error Fetching Advertisement", err);
          
      }
    })
    this.advSubCategoryService.getAll().subscribe({
    next: (data) => {
      console.log("SubCategories: ", data);
      this.totalSubCategories = data.length; 
    },
    error: (err) => {
      console.log("error fetching subcategories: ", err);
    }
  })
  }


  SubCategorySubmit(){
    this.loadingservice.show();
    console.log("Form Value: ", this.SubCategoryForm.getRawValue());

    if(!this.SubCategoryForm.valid){
      this.loadingservice.hide();
      this.notificationService.showError("Please fill all required fields Correctly.", "Incorrect!");
      return;
    }
    if(this.SubCategoryForm.valid){
      const rawData = this.SubCategoryForm.getRawValue();
      const selectedCategoryId = Number(rawData.categoryId);
  

      const selectedCategory = this.Category.find(cate => cate.id === selectedCategoryId);
      
      const SubCategory = {
        id: 0,
        name: rawData.name || '',
        description: rawData.description || '',
        categoryId: selectedCategoryId,
        category: selectedCategory,
        image: rawData.image || ''
      };
      console.log("SubCategory object with both CategoryId and Category: ", SubCategory);
      
      this.advSubCategoryService.create(SubCategory).subscribe({
        next: (data) => {
          console.log("Saved: ", data);
          this.refreshCategories();
          this.loadingservice.hide();
          this.notificationService.showSuccess("Sub Category Created Successfully");
          this.SubCategoryForm.reset();
          this.refreshSubCategoriesCount();

        },error: (err) => {
          this.loadingservice.hide();
          console.log("Sign Up Error: ", err);
          
        }
      })
    }
    
  }

  CategorySubmit(){
    this.loadingservice.show();
    console.log("Form Value: ", this.CategoryForm.getRawValue());

    if(!this.CategoryForm.valid){
      this.loadingservice.hide();
      this.notificationService.showError("Please fill all required fields Correctly.", 'Incorrect !');
      return;
    }
      if(this.CategoryForm.valid){
      const rawData = this.CategoryForm.getRawValue();
      
      const Category = {
        id: 0,
        name: rawData.name || '',
        description: rawData.description || '',
        image: rawData.image || ''
      };

      this.advCategoryService.create(Category).subscribe({
        next: (data) => {
          console.log("Saved: ", data);
          this.loadingservice.hide();
          this.notificationService.showSuccess("Category Created Successfully");
          this.CategoryForm.reset();
        },error: (err) => {
          this.loadingservice.hide();
          console.log("Error: ", err);
        }
      })
    }
}


refreshCategories() {
  this.loadingservice.show();
  this.advCategoryService.getAll().subscribe({
    next: (data) => {
      console.log("Refreshed Categories: ", data);
      this.Category = data.map((ur: any) => ({
        id: ur.id,
        name: ur.name,
        description: ur.description,
        image: ur.image
      }))
      this.totalCategories = data.length;
      this.loadingservice.hide();
    },
    error: (err) => {
      console.log("error: ", err);
      this.loadingservice.hide();
    }
  })
}
refreshSubCategoriesCount() {
  this.advSubCategoryService.getAll().subscribe({
    next: (data) => {
      this.totalSubCategories = data.length;
    },
    error: (err) => {
      console.log("error fetching subcategories count: ", err);
    }
  })
}
}