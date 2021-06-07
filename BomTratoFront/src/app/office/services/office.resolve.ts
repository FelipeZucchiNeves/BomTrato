import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot } from "@angular/router";
import { Office } from "../../shared/models/office";
import { OfficeService } from "../../shared/services/office.service";

@Injectable()
export class OfficeResolver implements Resolve<Office> {

    constructor(private officeService: OfficeService) { }

    resolve(route: ActivatedRouteSnapshot) {
        return this.officeService.getOfficeById(route.params['id']);
    }
}