import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { CustomValidators } from 'ngx-custom-validators';
import { fromEvent, merge, Observable } from 'rxjs';

import { DisplayMessage, GenericValidator, ValidationMessages } from 'src/app/utils/generic-form-validation';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';

import { ToastrService } from 'ngx-toastr';




@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, {read: ElementRef}) formInputElements: ElementRef[];

  registerForm: FormGroup;
  user: User;

  errors: any[] =[];

  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  changesNotSave: boolean;

  constructor(private fb: FormBuilder, 
              private accountService: AccountService,
              private router: Router,
              private toastr: ToastrService) {
    this.validationMessages = {
      email:{
        required: 'Informe o e-mail',
        email: 'Email inválido'
      },
      password:{
        required: 'Informe a senha',
        rangeLength: 'A senha deve possuir entre 6 e 15 caracteres números e caracteres especiais'
      },
      confirmPassword:{
        required: 'Informe a mesma senha',
        rangeLength: 'A senha deve possuir entre 6 e 15 caracteres números e caracteres especiais',
        equalTo: 'As senhas não conferem'
      },
    };

    this.genericValidator = new GenericValidator(this.validationMessages)
   }

  ngOnInit(): void {


    let pass = new FormControl('', [Validators.required, CustomValidators.rangeLength([6, 15])]);
    let confirmPass = new FormControl('', [Validators.required, CustomValidators.rangeLength([6, 15]), CustomValidators.equalTo(pass)]);

    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: pass,
      confirmPassword: confirmPass
    })
  }

  ngAfterViewInit(): void{
    let controlBlurs: Observable<any>[] = this.formInputElements
    .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processMessages(this.registerForm);
      this.changesNotSave = true;
    });
  }

  addAccount(){
    if(this.registerForm.dirty && this.registerForm.valid){
      this.user = Object.assign({}, this.user, this.registerForm.value);

      console.log(this.user)

      this.accountService.registerUser(this.user).subscribe(
        success => {this.proccessSuccess(success)},
        error => {this.proccessError(error)}
      );

      this.changesNotSave = false;
    }
  }

  proccessSuccess(response : any) {
    this.registerForm.reset();
    this.errors =[];

    this.accountService.LocalStorage.saveUserLocalData(response);

    //this.toastr.success('Registro realizado com sucesso', 'Bem vindo!')

    this.router.navigate(['/home'])
  }

  proccessError(error : any) {
    this.errors = error.error.errors.Message;
    this.toastr.error('Ocorreu um erro!', 'Opa :(')
  }

}
