import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { PaymentForm } from "./payment-form/payment-form";
import { Products } from "./products/products";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'RazorPay';
}
