import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { QuanLyMonAnService } from '../api/quan-ly-mon-an.service';
import { AddEditMonComponent } from './add-edit-mon/add-edit-mon.component';
import { ConfigNguyenLieuComponent } from './config-nguyen-lieu/config-nguyen-lieu.component';

@Component({
  selector: 'app-quan-ly-mon-an',
  templateUrl: './quan-ly-mon-an.component.html',
  styleUrls: ['./quan-ly-mon-an.component.css'],
  animations: [appModuleAnimation()]
})
export class QuanLyMonAnComponent extends PagedListingComponentBase<any> implements OnInit {

  tenMonAn: any = '';
  donGia: any = '';
  nguyenLieu: any = '';
  dsMonAn: any;
  bsModalRef: BsModalRef;

  constructor(
    private api: QuanLyMonAnService,
    private inject: Injector,
    private modalService: BsModalService
  ) {
    super(inject)
  }

  ngOnInit(): void {
    this.refresh();
  }

  list(
    request: PagedRequestDto,
    pageNumber: number,
  ) {
    let input = {
      gridParam: request,
      tenMonan: this.tenMonAn,
      dongia: this.donGia,
      nguyenlieu: this.nguyenLieu
    }
    this.api.getMonAn(input).subscribe(data => {
      this.dsMonAn = data.result.items;
      this.showPaging(data.result, pageNumber);
    })
  }

  add() {
    const initialState = {
      title: 'Thêm món ăn'
    };
    this.bsModalRef = this.modalService.show(AddEditMonComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
    this.bsModalRef.content.event.subscribe(data => {
      this.refresh();
    });
  }

  edit(data) {
    const initialState = {
      title: 'Sửa món ăn: ' + data.tenMonan,
      detailMonAn: data
    };
    this.bsModalRef = this.modalService.show(AddEditMonComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
    this.bsModalRef.content.event.subscribe(data => {
      this.refresh();
    });
  }

  CFNguyenLieu(data) {
    const initialState = {
      title: 'Thêm/Sửa nguyên liệu cho món: ' + data.tenMonan,
      detail: data
    };
    this.bsModalRef = this.modalService.show(ConfigNguyenLieuComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
    // this.bsModalRef.content.event.subscribe(data => {
    //   this.refresh();
    // });
  }

  delete() { }

  search() {
    this.getDataPage(1);
  }

  viewDonVi(aa: number) {
    if (aa == 1) {
      return 'Đĩa'
    }
    if (aa == 2) {
      return 'Bát'
    }
    if (aa == 3) {
      return 'Con'
    }
    if (aa == 4) {
      return 'Kg'
    }
  }
}