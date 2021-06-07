import { Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomValidators } from 'ngx-custom-validators';
import { ToastrService } from 'ngx-toastr';
import { Observable, fromEvent, merge } from 'rxjs';
import { Office } from 'src/app/shared/models/office';
import { OfficeService } from 'src/app/shared/services/office.service';
import { ValidationMessages, GenericValidator, DisplayMessage } from 'src/app/utils/generic-form-validation';
import { LocalStorageUtils } from 'src/app/utils/localstorage';
import { Process } from '../../shared/models/process';
import { ProcessService } from '../../shared/services/process.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html'
})
export class AddComponent implements OnInit {

  @ViewChildren(FormControlName, { read: ElementRef}) formInputElements: ElementRef[];

  errors: any [] = [];
  processForm: FormGroup;
  process: Process = new Process;
  offices: Office[];

  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};
  localStorageUtils = new LocalStorageUtils();

  user = this.localStorageUtils.getUser();

  errorMessage: string;

  formResult: string = '';

  changesNotSave: boolean;


  constructor(private fb: FormBuilder,
    private processService: ProcessService,
    private router: Router,
    private toastr: ToastrService,
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
        complainerName: {
          required: 'Informe nome do cliente',
          rangeLength: 'A senha deve possuir entre 2 e 100 caracteres números e caracteres especiais'
        }
      };
      this.genericValidator = new GenericValidator(this.validationMessages);
    }

  ngOnInit(): void {

    this.processForm = this.fb.group({
      processNumber: ['', [Validators.required, CustomValidators.rangeLength([12,12])]],
      value: ['', [Validators.required, CustomValidators.min(30000)]],
      escritorioId: [''],
      complainerName: ['', [Validators.required, CustomValidators.rangeLength([2,100])]],
    });

    this.officeService.getAllOffices()
    .subscribe(
      offices => this.offices = offices,
      error => this.errorMessage);

  }

  ngAfterViewInit(): void {


    let controlBlurs: Observable<any>[] = this.formInputElements
    .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processMessages(this.processForm);
      this.changesNotSave = true;
    });

  }

  addProcess() {
    
    if (this.processForm.dirty && this.processForm.valid) {

      this.process = Object.assign({}, this.process, this.processForm.value);

      this.process.aprovadorId = this.user.id;

      this.processService.newProcess(this.process)
        .subscribe(
          success => { this.processSuccess(success) },
          error => { this.processError(error) }
        );
    }
  }


  processSuccess(response: any) {
    this.processForm.reset();
    this.errors = [];

    this.changesNotSave = false;

    let toast = this.toastr.success('Processo cadastrado com sucesso!', 'Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/processos/listar-todos']);
      });
    }
  }

  processError(fail: any) {
    this.errors = fail.error.errors.Message;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }


}
