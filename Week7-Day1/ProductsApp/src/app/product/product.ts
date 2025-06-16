import { Component, Input } from '@angular/core';
import { ProductModel } from '../models/ProductModel';
import { CurrencyPipe } from '@angular/common';
import { HighlightPipe } from '../pipes/highlight.pipe';

@Component({
  selector: 'app-product',
  imports: [CurrencyPipe, HighlightPipe],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
@Input() product: ProductModel | null = new ProductModel();
@Input() search: string = ""; 

  getInrPrice(): number {
    if (!this.product) return 0;
    const exchangeRate = 86.03;
    return this.product.price * exchangeRate;
  }
}
