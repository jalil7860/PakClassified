import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { apiBaseUrl } from '../../../environments/environment.dev';
import { AdvertisementType } from '../../Model/Advertisment/AdvertisementType.model';


@Injectable({
  providedIn: 'root'
})
export class AdvertisementTypeService {
  private httpClient = inject(HttpClient);
  private baseUrl = apiBaseUrl+'AdvertisementType';
  constructor( ) { }
  getById(id:number){
    return this.httpClient.get<AdvertisementType>(`${this.baseUrl}/${id}`);
  }
  getAll(){
    return this.httpClient.get<AdvertisementType[]>(this.baseUrl);
  }
  create(req:AdvertisementType){
    return this.httpClient.post<AdvertisementType>(this.baseUrl, req);
  }
  update(id:number, req:AdvertisementType){
    return this.httpClient.put<AdvertisementType>(this.baseUrl+'/'+id, req)
  }
  delete(id:number){
    return this.httpClient.delete(this.baseUrl+'/'+id)
  }
}
