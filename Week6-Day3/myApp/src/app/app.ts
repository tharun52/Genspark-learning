import { Component } from '@angular/core';
import { First } from "./first/first";
import { CustomerDetails } from "./customer-details/customer-details";
import { Cart } from "./cart/cart";
import { Product } from "./product/product";
import { Products } from "./products/products";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [Products]
})
export class App {
  protected title = 'myApp';
}
