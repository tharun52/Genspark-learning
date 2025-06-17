import { Component, Input } from '@angular/core';
import { ProductModel } from '../models/ProductModel';
import { CurrencyPipe } from '@angular/common';
import { HighlightPipe } from '../pipes/highlight.pipe';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-product',
  imports: [CurrencyPipe, HighlightPipe, RouterLink],
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
