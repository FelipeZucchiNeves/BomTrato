import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AproverService } from 'src/app/shared/services/aprover.service';
import { Office } from '../../shared/models/office';
import { OfficeService } from '../../shared/services/office.service';

@Component({
  selector: 'app-remove',
  templateUrl: './remove.component.html'
})
export class RemoveComponent{

  office: Office = new Office();
  errors: any [] = []
  addressToMap;

  constructor(private officeService: OfficeService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private sanitizer: DomSanitizer) { 

      this.office = this.route.snapshot.data['office'];
      this.addressToMap = this.sanitizer.bypassSecurityTrustResourceUrl("https://www.google.com/maps/embed/v1/place?q=" + this.fullAddress() + "&key=AIzaSyAP0WKpL7uTRHGKWyakgQXbW6FUhrrA5pE");


    }

    public fullAddress(): string {
      return this.office.street + ", " + this.office.number + " - " + this.office.district + ", " + this.office.city + " - " + this.office.state;
    }

    removeOffice() {
      this.officeService.deleteOffice(this.office.id)
        .subscribe(
          fornecedor => { this.removeSuccess(fornecedor) },
          error => { this.fail(error) }
        );
    }
  
    removeSuccess(success: any) {

      const toast = this.toastr.success('Escritorio excluido com Sucesso!', 'Good bye :D');
      if (toast) {
        toast.onHidden.subscribe(() => {
          this.router.navigate(['/escritorios/listar-todos']);
        });
      }
    }
  
    fail(fail) {
      this.errors = fail.error.errors.Message;
      this.toastr.error('Houve um erro no processamento!', 'Ops! :(');
    }

}
