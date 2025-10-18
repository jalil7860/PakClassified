import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { apiBaseUrl } from '../../../environments/environment.dev';
import { AdvertisementSubCategory } from '../../Model/Advertisment/AdvertisementSubCategory.model';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class AdvertisementSubCategoryService {
  private httpClient = inject(HttpClient);
  private baseUrl = apiBaseUrl+'SubCategory';
  constructor( ) { }
  getById(id:number){
    return this.httpClient.get<AdvertisementSubCategory>(`${this.baseUrl}/${id}`);
  }
  GetByCategoryId(categoryId: number){
    return this.httpClient.get<AdvertisementSubCategory[]>(`${this.baseUrl}/ByCategory/${categoryId}`)
  }
  getAll(){
    return this.httpClient.get<AdvertisementSubCategory[]>(this.baseUrl);
  }
  create(req:AdvertisementSubCategory){
    return this.httpClient.post<AdvertisementSubCategory>(this.baseUrl, req);
  }
  update(id:number, req:AdvertisementSubCategory){
    return this.httpClient.put<AdvertisementSubCategory>(this.baseUrl+'/'+id, req)
  }
  delete(id:number){
    return this.httpClient.delete(this.baseUrl+'/'+id)
  }
}
