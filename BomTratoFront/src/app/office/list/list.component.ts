import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Office } from '../../shared/models/office';
import { OfficeService } from '../../shared/services/office.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html'
})
export class ListComponent implements OnInit {

  public offices: Office [];
  errorMessage: string;

  constructor(private officeService: OfficeService,
              private spinner: NgxSpinnerService) { }

  ngOnInit(): void {

    this.spinner.show();

    this.officeService.getAllOffices()
    .subscribe(
      offices => this.offices = offices,
      error => this.errorMessage);

    this.spinner.hide();
  };

}
