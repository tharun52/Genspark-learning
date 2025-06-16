import { Component, Input } from '@angular/core';
import { ProductModel } from '../models/ProductModel';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product',
  imports: [CurrencyPipe],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
@Input() product: ProductModel | null = new ProductModel();

  getInrPrice(): number {
    if (!this.product) return 0;
    const exchangeRate = 86.03;
    return this.product.price * exchangeRate;
  }
}
