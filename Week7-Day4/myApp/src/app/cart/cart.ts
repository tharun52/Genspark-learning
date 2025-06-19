import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import * as productData from '../assets/products.json';  

@Component({
  selector: 'app-cart',
  imports: [CommonModule],
  templateUrl: './cart.html',
  styleUrl: './cart.css'
})
export class Cart {
  data: any = productData;
  
  cartquantity:number = 0;

  ngOnInit(){
    this.data.default.forEach((product: any) => {
      product.quantity = 0;
    });
  }
  toggleAddCart(product:any){
    product.quantity += 1;
    this.cartquantity += 1;
  }
  toggleMinusCart(product:any){
    if (product.quantity > 0){
      product.quantity -= 1;
      this.cartquantity -= 1;
    }
  }
}
