import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from "../models/user";
import { catchError, map} from "rxjs/operators";
import { BaseService } from "src/app/services/base.service";

@Injectable()
export class AccountService extends BaseService {
    constructor(private http: HttpClient){ super(); }


    registerUser(user: User) : Observable<User>{
        let response = this.http
            .post(this.UrlServiceV1 + 'register', user, this.GetHeaderJson())
            .pipe(
                map(this.extractData),
                catchError(this.serviceError));

        return response;

    }

    login(user: User) : Observable<User>{
        let response = this.http
        .post(this.UrlServiceV1 + 'login', user, this.GetHeaderJson())
        .pipe(
            map(this.extractData),
            catchError(this.serviceError));

        return response;
    }
}