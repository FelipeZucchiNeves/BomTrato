import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Aprover } from 'src/app/shared/models/aprover';
import { AproverService } from 'src/app/shared/services/aprover.service';
import { Process } from '../../shared/models/process';
import { ProcessService } from '../../shared/services/process.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html'
})
export class DetailsComponent implements OnInit{

  process: Process = new Process();
  date: Date;
  aprover: Aprover = new Aprover();
  
  errorMessage: string;
  constructor(
    private route: ActivatedRoute,
    private processService: ProcessService,
    private aproverService: AproverService,
    private spinner: NgxSpinnerService) {

    this.process = this.route.snapshot.data['process'];

    
  }
  ngOnInit(): void {
    this.spinner.show();

    this.aproverService.getById(this.process.aprovadorId)
      .subscribe(
        aprovers => this.aprover = aprovers,
        error => this.errorMessage
      );

    this.spinner.hide();
  }
}
