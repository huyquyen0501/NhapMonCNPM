import { Component, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { QuanLyMonAnService } from '@app/api/quan-ly-mon-an.service';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-add-edit-mon',
  templateUrl: './add-edit-mon.component.html',
  styleUrls: ['./add-edit-mon.component.css']
})
export class AddEditMonComponent implements OnInit {

  formAdd: FormGroup;
  title: any;
  tenMonAn: any;
  donGia: any;
  nguyenLieu: any;
  donViTinh: any;
  file: any;
  dsDV = [
    { text: 'Đĩa', value: 1 },
    { text: 'Bắt', value: 2 },
    { text: 'Con', value: 3 },
    { text: 'Kg', value: 4 },
  ]
  imgsrc = '../../../assets/img/iiii.png';
  public event: EventEmitter<any> = new EventEmitter();
  detailMonAn: any;

  constructor(
    private api: QuanLyMonAnService,
    public domFile: DomSanitizer,
    public bsModalRef: BsModalRef,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    if (this.detailMonAn === undefined) {
      this.formAdd = this.fb.group({
        TenMonAn: ['', Validators.required],
        DonGia: ['', Validators.required],
        DonViTinh: ['', Validators.required],
        DanhsachNguyenlieu: ['', Validators.required],
        
      })
    } else {
      this.formAdd = this.fb.group({
        TenMonAn: [this.detailMonAn.tenMonan],
        DonGia: [this.detailMonAn.giaMon],     
        
        DonViTinh: [this.detailMonAn.donViTinh, Validators.required],
        DanhsachNguyenlieu: ['', Validators.required],
      })
      if (this.detailMonAn.hinhAnh != null) {
        this.imgsrc = this.detailMonAn.hinhAnh
      }
      console.log(this.formAdd.value.donGia);
    }
  }

  fileChange(e) {
    this.file = e.srcElement.files[0];
    const extFileAccept = ['bmp', 'dib', 'png', 'jfif', 'pjpeg', 'jpeg', 'pjp', 'jpg', 'gif'];
    const ext = this.file.name.substring(this.file.name.lastIndexOf('.') + 1);

    if (!extFileAccept.includes(ext)) {
      abp.message.error('File ảnh không đúng định dạng');
      return;
    }

    this.imgsrc = window.URL.createObjectURL(this.file);
  }

  save() {
    
    
    if(this.formAdd.value.TenMonAn==''){
      abp.message.error('Bạn chưa điền tên món ăn');
      
    }else
    if(this.formAdd.value.DonGia=='' || (this.formAdd.value.DonGia<=0)){
      abp.message.error('Bạn chưa nhập đơn giá hoặc đơn giá nhỏ hơn = 0');
    }else
    if(this.formAdd.value.DonViTinh == ''){
      abp.message.error('Bạn chưa chọn đơn vị tính');
    }else if(this.file===undefined){
      abp.message.error('Bạn chưa đăng ảnh món ăn')
    }
    else{
     
      
      
    const formData = new FormData();
    formData.append('tenMonAn', this.formAdd.value.TenMonAn);
    formData.append('donGia', this.formAdd.value.DonGia);
    formData.append('danhsachNguyenlieu', this.formAdd.value.DanhsachNguyenlieu);
    formData.append('donViTinh', this.formAdd.value.DonViTinh);
    formData.append('HinhAnh', this.file);
    formData.append('DanhsachNguyenlieu', '')
    if (this.detailMonAn) {
      formData.append('id', this.detailMonAn.id);
      this.api.Sua(formData).subscribe(() => {
        abp.message.success('Sửa thành công')
        this.event.emit({ data: 'ok' });
        this.bsModalRef.hide();
      }, r => {

      })
    } else {
      this.api.Add(formData).subscribe(() => {
        abp.message.success('Thêm thành công')
        this.event.emit({ data: 'ok' });
        this.bsModalRef.hide();
      }, r => {

      })
    }}
  }

}
