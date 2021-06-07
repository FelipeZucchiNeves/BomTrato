import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { AproverResolver } from 'src/app/aprover/services/aprover.resolve';
import { BaseService } from 'src/app/services/base.service';
import { Office, GetCep } from '../models/office';

@Injectable()
export class OfficeService extends BaseService{

  fornecedor: Office = new Office();

    constructor(private http: HttpClient) { super() }

    getAllOffices(): Observable<Office[]> {AproverResolver
        return this.http
            .get<Office[]>(this.UrlServiceV1 + "escritorio-management")
            .pipe(catchError(super.serviceError));
    }

    getOfficeById(id: string): Observable<Office> {
        return this.http
            .get<Office>(this.UrlServiceV1 + "escritorio-management/" + id, super.getAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    newOffice(escritorio: Office): Observable<Office> {
        return this.http
            .post(this.UrlServiceV1 + "escritorio-management", escritorio, this.getAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    updateOffice(escritorio: Office): Observable<Office> {
        return this.http
            .put(this.UrlServiceV1 + "escritorio-management", escritorio, super.getAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    deleteOffice(id: string): Observable<Office> {
        return this.http
            .delete(this.UrlServiceV1 + "escritorio-management/" + id, super.getAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }


    getCep(cep: string): Observable<GetCep> {
        return this.http
            .get<GetCep>(`https://viacep.com.br/ws/${cep}/json/`)
            .pipe(catchError(super.serviceError))
    }
}
