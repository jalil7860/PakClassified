import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

import { apiBaseUrl } from '../../../environments/environment.dev';
import { CityArea } from '../../Model/location/cityArea.model';



@Injectable({
  providedIn: 'root'
})
export class CityAreaService {
  private httpClient = inject(HttpClient);
  private baseUrl = apiBaseUrl+'CityArea';
  constructor( ) { }
  getById(id:number){
    return this.httpClient.get<CityArea>(`${this.baseUrl}/${id}`);
  }
  getAll(){
    return this.httpClient.get<CityArea[]>(this.baseUrl);
  }
  create(req:CityArea){
    return this.httpClient.post<CityArea>(this.baseUrl, req);
  }
  update(id:number, req:CityArea){
    return this.httpClient.put<CityArea>(this.baseUrl+'/'+id, req)
  }
  delete(id:number){
    return this.httpClient.delete(this.baseUrl+'/'+id)
  }
}
