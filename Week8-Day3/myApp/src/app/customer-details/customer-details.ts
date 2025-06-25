import { Component } from '@angular/core';
import * as customerData from '../assets/customers.json';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-customer-details',
  imports: [CommonModule],
  templateUrl: './customer-details.html',
  styleUrl: './customer-details.css'
})
export class CustomerDetails {
  data: any = customerData;

  ngOnInit() {
    this.data.default.forEach((customer: any) => {
      customer.likeCount = 0;
      customer.likeClassName = "bi bi-hand-thumbs-up";
      customer.unLikeClassName = "bi bi-hand-thumbs-down";
    });
  }

  toggleLike(customer: any) {
    customer.likeClassName = "bi bi-hand-thumbs-up-fill";
    customer.likeCount += 1;
    if (customer.likeCount == 0) {
      customer.likeClassName = "bi bi-hand-thumbs-up";
      customer.unLikeClassName = "bi bi-hand-thumbs-down";
    }
  }

  toggleUnLike(customer: any) {
    customer.unLikeClassName = "bi bi-hand-thumbs-down-fill";
    customer.likeCount -= 1;
    if (customer.likeCount == 0) {
      customer.likeClassName = "bi bi-hand-thumbs-up";
      customer.unLikeClassName = "bi bi-hand-thumbs-down";
    }
  }
  getLikeClass(customer: any): string {
    return customer.likeClassName;
  }
  getUnLikeClass(customer: any): string {
    return customer.unLikeClassName;
  }
}

