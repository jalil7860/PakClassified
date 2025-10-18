import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { apiBaseUrl } from '../../../environments/environment.dev';
import { User } from '../../Model/User/User.model';



@Injectable({
  providedIn: 'root'
})
export class UserService {
  private httpClient = inject(HttpClient);
  private baseUrl = apiBaseUrl+'User';
  constructor( ) { }
  getById(id:number){
    return this.httpClient.get<User>(`${this.baseUrl}/${id}`);
  }
  getAll(){
    return this.httpClient.get<User[]>(this.baseUrl);
  }
  create(req:User){
    return this.httpClient.post<User>(this.baseUrl, req);
  }
  update(id:number, req:User){
    return this.httpClient.put<User>(this.baseUrl+'/'+id, req)
  }
  delete(id:number){
    return this.httpClient.delete(this.baseUrl+'/'+id)
  }
}
