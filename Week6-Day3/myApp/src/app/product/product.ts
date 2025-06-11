import { Component, inject, Input } from '@angular/core';
import { ProductService } from '../services/product.service';
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
  private productService = inject(ProductService);

  constructor() {
  }
}