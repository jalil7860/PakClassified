import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { apiBaseUrl } from '../../../environments/environment.dev';
import { AdvertisementStatus } from '../../Model/Advertisment/AdvertisementStatus.model';



@Injectable({
  providedIn: 'root'
})
export class  AdvertisementStatusService{
  private httpClient = inject(HttpClient);
  private baseUrl = apiBaseUrl+'AdvertisementStatus';
  constructor( ) { }
  getById(id:number){
    return this.httpClient.get<AdvertisementStatus>(`${this.baseUrl}/${id}`);
  }
  getAll(){
    return this.httpClient.get<AdvertisementStatus[]>(this.baseUrl);
  }
  create(req:AdvertisementStatus){
    return this.httpClient.post<AdvertisementStatus>(this.baseUrl, req);
  }
  update(id:number, req:AdvertisementStatus){
    return this.httpClient.put<AdvertisementStatus>(this.baseUrl+'/'+id, req)
  }
  delete(id:number){
    return this.httpClient.delete(this.baseUrl+'/'+id)
  }
}
