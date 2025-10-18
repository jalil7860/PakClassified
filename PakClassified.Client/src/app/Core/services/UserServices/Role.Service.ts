import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { apiBaseUrl } from '../../../environments/environment.dev';
import { Role } from '../../Model/User/Role.model';





@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private httpClient = inject(HttpClient);
  private baseUrl = apiBaseUrl+'Roles';
  constructor( ) { }
  getById(id:number){
    return this.httpClient.get<Role>(`${this.baseUrl}/${id}`);
  }
  getAll(){
    return this.httpClient.get<Role[]>(this.baseUrl);
  }
  create(req:Role){
    return this.httpClient.post<Role>(this.baseUrl, req);
  }
  update(id:number, req:Role){
    return this.httpClient.put<Role>(this.baseUrl+'/'+id, req)
  }
  delete(id:number){
    return this.httpClient.delete(this.baseUrl+'/'+id)
  }
}
