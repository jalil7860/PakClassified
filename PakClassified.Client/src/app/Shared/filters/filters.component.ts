import { CommonModule } from "@angular/common";
import { Component, inject, OnInit, Output, EventEmitter } from "@angular/core";
import { AbstractControl, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors } from "@angular/forms";
import { NotificationService } from "../../Core/Common/Notification/notification.service";
import { AdvertisementService } from "../../Core/services/PakClassified/advertisement.service";
import { AdvertisementCategoryService } from "../../Core/services/PakClassified/AdvertisementCategory.service";
import { CityAreaService } from "../../Core/services/LoactionServices/city-area.service";
import { LoadingService } from "../../Core/Common/Loading/loading.service";
import { AdvertisementModel } from "../../Core/Model/Advertisment/Advertisement.model";
import { AdvertisementCategory } from "../../Core/Model/Advertisment/AdvertisementCategory.model";
import { CityArea } from "../../Core/Model/location/cityArea.model";

export type SearchForm = FormGroup<{
  keyword: FormControl<string | null>;
  categoryId: FormControl<number | null>;
  cityAreaId: FormControl<number | null>;
}>;
 
export function atLeastOneRequired(
  control: AbstractControl
): ValidationErrors | null {
  const keyword = control.get('keyword')?.value;
  const categoryId = control.get('categoryId')?.value;
  const cityAreaId = control.get('cityAreaId')?.value;
 
  // Assuming a default dropdown value of 0 indicates no selection.
  if (
    (keyword && keyword.trim() !== '') ||
    (categoryId && categoryId !== 0) ||
    (cityAreaId && cityAreaId !== 0)
  )
    return null;
 
  return { atLeastOneRequired: true };
}
 
@Component({
  selector: 'app-filters',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.css'
})
 
export class FiltersComponent implements OnInit {
 
  searchSubmitted = false;
  notificationService = inject(NotificationService);
  advService = inject(AdvertisementService);
  categoryService = inject(AdvertisementCategoryService);
  cityareaService = inject(CityAreaService)
  loadingService = inject(LoadingService)
 
  fb = inject(FormBuilder);

  @Output() searchResults = new EventEmitter<AdvertisementModel[]>();
 
  advertisements: AdvertisementModel[] = [];
  searchcategory: AdvertisementCategory[] = [];
  searchcityarea: CityArea[] = [];
 
  form: SearchForm = this.fb.group(
    {
      keyword: this.fb.control<string>(''),
      categoryId: this.fb.control<number>(0),
      cityAreaId: this.fb.control<number>(0),
    },
    { validators: atLeastOneRequired }
  );
 
 
  ngOnInit() {
 
    this.categoryService.getAll().subscribe({
      next: (data) => {
        console.log('search bar categories', data);
        this.searchcategory = data;
      },
      error: (err) => {
        console.log(err);
      }
    });
 
    this.cityareaService.getAll().subscribe({
      next: (data) => {
        console.log('search bar cityareas', data);
        this.searchcityarea = data;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
 
 
  onSubmit() {
 
    this.loadingService.show();
 
    if (this.form.valid) {
      const values = this.form.getRawValue();
 
      this.advService
        .searchByQuery(
          values.keyword ?? undefined,
          values.categoryId ?? undefined,
          values.cityAreaId ?? undefined
        )
        .subscribe({
          next: (data: AdvertisementModel[]) => {
            console.log('Search Result:', data);
            this.advertisements = data.map((sr: any) => ({
              name: sr.name,
              description: sr.description,
              image: sr.image,
              categoryId: sr.categoryId,
              advertisement: sr.advertisement ?? null,
              id: sr.id,
              subCategoryId: sr.subCategoryId,
              subCategory: sr.subCategory,
              statusId: sr.statusId,
              status: sr.status,
              typeId: sr.typeId,
              type: sr.type,
              postedById: sr.postedById,
              postedBy: sr.postedBy,
              cityAreaId: sr.cityAreaId,
              cityArea: sr.cityArea,
              city: sr.city,
              title: sr.title,
              price: sr.price,
              features: sr.features,
              clientDescription: sr.clientDescription,
              hits: sr.hits,
              endsOn: sr.endsOn,
              startsOn: sr.startsOn,
              createdDate: sr.createdDate,
            }));
            
            // Emit search results to parent component
            this.searchResults.emit(this.advertisements);
            
            this.loadingService.hide();
            this.searchSubmitted = true;
 
            if (this.advertisements.length > 0) {
              this.notificationService.showSuccess("Advertisement Found");
            } else {
              this.notificationService.showError("No advertisements found!");
            }
           
          },
          error: (err)=> {
            this.loadingService.hide();
            this.notificationService.showError("Something went wrong while fetching advertisements!");
          }
        });
    }
    this.searchSubmitted = false;
  }
 
}