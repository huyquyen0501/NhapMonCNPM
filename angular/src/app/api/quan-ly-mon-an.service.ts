import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QuanLyMonAnService extends BaseApiService{
  constructor(http: HttpClient) {
    super(http);
  }
  name() {
    return 'QuanlyMonan';
  }

  getMonAn(input: any):Observable<any>{
    return this.http.post(this.rootUrl + '/DanhSachMonAn', input)
  }
  Sua(input: any):Observable<any>{
    return this.http.post(this.rootUrl + '/SuaMonAn', input)
  }
  Add(input: any):Observable<any>{
    return this.http.post(this.rootUrl + '/ThemMonAn', input)
  }
}