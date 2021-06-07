import { Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { CustomValidators } from 'ngx-custom-validators';
import { ToastrService } from 'ngx-toastr';
import { Observable, fromEvent, merge } from 'rxjs';
import { ValidationMessages, GenericValidator, DisplayMessage } from 'src/app/utils/generic-form-validation';
import { Aprover } from '../../shared/models/aprover';
import { IDate } from '../models/IDate';
import { AproverService } from '../../shared/services/aprover.service';


@Component({
  selector: 'app-add',
  templateUrl: './add.component.html'
})
export class AddComponent implements OnInit {

  @ViewChildren(FormControlName, {read: ElementRef}) formInputElements: ElementRef[];


  errors: any[] = [];
  model: NgbDateStruct;
  aproverForm: FormGroup;

  aprover: Aprover = new Aprover();


  validateMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  formResult: string = '';
  changesNotSave: boolean;

  constructor(private fb: FormBuilder,
    private aproverService: AproverService,
    private router: Router,
    private toastr: ToastrService) { 

      this.validateMessages = {
        name: {
          required: 'Informe o nome do colaborador',
          rangeLength: 'A senha deve possuir entre 2 e 50 caracteres números e caracteres especiais'
        },
        lastName: {
          required: 'Informe o sobrenome do colaborador',
          rangeLength: 'A senha deve possuir entre 2 e 50 caracteres números e caracteres especiais'
        },
        email: {
          required: 'E-mail requerido',
          email: 'Email inválido'
        },
        birthDate: {
          required: 'Informe a data de nascimento do colaborador'
        },

      }

      this.genericValidator = new GenericValidator(this.validateMessages);
    }

  ngOnInit() {
    this.aproverForm = this.fb.group({
      name: ['', [Validators.required, CustomValidators.rangeLength([2,50])]],
      lastName: ['', [Validators.required, CustomValidators.rangeLength([2,50])]],
      email: ['', [Validators.email, Validators.required]],
      birthDate: ['', [Validators.required]]
    })
  }

  ngAfterViewInit(){
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processMessages(this.aproverForm);
      this.changesNotSave = true;
    })
  }

  addAprover(){
    if(this.aproverForm.dirty && this.aproverForm.valid){


      this.aprover = Object.assign({}, this.aprover, this.aproverForm.value);
      this.formResult = JSON.stringify(this.aprover);

      var date = this.validateDate(this.aprover.birthDate)

      this.aprover.birthDate = `${date.year}-${date.month}-${date.day}`

      this.aproverService.newAprover(this.aprover)
        .subscribe(
          success => { this.proccessSuccess(success)},
          error => { this.proccessFail(error)}
        );
      
      this.changesNotSave = false;
    }
  }

  proccessSuccess(response: any){
    this.aproverForm.reset
    this.errors = [];

    let toast = this.toastr.success('Aprovador cadastrado com sucesso!', 'Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/aprovadores/listar-todos']);
      });
    }
  }

  proccessFail(error: any){
    this.errors = error.error.errors.Message;
    this.toastr.error("Ocorreu um erro!, Ops..!!!");
  }


  validateDate(date: IDate) : IDate{
    if(date.day < '10') date.day = '0'+date.day
    if(date.month < '10') date.month = '0'+date.month
    return date;
  }




}
