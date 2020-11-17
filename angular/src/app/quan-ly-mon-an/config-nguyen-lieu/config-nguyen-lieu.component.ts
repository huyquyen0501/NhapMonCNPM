import { Component, OnInit } from '@angular/core';
import { NguyenLieuService } from '@app/api/nguyen-lieu.service';
import { QuanLyMonAnService } from '@app/api/quan-ly-mon-an.service';
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
    private apiQLMA: QuanLyMonAnService,
    public bsModalRef: BsModalRef
  ) { }

  ngOnInit(): void {
    
    let a;
     this.apiQLMA.chiTietMonAn(this.detail.id).subscribe(s=>{a=s.result?.danhsachNguyenLieu
    this.dsNguyenLieuBanDau = a
    
    
    this.api.DanhSachNguyenLieuNoPaging().subscribe(data => {
      this.dsNguyenLieu = data.result;
    console.log('dsnguyenlieu',this.dsNguyenLieu);
    console.log('dsnguyenlieubandau',this.dsNguyenLieuBanDau);
    
    
      
      this.dsNguyenLieu.forEach(x =>
        (this.dsNguyenLieuBanDau==null?[]:this.dsNguyenLieuBanDau).forEach(y => {
          if(x.maNguyenLieu == y?.maNguyenLieu){
            x.value = true;
            x.soLuong= y?.soLuong
          }
        })
      )
    })
    })
    // this.api.DanhSachNguyenLieuNoPaging().subscribe(data => {
    //   this.dsNguyenLieu = data.result;
    //   console.log('aa',this.dsNguyenLieuBanDau);
      
    //   this.dsNguyenLieu.forEach(x =>
    //     this.dsNguyenLieuBanDau.forEach(y => {
    //       if(x.maNguyenLieu == y){
    //         x.value = true;
    //       }
    //     })
    //   )
    // })
  }

  save() {
    let item = [];
    this.dsNguyenLieu.forEach(x => {
      if(x.value){
        let tg = {
          maNguyenLieu: x.maNguyenLieu,
      tenNguyenLieu: x.tenNguyenLieu,
      soLuong: x.soLuong
        }
        item.push(tg);
      }
    })
    let data = {
      idMonAn: this.detail.id,
      danhSachNguyenLieu: item
    }
    this.apiQLMA.capNhatNguyenLieu(data).subscribe(() =>{
      abp.notify.success("Thêm thành công!")
    });
    this.bsModalRef.hide();
  }

}
