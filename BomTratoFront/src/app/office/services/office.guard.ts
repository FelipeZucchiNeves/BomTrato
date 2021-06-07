import { Injectable } from "@angular/core";
import { CanActivate, CanDeactivate, Router } from "@angular/router";
import { AddComponent } from "src/app/office/add/add.component";
import { LocalStorageUtils } from "src/app/utils/localstorage";
import { OfficeAppComponent } from "../office.app.component";

@Injectable()
export class OfficeGuard implements CanDeactivate<OfficeAppComponent>, CanActivate{
    
    localStorageUtils = new LocalStorageUtils();
    
    constructor(private router: Router){ }
    canDeactivate(component: AddComponent) {
        if(component.changesNotSave){
            return window.confirm('Tem certeza que deseja abandonar o preenchimento do formulario?');
        }

        return true;
    }

    canActivate(){
        if (!this.localStorageUtils.getUserToken()) {
            this.router.navigate(['/conta/login']);
        }

        return true;
    }
}