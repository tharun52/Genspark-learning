import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { PaymentForm } from './payment-form';
import { Router } from '@angular/router';
import { PaymentService } from '../Services/payment.service';
import { ProductModel } from '../Models/product.models';


describe('PaymentForm', () => {
  let component: PaymentForm;
  let fixture: ComponentFixture<PaymentForm>;
  let mockRouter: any;
  let mockPaymentService: any;

  const mockProduct: ProductModel = {
    id: 1,
    title: 'Mock Product',
    price: 10,
    description: '',
    thumbnail: '',
  };

  beforeEach(() => {
    mockRouter = {
      getCurrentNavigation: () => ({
        extras: {
          state: { product: mockProduct }
        }
      }),
      navigate: jasmine.createSpy('navigate')
    };

    mockPaymentService = {
      payNow: jasmine.createSpy('payNow'),
      convertToINR: (usd: number) => Math.round(usd * 85.34)
    };

    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, PaymentForm],
      providers: [
        { provide: Router, useValue: mockRouter },
        { provide: PaymentService, useValue: mockPaymentService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(PaymentForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display product info with INR conversion', () => {
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.alert-info')?.textContent).toContain('Mock Product');
    expect(compiled.querySelector('.alert-info')?.textContent).toContain('₹ ' + component.convertToINR(mockProduct.price));
  });

  it('should validate incorrect email and phone', () => {
    component.paymentForm.setValue({
      name: 'User',
      email: 'wrongemail',
      contactno: '123'
    });
    expect(component.paymentForm.valid).toBeFalse();
  });

  it('should validate correct form data', () => {
    component.paymentForm.setValue({
      name: 'User',
      email: 'user@example.com',
      contactno: '9876543210'
    });
    expect(component.paymentForm.valid).toBeTrue();
  });

  it('should call paymentService.payNow with proper data on valid form submit', () => {
    const spy = spyOn(mockPaymentService, 'convertToINR').and.callThrough();

    component.paymentForm.setValue({
      name: 'John',
      email: 'john@example.com',
      contactno: '9999999999'
    });

    component.onSubmit();

    expect(component.paymentForm.valid).toBeTrue();
    expect(spy).toHaveBeenCalledWith(10); 
    expect(mockPaymentService.payNow).toHaveBeenCalledWith(jasmine.objectContaining({
      name: 'John',
      email: 'john@example.com',
      contactno: '9999999999',
      productName: 'Mock Product',
      amount: Math.round(10 * 85.34),
    }));
  });

});
