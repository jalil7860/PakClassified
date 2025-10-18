import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { apiBaseUrl } from '../../../environments/environment.dev';
import { Province } from '../../Model/location/province.model';



@Injectable({
  providedIn: 'root'
})
export class ProvinceService {
  private httpClient = inject(HttpClient);
  private baseUrl = apiBaseUrl+'Province';
  constructor( ) { }
  getById(id:number){
    return this.httpClient.get<Province>(`${this.baseUrl}/${id}`);
  }
  getAll(){
    return this.httpClient.get<Province[]>(this.baseUrl);
  }
  create(req:Province){
    return this.httpClient.post<Province>(this.baseUrl, req);
  }
  update(id:number, req:Province){
    return this.httpClient.put<Province>(this.baseUrl+'/'+id, req)
  }
  delete(id:number){
    return this.httpClient.delete(this.baseUrl+'/'+id)
  }
}
