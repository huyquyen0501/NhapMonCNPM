import { Component, Inject, Injector, OnInit } from '@angular/core';
import { NguyenLieuService } from '@app/api/nguyen-lieu.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { MAT_DIALOG_DATA } from "@angular/material/dialog";

@Component({
  selector: 'app-config-nguyen-lieu',
  templateUrl: './config-nguyen-lieu.component.html',
  styleUrls: ['./config-nguyen-lieu.component.css']
})
export class ConfigNguyenLieuComponent implements OnInit {

  title: string;
  dsNguyenLieu: any = [];
  detail: any;
  dsNguyenLieuBanDau: any = [];
  dsNguyenLieuView: any = [];

  constructor(
    private api: NguyenLieuService,
    public bsModalRef: BsModalRef,
  ) {
   }

  ngOnInit(): void {
    this.dsNguyenLieuBanDau = this.detail.danhsachNguyenLieu.map(x =>
      x['value'] = true
    )
    console.log(this.dsNguyenLieuBanDau)
    this.api.DanhSachNguyenLieuNoPaging().subscribe(data => {
      this.dsNguyenLieu = data.result;
      this.dsNguyenLieu.map(x =>
        x['value'] = false
      )
    })
  }
  getdsNL() {
    this.dsNguyenLieu.forEach(s => {
      this.detail.danhsachNguyenLieu.forEach(x => {
        if(s.tenNguyenLieu == x.tenNguyenLieu){
          let item = {
            id: s.id,
            tenNguyenLieu: s.tenNguyenLieu,
            soLuong: x.soLuong
          }
        }
      })
    })
  }

  save() {
   let dialog = MAT_DIALOG_DATA;
  }

}
