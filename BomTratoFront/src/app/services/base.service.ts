import { HttpErrorResponse, HttpHeaders } from "@angular/common/http"
import { throwError } from "rxjs";
import { environment } from "src/environments/environment";
import { LocalStorageUtils } from "../utils/localstorage";

export abstract class BaseService{

    public LocalStorage = new LocalStorageUtils();

    protected UrlServiceV1 : string = environment.apiUrlv1;

    protected GetHeaderJson(){
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        }
    }

    protected getAuthHeaderJson(){
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ${this.LocalStorage.getUserToken()}'
            })
        };
    }

    protected extractData(response: any){
        return response || {};
    }

    protected serviceError(response : Response | any) {
        let customError: string[] = [];

        if(response instanceof HttpErrorResponse){
            if(response.statusText === "Unknown Error"){
                customError.push("Ocorreu um erro desconhecido");
                response.error.errors.Message = customError;
                
            }
        }
        console.error(response);
        return throwError(response);
    }
    
}
