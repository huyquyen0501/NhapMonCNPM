import { Component, Injector, OnInit } from '@angular/core';
import { NguyenLieuService } from '@app/api/nguyen-lieu.service';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AddEditComponent } from './add-edit/add-edit.component';

@Component({
  selector: 'app-nguyen-lieu',
  templateUrl: './nguyen-lieu.component.html',
  styleUrls: ['./nguyen-lieu.component.css'],
  animations: [appModuleAnimation()]
})
export class NguyenLieuComponent extends PagedListingComponentBase<any> implements OnInit {

  bsModalRef: BsModalRef;
  tenNguyenLieu: string = '';
  dsNguyenLieu: any;
  constructor(
    private modalService: BsModalService,
    private inject: Injector,
    private api: NguyenLieuService
  ) {
    super(inject)
  }

  ngOnInit(): void {
    this.refresh();
  }

  list(
    request: PagedRequestDto,
    pageNumber: number
  ) {
    let input = {
      param: request,
      tenNguyenLieu: this.tenNguyenLieu ? this.tenNguyenLieu.trim() : ''
    }
    this.api.getAll(input).subscribe(data => {
      this.dsNguyenLieu = data.result.items;
      this.showPaging(data.result, pageNumber);
    })
  }

  delete() {

  }

  add() {
    const initialState = {
      title: 'Thêm nguyên liệu'
    };
    this.bsModalRef = this.modalService.show(AddEditComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
    this.bsModalRef.content.event.subscribe(data => {
      this.refresh();
    });
  }

  search() {
    this.getDataPage(1);
  }
  edit() {

  }
}
