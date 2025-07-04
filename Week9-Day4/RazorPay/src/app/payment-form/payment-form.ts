import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductModel } from '../Models/product.models';
import { Router } from '@angular/router';
import { PaymentModel } from '../Models/payment.model';
import { PaymentService } from '../Services/payment.service';


// spIOgfNnjJf5Bu4pPDnfTAXA
// rzp_test_MutvYPhZwBIOoj
@Component({
  selector: 'app-payment-form',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './payment-form.html',
  styleUrl: './payment-form.css'
})
export class PaymentForm {
  paymentForm: FormGroup;
  submitted = false;
  product: ProductModel | null = null;

  constructor(private router: Router, private paymentService:PaymentService) {
    const nav = this.router.getCurrentNavigation();
    this.product = nav?.extras?.state?.['product'] || null;

    this.paymentForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      contactno: new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]{10}$'),
      ]),
    });
  }

  public get name() {
    return this.paymentForm.get('name');
  }
  public get amount() {
    return this.paymentForm.get('amount');
  }
  public get email() {
    return this.paymentForm.get('email');
  }
  public get contactno() {
    return this.paymentForm.get('contactno');
  }

  onSubmit(): void {
  this.submitted = true;
  if (this.paymentForm.valid && this.product) {
    const data: PaymentModel = {
      name: this.paymentForm.value.name,
      email: this.paymentForm.value.email,
      contactno: this.paymentForm.value.contactno,
      productName: this.product.title,
      amount: this.paymentService.convertToINR(this.product.price),
    };
      this.paymentService.payNow(data);
    }
  }

  convertToINR(priceUSD: number): number {
    const exchangeRate = 85.34;
    return Math.round(priceUSD * exchangeRate);
  }
}