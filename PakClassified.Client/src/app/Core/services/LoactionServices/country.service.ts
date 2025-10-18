import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { apiBaseUrl } from '../../../environments/environment.dev';
import { Country } from '../../Model/location/country.model';


@Injectable({
  providedIn: 'root'
})
export class CountryService {
  private httpClient = inject(HttpClient);
  private baseUrl = apiBaseUrl+'Country';
  constructor( ) { }
  getById(id:number){
    return this.httpClient.get<Country>(`${this.baseUrl}/${id}`);
  }
  getAll(){
    return this.httpClient.get<Country[]>(this.baseUrl);
  }
  create(req:Country){
    return this.httpClient.post<Country>(this.baseUrl, req);
  }
  update(id:number, req:Country){
    return this.httpClient.put<Country>(this.baseUrl+'/'+id, req)
  }
  delete(id:number){
    return this.httpClient.delete(this.baseUrl+'/'+id)
  }
}
