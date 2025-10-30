import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AdvertisementService } from '../../Core/services/PakClassified/advertisement.service';
import { AdvertisementSubCategoryService } from '../../Core/services/PakClassified/AdvertismentSubCategory.service';
import { AdvertisementStatusService } from '../../Core/services/PakClassified/AdvertisementStatus.service';
import { AdvertisementTypeService } from '../../Core/services/PakClassified/AdvertisementType.service';
import { CityAreaService } from '../../Core/services/LoactionServices/city-area.service';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { NotificationService } from '../../Core/Common/Notification/notification.service';
import { AuthService } from '../../Core/services/AuthenticationServices/auth.service';

import { AdvertisementModel } from '../../Core/Model/Advertisment/Advertisement.model';
import { AdvertisementSubCategory } from '../../Core/Model/Advertisment/AdvertisementSubCategory.model';
import { AdvertisementStatus } from '../../Core/Model/Advertisment/AdvertisementStatus.model';
import { CityArea } from '../../Core/Model/location/cityArea.model';
import { AdvertisementType } from '../../Core/Model/Advertisment/AdvertisementType.model';

@Component({
  selector: 'app-ad-edit',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './ad-edit.component.html',
  styleUrl: './ad-edit.component.css'
})
export class AdEditComponent implements OnInit {
  private route = inject(ActivatedRoute);
  router = inject(Router);
  private adService = inject(AdvertisementService);
  private subCategoryService = inject(AdvertisementSubCategoryService);
  private statusService = inject(AdvertisementStatusService);
  private typeService = inject(AdvertisementTypeService);
  private cityAreaService = inject(CityAreaService);
  private loadingService = inject(LoadingService);
  private notificationService = inject(NotificationService);
  private authService = inject(AuthService);

  adId!: number;
  currentAd!: AdvertisementModel;

  // Dropdown data
  SubCategories: AdvertisementSubCategory[] = [];
  Statuses: AdvertisementStatus[] = [];
  CityAreas: CityArea[] = [];
  Types: AdvertisementType[] = [];

