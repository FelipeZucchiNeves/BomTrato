import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { Aprover } from "../../shared/models/aprover";
import { AproverService } from "../../shared/services/aprover.service";

@Injectable()
export class AproverResolver implements Resolve<Aprover> {
    constructor(private aprovadorService: AproverService){ }

    resolve(route: ActivatedRouteSnapshot){
    
        return this.aprovadorService.getById(route.params['id'])
    }
}