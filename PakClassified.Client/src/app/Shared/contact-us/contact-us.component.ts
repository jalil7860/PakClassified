import { Component, inject, OnInit } from '@angular/core';
import { LoadingService } from '../../Core/Common/Loading/loading.service';

@Component({
  selector: 'app-contact-us',
  imports: [],
  templateUrl: './contact-us.component.html',
  styleUrl: './contact-us.component.css'
})
export class ContactUsComponent implements OnInit{
  loadingservice = inject(LoadingService)
  ngOnInit(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
    this.loadingservice.show();

    setTimeout(() => {
      this.loadingservice.hide();
    }, 1500);
  }
}
