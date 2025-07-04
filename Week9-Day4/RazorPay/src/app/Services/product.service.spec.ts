import { TestBed } from '@angular/core/testing';
import { ProductService } from './product.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('ProductService', () => {
  let service: ProductService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ProductService],
    });

    service = TestBed.inject(ProductService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should fetch products from API', () => {
    const mockResponse = {
      products: [
        { id: 1, title: 'Test Product', price: 100, thumbnail: '', description: '' }
      ]
    };

    service.getProducts().subscribe((res) => {
      expect(res.products.length).toBe(1);
      expect(res.products[0].title).toBe('Test Product');
    });

    const req = httpMock.expectOne('https://dummyjson.com/products');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });
});
