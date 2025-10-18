import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AdvertisementModel } from '../../Core/Model/Advertisment/Advertisement.model';

@Component({
  selector: 'app-search-results',
  imports: [CommonModule, RouterModule],
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css']
})
export class SearchResultsComponent implements OnInit {
  @Input() advertisements: AdvertisementModel[] = [];
  @Input() searchTerm: string = '';

  ngOnInit(): void {
    console.log('Search Results Received:', this.advertisements);
  }

  formatPrice(price: number): string {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  }


  getImage(ad: AdvertisementModel): string {
    return ad.image || 'assets/images/default-vehicle.jpg';
  }

  getDaysAgo(createdDate: string): string {
    const created = new Date(createdDate);
    const now = new Date();
    const diffTime = Math.abs(now.getTime() - created.getTime());
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    
    if (diffDays === 1) return '1 day ago';
    if (diffDays < 7) return `${diffDays} days ago`;
    if (diffDays < 30) return `${Math.floor(diffDays / 7)} weeks ago`;
    return `${Math.floor(diffDays / 30)} months ago`;
  }
}