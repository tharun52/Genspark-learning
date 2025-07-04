import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../Models/product.models';
import { ProductService } from '../Services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  imports: [],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products implements OnInit{
  products: ProductModel[] = [];
  loading = true;

  constructor(
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe((res) => {
      this.products = res.products;
      this.loading = false;
    });
  }

  buyNow(product: ProductModel) {
    this.router.navigate(['/payment'], {
      state: { product },
    });
  }

  convertToINR(priceUSD: number): number {
    const exchangeRate = 85.34;
    return Math.round(priceUSD * exchangeRate);
  }
}
