import { TestBed } from '@angular/core/testing';
import { PaymentService } from './payment.service';
import { PaymentModel } from '../Models/payment.model';

describe('PaymentService', () => {
  let service: PaymentService;
  let razorpaySpy: jasmine.Spy;

  beforeEach(() => {
    (window as any).Razorpay = jasmine.createSpy('Razorpay').and.callFake(function (options: any) {
      return {
        open: jasmine.createSpy('open'),
        options: options
      };
    });

    TestBed.configureTestingModule({
      providers: [PaymentService],
    });

    service = TestBed.inject(PaymentService);
  });



  it('should initialize Razorpay and call open()', () => {
    const mockPaymentData: PaymentModel = {
      name: 'Test User',
      email: 'test@example.com',
      contactno: '9876543210',
      productName: 'Test Product',
      amount: 5, 
    };

    service.payNow(mockPaymentData);

    expect((window as any).Razorpay).toHaveBeenCalled();

    const instance = (window as any).Razorpay.calls.mostRecent().returnValue;
    expect(instance.open).toHaveBeenCalled();
  });

  it('should include correct prefill details', () => {
    const mockPaymentData: PaymentModel = {
      name: 'Jane Doe',
      email: 'jane@example.com',
      contactno: '1234567890',
      productName: 'Premium Product',
      amount: 3,
    };

    service.payNow(mockPaymentData);

    const lastCall = (window as any).Razorpay.calls.mostRecent().args[0];
    expect(lastCall.prefill.name).toBe('Jane Doe');
    expect(lastCall.prefill.email).toBe('jane@example.com');
    expect(lastCall.prefill.contact).toBe('1234567890');
    expect(lastCall.amount).toBe(service.convertToINR(mockPaymentData.amount));
  });
});
