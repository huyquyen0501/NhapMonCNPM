import { Component, OnInit } from '@angular/core';
import { QuanLyMonAnService } from '@app/api/quan-ly-mon-an.service';

@Component({
  selector: 'app-add-edit-mon',
  templateUrl: './add-edit-mon.component.html',
  styleUrls: ['./add-edit-mon.component.css']
})
export class AddEditMonComponent implements OnInit {

  title: any;
  tenMonAn: any;
  donGia: any;
  nguyenLieu: any;
  donViTinh: any;
  dsDV = [
    {text: 'Đĩa', value: 1},
    {text: 'Bắt', value: 2},
    {text: 'Con', value: 3},
    {text: 'Kg', value: 4},
  ]
  constructor(
    private api: QuanLyMonAnService
  ) { }

  ngOnInit(): void {

  }

  save(){
    let input = {
      tenMonAn: this.tenMonAn,
      donGia: this.donGia,
      donViTinh: this.donViTinh,
      danhsachNguyenlieu: [
        1
      ]
    }
    this.api.Add(input).subscribe(()=>{
      abp.message.success('Thêm thành công')
    })
  }

}
