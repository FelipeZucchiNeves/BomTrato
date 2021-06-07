import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot } from "@angular/router";
import { Process } from "../../shared/models/process";
import { ProcessService } from "../../shared/services/process.service";

@Injectable()
export class ProcessResolver implements Resolve<Process> {

    constructor(private officeService: ProcessService) { }

    resolve(route: ActivatedRouteSnapshot) {
        return this.officeService.getById(route.params['id']);
    }
}