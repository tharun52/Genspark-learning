import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { catchError, Observable, throwError } from "rxjs";

@Injectable()
export class ProductService{
    private http = inject(HttpClient);
    
    getProductSearchResult(searchData:string,limit:number=10,skip:number=0)
    {
        return this.http.get(`https://dummyjson.com/products/search?q=${searchData}&limit=${limit}&skip=${skip}`)
    }
    getSingleProduct(id:number)
    {
        return this.http.get(`https://dummyjson.com/products/${id}`);
    }
    createProductToken()
    {
        localStorage.setItem('token', "AccessToken");
    }
}