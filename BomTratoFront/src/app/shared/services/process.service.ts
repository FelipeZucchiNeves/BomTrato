import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { BaseService } from 'src/app/services/base.service';
import { Process } from '../models/process';

@Injectable()
export class ProcessService extends BaseService{

  process: Process = new Process();

    constructor(private http: HttpClient) { super() }

    getAll(): Observable<Process[]> {
        return this.http
            .get<Process[]>(this.UrlServiceV1 + "processo-management")
            .pipe(catchError(super.serviceError));
    }

    getById(id: string): Observable<Process> {
        return this.http
            .get<Process>(this.UrlServiceV1 + "processo-management/" + id, super.getAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    newProcess(process: Process): Observable<Process> {
        return this.http
            .post(this.UrlServiceV1 + "processo-management", process, this.getAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    updateProcess(process: Process): Observable<Process> {
        return this.http
            .put(this.UrlServiceV1 + "processo-management", process, super.getAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    deleteProcess(id: string): Observable<Process> {
        return this.http
            .delete(this.UrlServiceV1 + "processo-management/" + id, super.getAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }


}
