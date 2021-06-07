import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Aprover } from '../../shared/models/aprover';
import { AproverService } from '../../shared/services/aprover.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html'
})
export class ListComponent implements OnInit {

  public aprovers: Aprover[];
  errorMessage: string;
  

  constructor(private aproverService: AproverService,
              private spinner: NgxSpinnerService) { }

  ngOnInit() {

    this.spinner.show();

    this.aproverService.getAll()
      .subscribe(
        aprovers => this.aprovers = aprovers,
        error => this.errorMessage
      );

    this.spinner.hide();
  }

}
