import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { apiBaseUrl } from '../../../environments/environment.dev';
import { AdvertisementCategory } from '../../Model/Advertisment/AdvertisementCategory.model';



@Injectable({
  providedIn: 'root'
})
export class AdvertisementCategoryService {
  private httpClient = inject(HttpClient);
  private baseUrl = apiBaseUrl+'AdvertisementCategory';
  constructor( ) { }
  getById(id:number){
    return this.httpClient.get<AdvertisementCategory>(`${this.baseUrl}/${id}`);
  }
  getAll(){
    return this.httpClient.get<AdvertisementCategory[]>(this.baseUrl);
  }
  create(req:AdvertisementCategory){
    return this.httpClient.post<AdvertisementCategory>(this.baseUrl, req);
  }
  update(id:number, req:AdvertisementCategory){
    return this.httpClient.put<AdvertisementCategory>(this.baseUrl+'/'+id, req)
  }
  delete(id:number){
    return this.httpClient.delete(this.baseUrl+'/'+id)
  }
}
