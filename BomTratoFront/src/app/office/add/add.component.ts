import { Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { fromEvent, merge, Observable } from 'rxjs';
import { Router } from '@angular/router';

import { utilsBr } from 'js-brasil';
import { ToastrService } from 'ngx-toastr';
import { NgBrazilValidators } from 'ng-brazil';

import { GetCep, Office } from '../../shared/models/office';
import { ValidationMessages, GenericValidator, DisplayMessage } from 'src/app/utils/generic-form-validation';
import { OfficeService } from '../../shared/services/office.service';
import { StringUtils } from 'src/app/utils/string-utils';
import { CustomValidators } from 'ngx-custom-validators';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html'
})
export class AddComponent implements OnInit {

  @ViewChildren(FormControlName, { read: ElementRef}) formInputElements: ElementRef[];

  errors: any [] = [];
  officeForm: FormGroup;
  office: Office = new Office();


  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  MASKS = utilsBr.MASKS;
  formResult: string = '';

  changesNotSave: boolean;

  constructor(private fb: FormBuilder,
    private officeService: OfficeService,
    private router: Router,
    private toastr: ToastrService) { 

      this.validationMessages = {
        street: {
          required: 'Informe o Logradouro',
          rangeLength: 'A senha deve possuir entre 2 e 50 caracteres números e caracteres especiais'
        },
        number: {
          required: 'Informe o Número',
        },
        district: {
          required: 'Informe o Bairro',
        },
        cep: {
          required: 'Informe o CEP',
          cep: 'CEP em formato inválido'
        },
        city: {
          required: 'Informe a Cidade',
        },
        state: {
          required: 'Informe o Estado',
        }
      };
  
      this.genericValidator = new GenericValidator(this.validationMessages);
    }

  ngOnInit(): void {

    this.officeForm = this.fb.group({
        street: ['', [Validators.required, CustomValidators.rangeLength([2,50])]],
        number: ['', [Validators.required]],
        district: ['', [Validators.required]],
        cep: ['', [Validators.required, NgBrazilValidators.cep]],
        city: ['', [Validators.required]],
        state: ['', [Validators.required]]
    });

  }

  ngAfterViewInit(): void {


    let controlBlurs: Observable<any>[] = this.formInputElements
    .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processMessages(this.officeForm);
      this.changesNotSave = true;
    });

  }


  fillAddress(getCep: GetCep) {

    this.officeForm.patchValue({
        street: getCep.logradouro,
        district: getCep.bairro,
        cep: getCep.cep,
        city: getCep.localidade,
        state: getCep.uf
    });
  }

  getCep(cep: string) {

    cep = StringUtils.onlyNumbers(cep);
    if (cep.length < 8) return;

    this.officeService.getCep(cep)
      .subscribe(
        cepRetorno => this.fillAddress(cepRetorno),
        erro => this.errors.push(erro));
  }

  addOffice() {
    if (this.officeForm.dirty && this.officeForm.valid) {

      this.office = Object.assign({}, this.office, this.officeForm.value);

      let cep = StringUtils.onlyNumbers(this.officeForm.value['cep']);

      this.office.cep = Number.parseInt(cep);

      this.officeService.newOffice(this.office)
        .subscribe(
          success => { this.processSuccess(success) },
          error => { this.processError(error) }
        );
    }
  }

  processSuccess(response: any) {
    this.officeForm.reset();
    this.errors = [];

    this.changesNotSave = false;

    let toast = this.toastr.success('Escritório cadastrado com sucesso!', 'Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/escritorios/listar-todos']);
      });
    }
  }

  processError(fail: any) {
    this.errors = fail.error.errors;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }



}
