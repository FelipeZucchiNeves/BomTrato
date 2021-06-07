import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Process } from 'src/app/shared/models/process';
import { ProcessService } from 'src/app/shared/services/process.service';
import { LocalStorageUtils } from 'src/app/utils/localstorage';
import { Aprover } from '../../shared/models/aprover';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html'
})
export class DetailsComponent implements OnInit{

  aprover: Aprover = new Aprover();
  date: Date;

  errors: any[] = [];

  processes: Process[];
  localStorageUtils = new LocalStorageUtils();

  user = this.localStorageUtils.getUser();
  constructor(
    private route: ActivatedRoute,
    private processService: ProcessService) {

    this.aprover = this.route.snapshot.data['aprover'];
    
  }


  ngOnInit() {

    this.processService.getAll()
    .subscribe(
      process => this.processes = process,
      error => this.errors
    );

  }


}
