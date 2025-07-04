import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductModel } from '../Models/product.models';

@Injectable()
export class ProductService {
  private apiUrl = 'https://dummyjson.com/products';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<{ products: ProductModel[] }> {
    return this.http.get<{ products: ProductModel[] }>(this.apiUrl);
  }
}