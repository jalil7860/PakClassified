import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

import { apiBaseUrl } from '../../../environments/environment.dev';
import { AdvertisementModel } from '../../Model/Advertisment/Advertisement.model';



@Injectable({
  providedIn: 'root'
})
export class AdvertisementService {
  private httpClient = inject(HttpClient);
  private baseUrl = apiBaseUrl+'Advertisement';
  getById(id:number){
    return this.httpClient.get<AdvertisementModel>(`${this.baseUrl}/${id}`);
  }
  getPostByUserId(userId: number) {
       return this.httpClient.get<AdvertisementModel[]>(`${this.baseUrl}/GetPostByUserId/${userId}`);
    }
  getAll(){
    return this.httpClient.get<AdvertisementModel[]>(this.baseUrl);
  }
  searchByQuery(name?: string, categoryId?:number, cityAreaId?:number){
    let httpParams = new HttpParams();
    if(name)
      httpParams = httpParams.append("name", name)
    if(categoryId)
      httpParams = httpParams.append("category", categoryId)
    if(cityAreaId)
      httpParams = httpParams.append("cityArea", cityAreaId)
    
    return this.httpClient.get<AdvertisementModel[]>(this.baseUrl+'/Search', {
      params: httpParams
    }) 
  }
  create(req:AdvertisementModel){
    return this.httpClient.post<AdvertisementModel>(this.baseUrl, req);
  }
  update(id:number, req:AdvertisementModel){
    return this.httpClient.put<AdvertisementModel>(this.baseUrl+'/'+id, req)
  }
  delete(id:number){
    return this.httpClient.delete(this.baseUrl+'/'+id)
  }
}
