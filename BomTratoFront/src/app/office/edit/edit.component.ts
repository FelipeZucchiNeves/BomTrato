import { Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { fromEvent, merge, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

import { utilsBr } from 'js-brasil';
import { ToastrService } from 'ngx-toastr';
import { NgBrazilValidators } from 'ng-brazil';
import { NgxSpinnerService } from 'ngx-spinner';

import { GetCep, Office } from '../../shared/models/office';
import { ValidationMessages, GenericValidator, DisplayMessage } from 'src/app/utils/generic-form-validation';
import { OfficeService } from '../../shared/services/office.service';
import { StringUtils } from 'src/app/utils/string-utils';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html'
})
export class EditComponent implements OnInit {
  @ViewChildren(FormControlName, { read: ElementRef}) formInputElements: ElementRef[];

  errors: any [] = [];
  officeForm: FormGroup;
  office: Office = new Office();


  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  MASKS = utilsBr.MASKS;
  formResult: string = '';
  cep: string;

  changesNotSave: boolean;

  constructor(private fb: FormBuilder,
    private officeService: OfficeService,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private route: ActivatedRoute,) { 

      this.validationMessages = {
        street: {
          required: 'Informe o Logradouro',
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
      this.office = this.route.snapshot.data['office'];
      this.validateCep(this.office.cep.toString());
    }


  ngOnInit(): void {

    this.spinner.show()
    this.officeForm = this.fb.group({
      id:'',
      street: ['', [Validators.required]],
      number: ['', [Validators.required]],
      district: ['', [Validators.required]],
      cep: ['', [Validators.required, NgBrazilValidators.cep]],
      city: ['', [Validators.required]],
      state: ['', [Validators.required]]
  });

    this.fillForm();
    this.spinner.hide()

  }

  validateCep(cep: string) : string {
    if(cep.length <= 7)return this.cep = "0"+ cep;
    return this.cep = cep;
  }

  fillForm() {

    this.officeForm.patchValue({
        id: this.office.id,
        street: this.office.street,
        district: this.office.district,
        cep: this.cep,
        city: this.office.city,
        state: this.office.state
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

  getCep(cep: string) {

    cep = StringUtils.onlyNumbers(cep);
    if (cep.length < 8) return;

    this.officeService.getCep(cep)
      .subscribe(
        cepRetorno => this.fillAddress(cepRetorno),
        erro => this.errors.push(erro));
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

  editOffice() {
    if (this.officeForm.dirty && this.officeForm.valid) {

      this.office = Object.assign({}, this.office, this.officeForm.value);

      let cep = StringUtils.onlyNumbers(this.officeForm.value['cep']);

      console.log(cep)

      this.office.cep = Number.parseInt(cep);

      console.log(this.office)
      this.officeService.updateOffice(this.office)
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
