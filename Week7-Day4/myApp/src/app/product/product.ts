import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
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
  private productService = inject(ProductService);
  
  @Input() product: ProductModel | null = new ProductModel();
  @Output() addToCart: EventEmitter<number> = new EventEmitter<number>();

  handleBuyClick(pid: number | undefined) {
    if (pid) {
      this.addToCart.emit(pid);
    }
  }
  constructor() {
    // this.productService.getProduct(1).subscribe(
    //   {
    //     next:(data)=>{

    //       this.product = data as ProductModel;
    //       console.log(this.product)
    //     },
    //     error:(err)=>{
    //       console.log(err)
    //     },
    //     complete:()=>{
    //       console.log("All done");
    //     }
    //   })
  }

}
