import { Component } from '@angular/core';
import { CarouselComponent } from '../carousel/carousel.component';
import { FiltersComponent } from '../filters/filters.component';
import { CategoriesComponent } from '../categories/categories.component';
import { LatestPostingComponent } from '../latest-posting/latest-posting.component';
import { FooterComponent } from '../footer/footer.component';
import { SearchResultsComponent } from '../search-results/search-results.component';
import { AdvertisementModel } from '../../Core/Model/Advertisment/Advertisement.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [CommonModule,CarouselComponent, FiltersComponent, CategoriesComponent, LatestPostingComponent, SearchResultsComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
 searchResults: AdvertisementModel[] = [];
  searchKeyword: string = '';

  onSearchResults(results: AdvertisementModel[]) {
    this.searchResults = results;
    console.log("result: ",results);
    
  }
}
