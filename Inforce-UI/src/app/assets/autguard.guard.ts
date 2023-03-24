import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';



@Injectable({
  providedIn: 'root'
})
export class AutguardGuard implements CanActivate {
  constructor(private auth:AuthService){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const isAuthorized = this.auth.isLogged();  
    if(!isAuthorized)
    {
      window.alert('You must login to continue')
    } 
    return isAuthorized; 
  }
  
}