  // Form
  EditAdvertisementForm = new FormGroup({
    subCategoryId: new FormControl('', [Validators.required]),
    statusId: new FormControl('', [Validators.required]),
    typeId: new FormControl('', [Validators.required]),
    cityAreaId: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required]),
    price: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    features: new FormControl('', [Validators.required]),
    image: new FormControl('')
  });

  // Form control getters
  get subCategoryId_edit() { return this.EditAdvertisementForm.get('subCategoryId'); }
  get statusId_edit() { return this.EditAdvertisementForm.get('statusId'); }
  get typeId_edit() { return this.EditAdvertisementForm.get('typeId'); }
  get cityAreaId_edit() { return this.EditAdvertisementForm.get('cityAreaId'); }
  get name_edit() { return this.EditAdvertisementForm.get('name'); }
  get price_edit() { return this.EditAdvertisementForm.get('price'); }
  get description_edit() { return this.EditAdvertisementForm.get('description'); }
  get features_edit() { return this.EditAdvertisementForm.get('features'); }
  get image_edit() { return this.EditAdvertisementForm.get('image'); }

  selectedImage: { url: string, file: File, name: string } | null = null;

  ngOnInit(): void {
    this.adId = +this.route.snapshot.params['id'];
    this.loadDropdownData();
    this.loadAdData();
  }

  loadDropdownData(): void {
    this.subCategoryService.getAll().subscribe({
      next: (data: AdvertisementSubCategory[]) => {
        this.SubCategories = data;
      },
      error: (err) => {
        console.log("Error loading subcategories: ", err);
      }
    });

    this.statusService.getAll().subscribe({
      next: (data: AdvertisementStatus[]) => {
        this.Statuses = data;
      },
      error: (err) => {
        console.log("Error loading statuses: ", err);
      }
    });

    this.cityAreaService.getAll().subscribe({
      next: (data: CityArea[]) => {
        this.CityAreas = data;
      },
      error: (err) => {
        console.log("Error loading city areas: ", err);
      }
    });

    this.typeService.getAll().subscribe({
      next: (data: AdvertisementType[]) => {
        this.Types = data;
      },
      error: (err) => {
        console.log("Error loading types: ", err);
      }
    });
  }

  loadAdData(): void {
    this.loadingService.show();
    this.adService.getById(this.adId).subscribe({
      next: (ad: AdvertisementModel) => {
        this.currentAd = ad;
        this.populateForm(ad);
        this.loadingService.hide();
      },
      error: (err) => {
        this.loadingService.hide();
        this.notificationService.showError('Error loading advertisement', 'ERROR !');
        console.error('Load error:', err);
      }
    });
  }

  populateForm(ad: AdvertisementModel): void {
    this.EditAdvertisementForm.patchValue({
      subCategoryId: ad.subCategoryId.toString(),
      statusId: ad.statusId.toString(),
      typeId: ad.typeId.toString(),
      cityAreaId: ad.cityAreaId.toString(),
      name: ad.name,
      price: ad.price.toString(),
      description: ad.description,
      features: ad.features,
      image: ad.image
    });

    // Set image preview if exists
    if (ad.image) {
      this.selectedImage = {
        url: ad.image,
        file: new File([], 'current-image'),
        name: 'Current Image'
      };
    }
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.processFile(file);
      const reader = new FileReader();
      reader.onload = () => {
        const base64String = reader.result as string;
        this.EditAdvertisementForm.get('image')?.setValue(base64String);
      };
      reader.readAsDataURL(file);
    }
  }

  private processFile(file: File): void {
    if (this.validateFile(file)) {
      this.createImagePreview(file);
    }
  }

  private validateFile(file: File): boolean {
    const validTypes = ['image/jpeg', 'image/jpg', 'image/png'];
    const maxSize = 5 * 1024 * 1024; // 5MB
    
    if (!validTypes.includes(file.type)) {
      this.notificationService.showError('Please select only JPG, JPEG or PNG files', 'File Type Error');
      return false;
    }
    
    if (file.size > maxSize) {
      this.notificationService.showError('File size should be less than 5MB', 'File Size Error');
      return false;
    }
    
    return true;
  }

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

  removeImage(): void {
    this.selectedImage = null;
    this.EditAdvertisementForm.get('image')?.setValue('');
  }

  editSubmit(): void {
    this.loadingService.show();

    if (!this.EditAdvertisementForm.valid) {
      this.loadingService.hide();
      this.notificationService.showError('Please fill all required fields correctly.', 'Invalid');
      return;
    }

    const rawData = this.EditAdvertisementForm.getRawValue();

    const advertisement: AdvertisementModel = {
      id: this.adId,
      subCategoryId: Number(rawData.subCategoryId),
      statusId: Number(rawData.statusId),
      typeId: Number(rawData.typeId),
      postedById: this.currentAd.postedById,
      cityAreaId: Number(rawData.cityAreaId),
      name: rawData.name || '',
      price: rawData.price ? Number(rawData.price) : 0,
      description: rawData.description || '',
      features: rawData.features || '',
      hits: this.currentAd.hits,
      endsOn: this.currentAd.endsOn,
      startsOn: this.currentAd.startsOn,
      createdDate: this.currentAd.createdDate,
      image: rawData.image || ''
    }

    console.log("Advertisement Object for Update", advertisement);

    this.adService.update(this.adId, advertisement).subscribe({
      next: (data: any) => {
        console.log("Updated: ", data);
        this.loadingService.hide();
        this.notificationService.showSuccess("Advertisement updated successfully");
        this.router.navigate(['/user/dashboard']);
      },
      error: (err) => {
        this.loadingService.hide();
        this.notificationService.showError("Error updating advertisement", 'Failed !');
        console.error('Update error:', err);
      }
    });
  }
}