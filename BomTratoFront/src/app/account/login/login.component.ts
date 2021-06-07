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
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

  @ViewChildren(FormControlName, {read: ElementRef}) formInputElements: ElementRef[];

  loginForm: FormGroup;
  user: User;

  errors: any[] =[];

  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

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
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages)
   }

  ngOnInit(): void {
    
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, CustomValidators.rangeLength([6,15])]],
    })
  }

  ngAfterViewInit(): void{
    let controlBlurs: Observable<any>[] = this.formInputElements
    .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processMessages(this.loginForm);
    });
  }

  login(){
    if(this.loginForm.dirty && this.loginForm.valid){
      this.user = Object.assign({}, this.user, this.loginForm.value);

      this.accountService.login(this.user).subscribe(
        success => {this.proccessSuccess(success)},
        error => {this.proccessError(error)}
      );

    }
  }

  proccessSuccess(response : any) {
    this.loginForm.reset();
    this.errors =[];

    this.accountService.LocalStorage.saveUserLocalData(response);

    //this.toastr.success('Login realizado com sucesso', 'Bem vindo!')
    this.router.navigate(['/home'])
  }

  proccessError(error : any) {
    this.errors = error.error.errors.Message;
    this.toastr.error('Ocorreu um erro!', 'Opa :(')
  }

}
