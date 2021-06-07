import { Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomValidators } from 'ngx-custom-validators';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { fromEvent, merge, Observable } from 'rxjs';
import { Process } from 'src/app/shared/models/process';
import { ProcessService } from 'src/app/shared/services/process.service';
import { DisplayMessage, GenericValidator, ValidationMessages } from 'src/app/utils/generic-form-validation';
import { LocalStorageUtils } from 'src/app/utils/localstorage';
import { Aprover } from '../../shared/models/aprover';
import { AproverService } from '../../shared/services/aprover.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html'
})
export class EditComponent implements OnInit {

  @ViewChildren(FormControlName, {read: ElementRef}) formInputElements: ElementRef[];

  errors: any[] = [];
  aproverForm: FormGroup;

  aprover: Aprover = new Aprover();

  process: Process[];
  localStorageUtils = new LocalStorageUtils();

  user = this.localStorageUtils.getUser();

  validateMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  formResult: string = '';
  changesNotSave: boolean;

  constructor(private fb: FormBuilder,
    private aproverService: AproverService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private processService: ProcessService) { 

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
          required: 'Informe o email do colaborador',
          email: 'Email inválido'
        },

      }

      this.genericValidator = new GenericValidator(this.validateMessages);

      this.aprover = this.route.snapshot.data['aprover'];
      



    }

  ngOnInit() {

    this.spinner.show();

    this.aproverForm = this.fb.group({
      id: '',
      name: ['', [Validators.required, CustomValidators.rangeLength([2,50])]],
      lastName: ['', [Validators.required, CustomValidators.rangeLength([2,50])]],
      email: ['', [Validators.email, Validators.required]],
    })

    this.fillForm();

    this.spinner.hide();
  }

  fillForm(){
    this.aproverForm.patchValue({
      id: this.aprover.id,
      name: this.aprover.name,
      lastName: this.aprover.lastName,
      email: this.aprover.email
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

  editAprover(){
    if(this.aproverForm.dirty && this.aproverForm.valid){
      
      this.aprover = Object.assign({}, this.aprover, this.aproverForm.value);

      this.aproverService.updateAprover(this.aprover)
        .subscribe(
          success => { this.proccessSuccess(success)},
          fail => { this.proccessFail(fail)}
        );

        this.changesNotSave = false;
    }
  }


  proccessSuccess(response: any){
    this.errors = [];

    let toast = this.toastr.success('Aprovador atualizado com sucesso!', 'Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/aprovadores/listar-todos']);
      });
    }
  }

  proccessFail(fail: any){
    this.errors = fail.error.errors.Message;
    this.toastr.error("Ocorreu um erro!, Ops..!!!");
  }

}
