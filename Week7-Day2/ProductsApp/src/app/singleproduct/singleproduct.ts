import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SingleProductModel } from '../models/SingleProductModel';
import { CurrencyPipe} from '@angular/common';

@Component({
  selector: 'app-singleproduct',
  imports: [CurrencyPipe],
  templateUrl: './singleproduct.html',
  styleUrl: './singleproduct.css'
})
export class Singleproduct implements OnInit {

  router = inject(ActivatedRoute);
  productService = inject(ProductService);
  product: SingleProductModel | undefined = new SingleProductModel();

  getInrPrice(): number {
    if (!this.product) return 0;
    const exchangeRate = 86.03;
    return this.product.price * exchangeRate;
  }

  ngOnInit(): void {
    const productId = this.router.snapshot.params["id"] as number;
    this.productService.getSingleProduct(productId).subscribe({
      next: (data: any) => {
        this.product = data as SingleProductModel;
      },
      error: () => { }
    })
  }
}
