import { Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { CustomValidators } from 'ngx-custom-validators';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { fromEvent, merge, Observable } from 'rxjs';
import { Office } from 'src/app/shared/models/office';
import { OfficeService } from 'src/app/shared/services/office.service';
import { ValidationMessages, GenericValidator, DisplayMessage } from 'src/app/utils/generic-form-validation';
import { LocalStorageUtils } from 'src/app/utils/localstorage';
import { Process } from '../../shared/models/process';
import { ProcessService } from '../../shared/services/process.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html'
})
export class EditComponent implements OnInit {

  @ViewChildren(FormControlName, {read: ElementRef}) formInputElements: ElementRef[];

  errors: any[] = [];
  processForm: FormGroup;

  process: Process = new Process();
  offices: Office[];

  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};
  localStorageUtils = new LocalStorageUtils();
  user = this.localStorageUtils.getUser();

  formResult: string = '';
  changesNotSave: boolean; 
  errorMessage: string;
  
  constructor(
    private fb: FormBuilder,
    private processService: ProcessService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private officeService: OfficeService
  ) { 
    
    this.validationMessages = {
    processNumber: {
      required: 'Informe o número do processo',
      rangeLength:'Processo deve ter 12 digitos'
    },
    value: {
      required: 'Informe o Valor do Processo',      
      min:'O valor mínimo do processo é de 30 mil reais'
    },
    escritorioId: {
      required: 'Informe o escritorio.',
    },
    complainerName: {
      required: 'Informe nome do cliente',
    }
  };
  this.genericValidator = new GenericValidator(this.validationMessages);

  this.process = this.route.snapshot.data['process'];
}

  ngOnInit(): void {

    this.spinner.show();

    this.processForm = this.fb.group({
      id: '',
      processNumber: ['', [Validators.required,CustomValidators.rangeLength([12,12])]],
      value: ['', [Validators.required, CustomValidators.min(30000)]],
      escritorioId: ['', [Validators.required]],
      aproved: ['', [Validators.required]],
      complainerName: ['', [Validators.required]],
    })

    this.officeService.getAllOffices()
    .subscribe(
      offices => this.offices = offices,
      error => this.errorMessage);
    this.fillForm();
    this.spinner.hide();
  }

  fillForm(){
    this.processForm.patchValue({
      id: this.process.id,
      processNumber: this.process.processNumber,
      value: this.process.value,
      aproved: this.process.aproved,
      complainerName: this.process.complainerName
    })
  }


  ngAfterViewInit(){
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processMessages(this.processForm);
      this.changesNotSave = true;
    })
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

  editProcess(){
    if(this.processForm.dirty && this.processForm.valid){
      
      this.process = Object.assign({}, this.process, this.processForm.value);
      this.process.aprovadorId = this.user.id;

      this.processService.updateProcess(this.process)
        .subscribe(
          success => { this.proccessSuccess(success)},
          fail => { this.proccessFail(fail)}
        );

        this.changesNotSave = false;
    }
  }

}



