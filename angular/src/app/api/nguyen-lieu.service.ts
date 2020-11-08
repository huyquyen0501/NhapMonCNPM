import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class NguyenLieuService extends BaseApiService {

  constructor(http: HttpClient) {
    super(http);
  }
  name() {
    return 'QuanLyNguyenLieu';
  }

  getAll(input: any): Observable<any> {
    return this.http.post(this.rootUrl + '/DanhSachNguyenLieuPaging', input)
  }

  ThemNguyenLieu(input: any): Observable<any> {
    return this.http.post(this.rootUrl + '/ThemNguyenLieu', input)
  }

  DanhSachNguyenLieuNoPaging(): Observable<any> {
    return this.http.get(this.rootUrl + '/DanhSachNguyenLieuNoPaging')
  }
}
