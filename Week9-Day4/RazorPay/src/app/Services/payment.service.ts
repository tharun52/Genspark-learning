import { Injectable } from '@angular/core';
import { PaymentModel } from '../Models/payment.model';
import { environment } from '../../enviroment/environment';

declare var Razorpay: any;

@Injectable()
export class PaymentService {
  private razorpayKey = 'rzp_test_MutvYPhZwBIOoj'; 

  constructor() {}

  public payNow(paymentData: PaymentModel): void {
     const options = {
      key: environment.api, 
      amount: this.convertToINR(paymentData.amount),
      currency: 'INR',
      name: 'UPI Simulator',
      description: 'Transaction for testing',
      handler:function (response: any) {
        alert('Payment Successful!');
      },
      prefill: {
        name: paymentData.name,
        email: paymentData.email,
        contact: paymentData.contactno
      },
      notes: {
        address: 'Test address'
      },
      theme: {
        color: '#ffffff'
      },
      method: {
        upi: true
      },
    };
    const rzp = new Razorpay(options);
    rzp.open();
  }

  convertToINR(priceUSD: number): number {
    const exchangeRate = 85.34;
    return Math.round(priceUSD * exchangeRate);
  }
}
