export class LocalStorageUtils{

    public getUser(){
        return JSON.parse(localStorage.getItem('bt.user'));
    }


    public saveUserLocalData( response : any){
        this.saveUserToken(response.jwt);
        this.saveUser(response.user);
    }

    public removeUserLocalData(){
        localStorage.removeItem('bt.jwt');
        localStorage.removeItem('bt.user');
    }

    public getUserToken() : string {
        return localStorage.getItem('bt.jwt');
    }


    public saveUserToken(token : string){
        localStorage.setItem('bt.jwt', token);
    }

    public saveUser(user : string){
        localStorage.setItem('bt.user', JSON.stringify(user));
    }


}