import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Products } from './products';
import { ProductService } from '../Services/product.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { ProductModel } from '../Models/product.models';


describe('Products Component', () => {
  let component: Products;
  let fixture: ComponentFixture<Products>;
  let mockProductService: any;
  let mockRouter: any;

  const mockProducts: ProductModel[] = [
    {
      id: 1,
      title: 'Test Product',
      price: 10,
      description: 'Test description',
      thumbnail: 'test.jpg'
    },
    {
      id: 2,
      title: 'Another Product',
      price: 20,
      description: 'Another description',
      thumbnail: 'another.jpg'
    }
  ];

  beforeEach(async () => {
    mockProductService = {
      getProducts: jasmine.createSpy('getProducts').and.returnValue(of({ products: mockProducts }))
    };

    mockRouter = {
      navigate: jasmine.createSpy('navigate')
    };

    await TestBed.configureTestingModule({
      imports: [Products],
      providers: [
        { provide: ProductService, useValue: mockProductService },
        { provide: Router, useValue: mockRouter }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Products);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call productService.getProducts()', () => {
    expect(mockProductService.getProducts).toHaveBeenCalled();
    expect(component.products.length).toBe(2);
    expect(component.loading).toBeFalse();
  });
});
