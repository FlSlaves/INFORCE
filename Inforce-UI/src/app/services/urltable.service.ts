import { HttpClient } from '@angular/common/http';
import { Injectable, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { urltable } from '../models/urltable';

@Injectable({
  providedIn: 'root'
})
export class UrltableService {
  private baseUrl:string = 'https://localhost:7161/api/Url';
  
  constructor(private http:HttpClient) { }

  getUrls() : Observable<urltable[]>{
    return  this.http.get<urltable[]>(`${this.baseUrl}`)
  }
  getUrlByCode(url: urltable): Observable<urltable[]> {
    return this.http.put<urltable[]>(`${this.baseUrl}/${url.urlName}`,url)
  }
  deleteUrls(url: urltable): Observable<urltable[]>{
    return this.http.delete<urltable[]>(`${this.baseUrl}/${url.id}`);
  }
  createUrls(url: urltable): Observable<urltable[]>{
    return this.http.post<urltable[]>(`${this.baseUrl}/CreateRecord`, url)
  }
  updateUrls(url: urltable): Observable<urltable[]> {
    return this.http.put<urltable[]>(`${this.baseUrl}/UpdateRecord`, url)
  }

}
