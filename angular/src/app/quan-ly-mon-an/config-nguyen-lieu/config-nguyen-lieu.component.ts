import { Component, OnInit } from '@angular/core';
import { NguyenLieuService } from '@app/api/nguyen-lieu.service';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-config-nguyen-lieu',
  templateUrl: './config-nguyen-lieu.component.html',
  styleUrls: ['./config-nguyen-lieu.component.css']
})
export class ConfigNguyenLieuComponent implements OnInit {

  title: string;
  dsNguyenLieu: any;
  detail: any;
  dsNguyenLieuBanDau: any
  dsNguyenLieuView: any

  constructor(
    private api: NguyenLieuService,
    public bsModalRef: BsModalRef
  ) { }

  ngOnInit(): void {
    this.dsNguyenLieuBanDau = this.detail.danhsachNguyenLieu.map(x =>
      x['value'] = true
    )
    this.api.DanhSachNguyenLieuNoPaging().subscribe(data => {
      this.dsNguyenLieu = data.result;
      this.dsNguyenLieu.map(x =>
        x['value'] = false
      )
    })
  }

  save() {

  }

}
