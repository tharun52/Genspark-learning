import { Component, HostListener } from '@angular/core';
import { Subject, debounceTime, distinctUntilChanged, tap, switchMap } from 'rxjs';
import { ProductModel } from '../models/ProductModel';
import { ProductService } from '../services/product.service';
import { FormsModule } from '@angular/forms';
import { Product } from '../product/product';

@Component({
  selector: 'app-products',
  imports: [FormsModule, Product],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  products: ProductModel[] = [];
  searchString: string = "";
  searchSubject = new Subject<string>();
  loading: boolean = true;
  limit = 10;
  skip = 0;
  total = 0;
  showBackToTop: boolean = false;

  constructor(private productService: ProductService) {
  }

  handleSearchProducts() {
    this.searchSubject.next(this.searchString);
  }

  ngOnInit(): void {
    this.productService.getProductSearchResult('', this.limit, this.skip).subscribe({
      next: (data: any) => {
        this.products = data.products as ProductModel[];
        this.total = data.total;
        this.loading = false;
      },
      error: (err) => {
        console.error("Error loading products:", err);
        this.loading = false;
      }
    });

    this.searchSubject.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      tap(() => this.loading = true),
      switchMap(query => this.productService.getProductSearchResult(query, this.limit, this.skip)),
      tap(() => this.loading = false)).subscribe({
        next: (data: any) => {
          this.products = data.products as ProductModel[];
          this.total = data.total;
          console.log(this.total)
        }
      });
  }

  @HostListener('window:scroll', [])
  onScroll(): void {

    const scrollPosition = window.innerHeight + window.scrollY;
    const threshold = document.body.offsetHeight - 100;
    this.showBackToTop = scrollPosition > 1000;
    if (scrollPosition >= threshold && this.products?.length < this.total) {
      this.loadMore();
    }
  }

  scrollToTop(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
    if (window.scrollY == 0) {
      this.showBackToTop = false;
    }
  }

  loadMore() {
    this.loading = true;
    this.skip += this.limit;

    this.productService.getProductSearchResult(this.searchString, this.limit, this.skip)
      .subscribe({
        next: (data: any) => {
          this.products = [...this.products, ...data.products];
          this.loading = false;
        }
      });
  }
}
