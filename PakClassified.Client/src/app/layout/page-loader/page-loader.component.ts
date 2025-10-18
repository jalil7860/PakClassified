import { Component, inject } from '@angular/core';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import { AsyncPipe, DOCUMENT } from '@angular/common';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-page-loader',
  imports: [AsyncPipe],
  templateUrl: './page-loader.component.html',
  styleUrl: './page-loader.component.css'
})
export class PageLoaderComponent {
  public loadingService = inject(LoadingService);
  
  private document = inject(DOCUMENT);
  private sub?: Subscription;
 
  isLoading = false;
 
  ngOnInit() {
    this.sub = this.loadingService.isLoading$.subscribe(val => {
      this.isLoading = val;
    });
 
    // Move host element outside app-root (directly under body)
    const host = (this.document.querySelector('app-loading-overlay') as HTMLElement);
    if (host) {
      this.document.body.appendChild(host);
    }
  }
 
  ngOnDestroy() {
    this.sub?.unsubscribe();
  }
}
