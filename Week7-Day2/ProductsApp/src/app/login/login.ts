import { Component } from '@angular/core';
import { ProductService } from '../services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  constructor(private productService:ProductService, private route: Router) {

  }
  handleLogin() {
    this.productService.createProductToken();
    this.route.navigateByUrl("/products");
  }
}
