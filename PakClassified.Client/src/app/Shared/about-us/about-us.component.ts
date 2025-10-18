import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AdvertisementService } from '../../Core/services/PakClassified/advertisement.service';
import { UserService } from '../../Core/services/UserServices/User.service';
import { CityService } from '../../Core/services/LoactionServices/city.service';

@Component({
  selector: 'app-about-us',
  imports: [],
  templateUrl: './about-us.component.html',
  styleUrl: './about-us.component.css'
})
export class AboutUsComponent implements OnInit{
    advertisementService  = inject(AdvertisementService);
    userService = inject(UserService);
    cityService = inject(CityService);

    totalAdvs: number = 0;
    totalUsers : number = 0;
    totalCities : number = 0;

    ngOnInit(): void {
      this.advertisementService.getAll().subscribe({
        next: (data) => {
          this.totalAdvs = data.length;
        },error: (err) => {
          console.log("Error: ", err);
        }
      });

      this.cityService.getAll().subscribe({
        next: (data) => {
          this.totalCities = data.length;
        },error: (err) => {
          console.log("Error: ", err);
        }
      });

      this.userService.getAll().subscribe({
        next: (data) => {
          this.totalUsers = data.length;
        },error: (err) => {
          console.log("Error: ", err);
        }
      });
    }
}
