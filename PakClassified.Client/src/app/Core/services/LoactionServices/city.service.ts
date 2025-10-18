import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { apiBaseUrl } from '../../../environments/environment.dev';
import { City } from '../../Model/location/city.model';



@Injectable({
  providedIn: 'root'
})
export class CityService {
  private httpClient = inject(HttpClient);
  private baseUrl = apiBaseUrl+'City';
  constructor( ) { }
  getById(id:number){
    return this.httpClient.get<City>(`${this.baseUrl}/${id}`);
  }
  getAll(){
    return this.httpClient.get<City[]>(this.baseUrl);
  }
  create(req:City){
    return this.httpClient.post<City>(this.baseUrl, req);
  }
  update(id:number, req:City){
    return this.httpClient.put<City>(this.baseUrl+'/'+id, req)
  }
  delete(id:number){
    return this.httpClient.delete(this.baseUrl+'/'+id)
  }
}
