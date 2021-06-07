import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Aprover } from 'src/app/shared/models/aprover';
import { AproverService } from 'src/app/shared/services/aprover.service';
import { Process } from '../../shared/models/process';
import { ProcessService } from '../../shared/services/process.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html'
})
export class ListComponent implements OnInit {

  errors: any[] = [];
  public processes: Process [];
  errorMessage: string;
  aprover: Aprover = new Aprover();

  constructor(private processService: ProcessService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private router: Router,
              private aproverService: AproverService) { }

  ngOnInit(): void {

    this.spinner.show();

    this.processService.getAll()
    .subscribe(
      offices => this.processes = offices,
      error => this.errorMessage);


    this.spinner.hide();
  };


  aproveProcess(process: Process){

    process.aproved = true;

    this.processService.updateProcess(process)
      .subscribe(
        success => { this.proccessSuccess(success)},
        fail => { this.proccessFail(fail)}
      )
    
  }


  InactiveProcess(process: Process){

    process.status = true;

    this.processService.updateProcess(process)
      .subscribe(
        success => { this.proccessSuccess(success)},
        fail => { this.proccessFail(fail)}
      )
    
  }

  proccessSuccess(response: any){
    this.errors = [];

    let toast = this.toastr.success('Processo atualizado com sucesso!', 'Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/processos/listar-todos']);
      });
    }
  }

  proccessFail(fail: any){
    this.errors = fail.error.errors.Message;
    this.toastr.error("Ocorreu um erro!, Ops..!!!");
  }

}