import { Component} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Aprover } from '../../shared/models/aprover';
import { AproverService } from '../../shared/services/aprover.service';

@Component({
  selector: 'app-remove',
  templateUrl: './remove.component.html'
})
export class RemoveComponent {

  aprover: Aprover = new Aprover();
  errors: any[] = [];

  constructor(
    private aproverService: AproverService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) { 
    
    this.aprover = this.route.snapshot.data['aprover'];


  }


  removeAprover(){
    this.aproverService.deleteAprover(this.aprover.id)
      .subscribe(
        success => { this.removeSuccess(success) },
        fail => { this.fail(fail) }
      );
      
  }

  removeSuccess(success: any) {

    const toast = this.toastr.success('Aprovador excluido com Sucesso!', 'Good bye :D');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/aprovadores/listar-todos']);
      });
    }
  }

  fail(fail) {
    this.errors = fail.error.errors.Message;
    this.toastr.error('Houve um erro no processamento!', 'Ops! :(');
  }

}
