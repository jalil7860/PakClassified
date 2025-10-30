import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../Core/services/AuthenticationServices/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AdvertisementService } from '../../Core/services/PakClassified/advertisement.service';
import { AdvertisementSubCategoryService } from '../../Core/services/PakClassified/AdvertismentSubCategory.service';
import { AdvertisementStatusService } from '../../Core/services/PakClassified/AdvertisementStatus.service';
import { AdvertisementTypeService } from '../../Core/services/PakClassified/AdvertisementType.service';
import { CityAreaService } from '../../Core/services/LoactionServices/city-area.service';
import { NotificationService } from '../../Core/Common/Notification/notification.service';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { AdvertisementModel } from '../../Core/Model/Advertisment/Advertisement.model';
import { AdvertisementSubCategory } from '../../Core/Model/Advertisment/AdvertisementSubCategory.model';
import { AdvertisementStatus } from '../../Core/Model/Advertisment/AdvertisementStatus.model';
import { CityArea } from '../../Core/Model/location/cityArea.model';
import { User } from '../../Core/Model/User/User.model';
import { AdvertisementType } from '../../Core/Model/Advertisment/AdvertisementType.model';
import { AdvertisementImage } from '../../Core/Model/Advertisment/AdvertisementImage.model';
import { UserService } from '../../Core/services/UserServices/User.service';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';


export function dateStartDateRangeValidity(control: AbstractControl): ValidationErrors | null {
  const startValue = control.value;
  if (!startValue) return null;

  const startDate = new Date(startValue);
  const today = new Date();
  today.setHours(0, 0, 0, 0);

  if (startDate < today) {
    return { pastDate: true }; // Consistent error key
  }

  return null;
}

export function dateEndDateRangeValidity(control: AbstractControl): ValidationErrors | null {
  const endValue = control.value;
  if (!endValue) return null;

  const endDate = new Date(endValue);
  const today = new Date();
  today.setHours(0, 0, 0, 0);

  if (endDate <= today) {
    return { pastDate: true }; // Consistent error key
  }

  return null;
}


@Component({
  selector: 'app-post-advertisement',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './post-advertisement.component.html',
  styleUrl: './post-advertisement.component.css'
})
export class PostAdvertisementComponent implements OnInit{
  authService = inject(AuthService);
  snackBar = inject(MatSnackBar);
  router = inject(Router);

  notificationService = inject(NotificationService);
  loadingService = inject(LoadingService);

  advertisementService = inject(AdvertisementService);
  subCategoryService = inject(AdvertisementSubCategoryService);
  statusService = inject(AdvertisementStatusService);
  typeService = inject(AdvertisementTypeService);
  cityAreaService = inject(CityAreaService);
  userService = inject(UserService);

  Advertisements: AdvertisementModel[] = [];
  SubCategories: AdvertisementSubCategory[] = [];
  Statuses: AdvertisementStatus[] = [];
  CityAreas: CityArea[] = [];
  Users: User[] = [];
  Types: AdvertisementType[] = [];
  Images: AdvertisementImage[] = [];

