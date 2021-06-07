import { Injectable } from "@angular/core";
import { CanActivate, CanDeactivate, Router } from "@angular/router";
import { AddComponent } from "src/app/office/add/add.component";
import { LocalStorageUtils } from "src/app/utils/localstorage";
import { ProcessAppComponent } from "../process.app.component";

@Injectable()
export class ProcessGuard implements CanDeactivate<ProcessAppComponent>, CanActivate{
    
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