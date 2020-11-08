import { Component, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NguyenLieuService } from '@app/api/nguyen-lieu.service';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.css']
})
export class AddEditComponent implements OnInit {

  title: any;
  formAdd: FormGroup;
  dsNguyenLieu: any;
  public event: EventEmitter<any> = new EventEmitter();

  constructor(
    private fb: FormBuilder,
    private api: NguyenLieuService,
    public bsModalRef: BsModalRef,
  ) { }

  ngOnInit(): void {
    this.formAdd = this.fb.group({
      tenNguyenLieu: [],
      soLuong: [],
      donGia: [],
    })
  }

  save() {
    let input = {
      tenNguyenLieu: this.formAdd.value.tenNguyenLieu,
      soLuong: this.formAdd.value.soLuong,
      donGia: this.formAdd.value.donGia,
    }
    this.api.ThemNguyenLieu(input).subscribe(data => {
      this.dsNguyenLieu = data.result;
      abp.message.success("Thêm thành công.");
      this.bsModalRef.hide();
      this.event.emit({ data: 'ok' });
    })
  }
}
