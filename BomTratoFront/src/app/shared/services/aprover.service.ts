import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { Aprover } from "../models/aprover";
import { BaseService } from "src/app/services/base.service";


@Injectable()
export class AproverService extends BaseService {

  aprover: Aprover =  new Aprover();

  constructor(private http: HttpClient) {super () }

  getAll(): Observable<Aprover[]>{
    return this.http
      .get<Aprover[]> (this.UrlServiceV1 + 'aprovador-management')
      .pipe(catchError(super.serviceError));
  }

  getById(id: string): Observable<Aprover>{
    return this.http
      .get<Aprover>(this.UrlServiceV1 + 'aprovador-management/' + id, super.getAuthHeaderJson())
      .pipe(catchError(super.serviceError))

  }

  
  newAprover(aprover: Aprover): Observable<Aprover>{
    return this.http
      .post(this.UrlServiceV1 + 'aprovador-management', aprover, this.getAuthHeaderJson())
      .pipe(
        map(super.extractData),
        catchError(super.serviceError));
  }


  updateAprover(aprover: Aprover): Observable<Aprover>{
    return this.http
            .put(this.UrlServiceV1 + "aprovador-management", aprover, super.getAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
  }


  deleteAprover(id: string): Observable<Aprover>{
    return this.http
      .delete(this.UrlServiceV1 + "aprovador-management/" +id, super.getAuthHeaderJson())
      .pipe(
        map(super.extractData),
        catchError(super.serviceError));
  }
}
