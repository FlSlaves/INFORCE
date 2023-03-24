import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user';
export const ACCESS_TOKEN_KEY = 'my_access_token';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl: string = 'https://localhost:7161/api/Authorize/';
  private user:any;
  constructor(private http: HttpClient) { 
    if(this.isLogged())
      this.user = this.decode();
  }
 
  register(userObj:any){
  return this.http.post<any>(`${this.baseUrl}Register`,userObj)
  }
  login(loginObj:any){
    return this.http.post<any>(`${this.baseUrl}SignIn`, loginObj);
  } 
  singOut(){
    localStorage.clear();
  }
  get token(): any {
    return localStorage.getItem('auth');
  }
  isLogged():boolean{
    return !!localStorage.getItem('auth');
  }
  decode(){
    return JSON.parse(atob(this.token.split('.')[1]));
  }
  getUserNameFromToken(){
    return this.user.Name
  }
  getUserRoleFromToken() {
      return this.user.Role
  }
  refresh(): void {
    window.location.reload();
  }
}