  PostAdvertisementForm = new FormGroup({
    subCategoryId: new FormControl('', [Validators.required]),
    statusId: new FormControl('', [Validators.required]),
    typeId: new FormControl('', [Validators.required]),
    postedById: new FormControl(''),
    cityAreaId: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required, Validators.maxLength(60), Validators.nullValidator]),
    price: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required, Validators.nullValidator]),
    features: new FormControl('', [Validators.required, Validators.nullValidator]),
    hits: new FormControl('',),
    endsOn: new FormControl('', [Validators.required, Validators.nullValidator, dateEndDateRangeValidity]),
    startsOn: new FormControl('', [Validators.required, Validators.nullValidator, dateStartDateRangeValidity]),
    // createdDate: new FormControl(''),
    image: new FormControl('')

  });

  get subCategoryId_post(){
    return this.PostAdvertisementForm.get('subCategoryId')
  }
  get statusId_post(){
    return this.PostAdvertisementForm.get('statusId')
  }
  get typeId_post(){
    return this.PostAdvertisementForm.get('typeId')
  }
  get postedById_post(){
    return this.PostAdvertisementForm.get('postedById')
  }
  get cityAreaId_post(){
    return this.PostAdvertisementForm.get('cityAreaId')
  }
  get name_post(){
    return this.PostAdvertisementForm.get('name')
  }
  get price_post(){
    return this.PostAdvertisementForm.get('price')
  }
  get description_post(){
    return this.PostAdvertisementForm.get('description')
  }
  get features_post(){
    return this.PostAdvertisementForm.get('features')
  }
  get endsOn_post(){
    return this.PostAdvertisementForm.get('endsOn')
  }
  get startsOn_post(){
    return this.PostAdvertisementForm.get('startsOn')
  }
  get image_post(){
    return this.PostAdvertisementForm.get('image')
  }

  ngOnInit(): void {
    this.subCategoryService.getAll().subscribe({
      next: (data: AdvertisementSubCategory[]) => {
        this.SubCategories = data;
      },
      error: (err) => {
        console.log("Error on loading subcategories: ", err); 
      }
    });

    this.statusService.getAll().subscribe({
      next: (data: AdvertisementStatus[]) => {
        this.Statuses = data;
      },
      error: (err) => {
        console.log("Error on loading Status: ", err); 
      }
    });

    this.cityAreaService.getAll().subscribe({
      next: (data: CityArea[]) => {
        this.CityAreas = data;
      },
      error: (err) => {
        console.log("Error on loading cityAreas: ", err); 
      }
    });

    this.typeService.getAll().subscribe({
      next: (data: AdvertisementType[]) => {
        this.Types = data;
      },
      error: (err) => {
        console.log("Error on loading Types: ", err); 
      }
    });
  }

  selectedImage: { url: string, file: File, name: string } | null = null;

  // File selection handler - sirf ek file
  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.processFile(file);
      const reader = new FileReader();
      reader.onload = () => {
        const base64String = reader.result as string;
        this.PostAdvertisementForm.get('image')?.setValue(base64String);
      };
      reader.readAsDataURL(file);
    }
  }


  // Process selected file
  private processFile(file: File): void {
    if (this.validateFile(file)) {
      this.createImagePreview(file);
    }
  }

  // Validate file type and size
  private validateFile(file: File): boolean {
    const validTypes = ['image/jpeg', 'image/jpg', 'image/png'];
    const maxSize = 5 * 1024 * 1024; // 5MB
    
    if (!validTypes.includes(file.type)) {
      alert('Please select only JPG, JPEG or PNG files');
      return false;
    }
    
    if (file.size > maxSize) {
      alert('File size should be less than 5MB');
      return false;
    }
    
    return true;
  }

  // Create image preview
  private createImagePreview(file: File): void {
    const reader = new FileReader();
    
    reader.onload = (e: any) => {
      this.selectedImage = {
        url: e.target.result,
        file: file,
        name: file.name
      };
    };
    
    reader.readAsDataURL(file);
  }

  // Remove image from preview
  removeImage(): void {
    this.selectedImage = null;
  }

  // Get final file for form submission
  getSelectedFile(): File | null {
    return this.selectedImage ? this.selectedImage.file : null;
  }

  // Form submission
  postSubmit(): void {
    this.loadingService.show();
    const file = this.getSelectedFile();
    console.log('Selected file:', file);

    console.log("Form Values: ", this.PostAdvertisementForm.getRawValue());

    if(!this.PostAdvertisementForm.valid) {
      this.loadingService.hide();
      this.notificationService.showError('Please fill all required fields correctly.', 'Incorrect !')
      return;
    }

    if(this.PostAdvertisementForm.valid){
      const rawData = this.PostAdvertisementForm.getRawValue();

      const selectedSubCategoryId = Number(rawData.subCategoryId);
      const selectedStatusId = Number(rawData.statusId);
      const selectedCityAreaId = Number(rawData.cityAreaId);
      const selectedTypeId = Number(rawData.typeId);

      const selectedSubCate = this.SubCategories.find(subcategory => subcategory.id === selectedSubCategoryId);
      if (!selectedSubCate) {
        this.snackBar.open("Please select a valid SubCategory", "", {
          duration: 5000,
          panelClass: ['error-snackbar']
        });
        return;
      }

      const selectedStatus = this.Statuses.find(status => status.id === selectedStatusId);
      if (!selectedStatus) {
        this.snackBar.open("Please select a valid Status", "", {
          duration: 5000,
          panelClass: ['error-snackbar']
        });
        return;
      }

      const selectedCa = this.CityAreas.find(cityarea => cityarea.id === selectedCityAreaId);
      if (!selectedCa) {
        this.snackBar.open("Please select a valid CityArea", "", {
          duration: 5000,
          panelClass: ['error-snackbar']
        });
        return;
      }

      const selectedType = this.Types.find(type => type.id === selectedTypeId);

      if (!selectedType) {
        this.snackBar.open("Please select a valid Type", "", {
          duration: 5000,
          panelClass: ['error-snackbar']
        });
        return;
      }

      const advertisement: AdvertisementModel = {
        id: 0,
        subCategoryId: selectedSubCategoryId,
        statusId: selectedStatusId,
        typeId: selectedTypeId,
        postedById: this.authService.getUserFromStorage()?.id ?? 0,
        cityAreaId: selectedCityAreaId,
        name: rawData.name || '',
        price: rawData.price ? Number(rawData.price) : 0,
        description: rawData.description || '',
        features: rawData.features || '', 
        hits: rawData.hits ? Number(rawData.hits) : 0,
        endsOn: rawData.endsOn ? new Date(rawData.endsOn).toISOString() : new Date().toISOString(),
        startsOn: rawData.startsOn ? new Date(rawData.startsOn).toISOString() : new Date().toISOString(),
        createdDate: new Date().toISOString(),
        image: rawData.image || ''
      }

      console.log("Advertisement Object", advertisement);

      this.advertisementService.create(advertisement).subscribe({
        next: (data: any) => {
          console.log("Saved: ", data);
          this.loadingService.hide();
          this.PostAdvertisementForm.reset();
          this.notificationService.showSuccess("Post Created Successfully");
        }
      });
    }else{
      this.loadingService.hide();
      this.notificationService.showError("Please fill the form correctly!", 'Something Went Wrong');
    }
  }
}